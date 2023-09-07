﻿using BAGeocoding.Api.Dto;
using BAGeocoding.Api.Interfaces;
using BAGeocoding.Api.Models;
using BAGeocoding.Api.Models.PBD;
using BAGeocoding.Bll;
using BAGeocoding.Entity.Enum;
using BAGeocoding.Utility;
using Nest;
using NetTopologySuite.Operation.Distance;
using System.ComponentModel;
using static BAGeocoding.Api.Models.PBD.RoadName;

namespace BAGeocoding.Api.Services;

public class RoadNameService : IRoadNameService
{
    private int NumberOfShards { get; set; } = 5;
    private int NumberOfReplicas { get; set; } = 1;
    private readonly ElasticClient _client;
    private readonly string _indexName;
    private readonly string _indexByProvince;
    //private readonly IConfiguration _configuration;
    private readonly IVietNamShapeService _vnShapeService;
    //private readonly ILogService _logService;

    private string GetIndexName()
    {
        var type = typeof(RoadName);

        var customAttributes = (DescriptionAttribute[])type
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (customAttributes != null && customAttributes.Length > 0)
            return customAttributes[0].Description;

        throw new Exception($"{nameof(RoadName)} description attribute is missing.");
    }

    private async Task<bool> IndexAsync(RoadName item, string indexName = null)
    {
        try
        {
            if (string.IsNullOrEmpty(indexName)) indexName = _indexName;

            var response = await _client.IndexAsync(new RoadName(item), q => q.Index(indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.UpdateAsync<RoadName>(item.RoadID.ToString(), a => a.Index(indexName).Doc(new RoadName(item)));
            }
            return response.IsValid;
        }
        catch { return false; }

    }

    private async Task<bool> IndexAsyncV2(RoadNameV2 item, string indexName = null)
    {
        try
        {
            if (string.IsNullOrEmpty(indexName)) indexName = _indexName;

            var response = await _client.IndexAsync(new RoadNameV2(item), q => q.Index(indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.UpdateAsync<RoadNameV2>(item.RoadID.ToString(), a => a.Index(indexName).Doc(new RoadNameV2(item)));
            }
            return response.IsValid;
        }
        catch { return false; }

    }

    private async Task<bool> IndexAsyncByProvince(RoadNameMerge item, string indexName = null)
    {
        try
        {
            if (string.IsNullOrEmpty(indexName)) indexName = _indexName;

            var response = await _client.IndexAsync(new RoadNameMerge(item), q => q.Index(indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.UpdateAsync<RoadNameMerge>(item.ProvinceID.ToString(), a => a.Index(indexName).Doc(new RoadNameMerge(item)));
            }
            return response.IsValid;
        }
        catch { return false; }

    }

    public async Task<string> CreateIndex(string indexName)
    {
        try
        {
            if (string.IsNullOrEmpty(indexName)) indexName = _indexName;

            await _client.Indices.DeleteAsync(Indices.Index(indexName));

            var indexResponse = await _client.Indices.CreateAsync(Indices.Index(indexName), c => c
           .Map<RoadName>(mm => mm.AutoMap())
           .Settings(s => s
               .NumberOfReplicas(NumberOfReplicas)
               .NumberOfShards(NumberOfShards)
               .Analysis(a => a
                   //.CharFilters(cf => cf
                   //    .Mapping("programming_language", mca => mca
                   //        .Mappings(new[]
                   //        {
                   //                 "c# => csharp",
                   //                 "C# => Csharp"
                   //        })
                   //    )
                   //  )
                   //.CharFilters(cf => cf
                   //    .Mapping("province_name", mca => mca
                   //        .Mappings(new[]
                   //        {
                   //             "hà nội => ha noi",
                   //             "Hà Nội => Ha Noi"
                   //        })
                   //    )
                   //  )
                   .TokenFilters(tf => tf
                       .AsciiFolding("ascii_folding", tk => new AsciiFoldingTokenFilter
                       {
                           PreserveOriginal = true
                       })
                       .Synonym("synonym_address", sf => new SynonymTokenFilter
                       {
                           Synonyms = new List<string>()
                           { "ha noi, hà nội, Hà Nội, Ha Noi, hn, hanoi, tp. ha noi, thành phố ha noi",
                                "tphcm,tp.hcm,tp hồ chí minh,sài gòn, saigon"
                           }
                       })
                       .Stop("stop_filter", st => new StopTokenFilter
                       {
                           StopWords = new List<string>()
                           { "H.","h.","Q.","q.","TP.","tp.","TX.","tx."
                           }
                       })
                   )
                   .Analyzers(an => an
                       .Custom("keyword_analyzer", ca => ca
                           .CharFilters("html_strip")
                           .Tokenizer("keyword")
                           .Filters("lowercase"))
                       .Custom("my_combined_analyzer", ca => ca
                           .CharFilters("html_strip")
                           .Tokenizer("vi_tokenizer")
                           .Filters("lowercase", "stop", "ascii_folding")
                       )
                       .Custom("vn_analyzer3", ca => ca
                           //.CharFilters("html_strip")
                           .Tokenizer("vi_tokenizer")
                           .Filters("lowercase", "ascii_folding", "stop_filter")
                       )
                       //.Custom("vi_analyzer2", ca => ca
                       //     .CharFilters("province_name")
                       //     .Tokenizer("vi_tokenizer")
                       //     .Filters("lowercase", "ascii_folding")
                       // )
                       .Custom("vn_analyzer", ca => ca
                           .CharFilters("html_strip")
                           .Tokenizer("vi_tokenizer")
                           .Filters("lowercase", "ascii_folding")
                       )
                       .Custom("address_analyzer", ca => ca
                           //.CharFilters("html_strip", "province_name")
                           .Tokenizer("vi_tokenizer")
                           .Filters("synonym_address", "lowercase", "ascii_folding")
                       )
                   )
               )
            )
        );

            return indexResponse.ApiCall.HttpStatusCode.ToString() ?? "OK";
        }
        catch (Exception ex) { return ex.ToString(); }
    }

    public async Task<string> CreateIndexByProvince(string indexName)
    {
        try
        {
            if (string.IsNullOrEmpty(indexName)) indexName = _indexName;

            await _client.Indices.DeleteAsync(Indices.Index(indexName));

            var indexResponse = await _client.Indices.CreateAsync(Indices.Index(indexName), c => c
           .Map<RoadNameMerge>(mm => mm.AutoMap())
           .Settings(s => s
               .NumberOfReplicas(NumberOfReplicas)
               .NumberOfShards(NumberOfShards)
               .Analysis(a => a
                   //.CharFilters(cf => cf
                   //    .Mapping("programming_language", mca => mca
                   //        .Mappings(new[]
                   //        {
                   //                 "c# => csharp",
                   //                 "C# => Csharp"
                   //        })
                   //    )
                   //  )
                   //.CharFilters(cf => cf
                   //    .Mapping("province_name", mca => mca
                   //        .Mappings(new[]
                   //        {
                   //             "hà nội => ha noi",
                   //             "Hà Nội => Ha Noi"
                   //        })
                   //    )
                   //  )
                   .TokenFilters(tf => tf
                       .AsciiFolding("ascii_folding", tk => new AsciiFoldingTokenFilter
                       {
                           PreserveOriginal = true
                       })
                       .Synonym("synonym_address", sf => new SynonymTokenFilter
                       {
                           Synonyms = new List<string>()
                           { "ha noi, hà nội, Hà Nội, Ha Noi, hn, hanoi, tp. ha noi, thành phố ha noi",
                                "tphcm,tp.hcm,tp hồ chí minh,sài gòn, saigon"
                           }
                       })
                       .Stop("stop_filter", st => new StopTokenFilter
                       {
                           StopWords = new List<string>()
                           { "H.","h.","Q.","q.","TP.","tp.","TX.","tx."
                           }
                       })
                   )
                   .Analyzers(an => an
                       .Custom("keyword_analyzer", ca => ca
                           .CharFilters("html_strip")
                           .Tokenizer("keyword")
                           .Filters("lowercase"))
                       .Custom("my_combined_analyzer", ca => ca
                           .CharFilters("html_strip")
                           .Tokenizer("vi_tokenizer")
                           .Filters("lowercase", "stop", "ascii_folding")
                       )
                       .Custom("vn_analyzer3", ca => ca
                           //.CharFilters("html_strip")
                           .Tokenizer("vi_tokenizer")
                           .Filters("lowercase", "ascii_folding", "stop_filter")
                       )
                       //.Custom("vi_analyzer2", ca => ca
                       //     .CharFilters("province_name")
                       //     .Tokenizer("vi_tokenizer")
                       //     .Filters("lowercase", "ascii_folding")
                       // )
                       .Custom("vn_analyzer", ca => ca
                           .CharFilters("html_strip")
                           .Tokenizer("vi_tokenizer")
                           .Filters("lowercase", "ascii_folding")
                       )
                       .Custom("address_analyzer", ca => ca
                           //.CharFilters("html_strip", "province_name")
                           .Tokenizer("vi_tokenizer")
                           .Filters("synonym_address", "lowercase", "ascii_folding")
                       )
                   )
               )
            )
        );

            return indexResponse.ApiCall.HttpStatusCode.ToString() ?? "OK";
        }
        catch (Exception ex) { return ex.ToString(); }
    }

    private async Task<int> GetProvinceId(double lat = 0, double lng = 0, string keyword = null)
    {
        List<VietNamShape> lst;
        lst = await _vnShapeService.GetDataSuggestion(lat, lng, GeoDistanceType.Arc, "50km", 5, null, GeoShapeRelation.Intersects);

        if (lst.Any())
            return lst.Where(x => x.provinceid > 0).Select(x => x.provinceid).FirstOrDefault();

        return 16;//Hà Nội
    }

    private async Task<int> GetProvinceIdIndex(double lat = 0, double lng = 0, string keyword = null)
    {
        List<VietNamShape> lst;
        lst = await _vnShapeService.GetDataSuggestion(lat, lng, GeoDistanceType.Arc, "50km", 5, null, GeoShapeRelation.Intersects);

        if (lst.Any())
            return lst.Where(x => x.provinceid > 0).Select(x => x.provinceid).FirstOrDefault();

        return 0;//Hà Nội
    }

    private MatchQuery MatchQuerySuggestion(string query, Field field, Fuzziness fuzziness, string analyzer = "vn_analyzer3")
    {
        return
            new MatchQuery
            {
                //Boost = 2.0,
                //Operator = Operator.And,
                //Fuzziness = Fuzziness.Auto,
                //PrefixLength = 3,
                //MinimumShouldMatch = 1,
                //Lenient = true,
                ZeroTermsQuery = ZeroTermsQuery.None,
                //AutoGenerateSynonymsPhraseQuery = true,
                //Name = "world_query",
                //Boosting = 1.0,
                //AutoGeneratePhraseQueries = true,
                //CutoffFrequency = 0.001,

                AutoGenerateSynonymsPhraseQuery = true,
                Name = "named_query",
                //Field = Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt),
                Field = field,
                Query = query,
                Fuzziness = fuzziness,
                Analyzer = "vn_analyzer3"
            };
    }
    private GeoDistanceQuery GeoDistanceQuerySuggestion(string field, double lat, double lng, string distance)
    {
        return new GeoDistanceQuery
        {
            Boost = 2.0,
            Name = "named_query",
            DistanceType = GeoDistanceType.Plane,
            Field = field,
            Location = new GeoLocation(lat, lng),
            Distance = distance,
            ValidationMethod = GeoValidationMethod.IgnoreMalformed
        };
    }

    public RoadNameService(ElasticClient client, IConfiguration configuration, IVietNamShapeService vnShapeService)
    {
        _client = client;
        //_indexName = GetIndexName();
        _indexName = "roadname-extv2";
        //_indexName = "roadname";
        //_indexName = "roadname-ext";
        //_configuration = configuration;
        _vnShapeService = vnShapeService;
        //_logService = logService;
    }

    public async Task<string> BulkAsyncv1(List<RoadNamePush> roadPushs)
    {
        try
        {
            //_logService.WriteLog($"BulkAsync Start", LogLevel.Info);
            List<RoadName> roads = new List<RoadName>();

            if (!roadPushs.Any())
                return "Success - No data to bulk insert";

            roadPushs.ForEach(item => roads.Add(new RoadName(item)));

            //Check xem khởi tạo index chưa, nếu chưa khởi tạo thì phải khởi tạo index mới được
            await CreateIndex(_indexName);

            var bulkAllObservable = _client.BulkAll(roads, b => b
                .Index(_indexName)
                // how long to wait between retries
                .BackOffTime("30s")
                // how many retries are attempted if a failure occurs
                .BackOffRetries(2)
                // refresh the index once the bulk operation completes
                .RefreshOnCompleted()
                // how many concurrent bulk requests to make
                .MaxDegreeOfParallelism(Environment.ProcessorCount)
                // number of items per bulk request
                .Size(10000)
                // decide if a document should be retried in the event of a failure
                //.RetryDocumentPredicate((item, road) =>
                //{
                //    return item.Error.Index == "even-index" && person.FirstName == "Martijn";
                //})
                // if a document cannot be indexed this delegate is called
                .DroppedDocumentCallback(async (bulkResponseItem, road) =>
                {
                    bool isCreate = true;
                    //isCreate = await CreateAsync(road, _indexName);
                    isCreate = await IndexAsync(road, _indexName);
                    while (isCreate == false)
                    {
                        //isCreate = await IndexAsync(road, _indexName);
                        isCreate = await IndexAsync(road, _indexName);
                        //_logService.WriteLog($"BulkAsync Err, road: {road.RoadID} - {road.RoadName}", LogLevel.Error);
                        //Console.OutputEncoding = Encoding.UTF8;
                        //Console.WriteLine($"{road.ProvinceID} - {road.RoadName}");
                    }
                })
                .Timeout(Environment.ProcessorCount)
                .ContinueAfterDroppedDocuments()
            )
            .Wait(TimeSpan.FromMinutes(15), next =>
            {
                // do something e.g. write number of pages to console
            });
            //_logService.WriteLog($"BulkAsync End", LogLevel.Info);
            return "Success";
        }
        catch (Exception ex)
        {
            //_logService.WriteLog($"BulkAsync - {ex.ToString()}", LogLevel.Error);
            return "Bulk False";
        }
    }

    public async Task<string> BulkAsync(List<RoadNamePush> roadPushs)
    {
        try
        {
            //_logService.WriteLog($"BulkAsync Start", LogLevel.Info);
            List<RoadNameV2> roads = new List<RoadNameV2>();

            if (!roadPushs.Any())
                return "Success - No data to bulk insert";
            roadPushs.ForEach(item => roads.Add(new RoadNameV2(item)));

            //Check xem khởi tạo index chưa, nếu chưa khởi tạo thì phải khởi tạo index mới được
            await CreateIndex(_indexName);

            var bulkAllObservable = _client.BulkAll(roads, b => b
                .Index(_indexName)
                // how long to wait between retries
                .BackOffTime("30s")
                // how many retries are attempted if a failure occurs
                .BackOffRetries(2)
                // refresh the index once the bulk operation completes
                .RefreshOnCompleted()
                // how many concurrent bulk requests to make
                .MaxDegreeOfParallelism(Environment.ProcessorCount)
                // number of items per bulk request
                .Size(10000)
                // decide if a document should be retried in the event of a failure
                //.RetryDocumentPredicate((item, road) =>
                //{
                //    return item.Error.Index == "even-index" && person.FirstName == "Martijn";
                //})
                // if a document cannot be indexed this delegate is called
                .DroppedDocumentCallback(async (bulkResponseItem, road) =>
                {
                    bool isCreate = true;
                    //isCreate = await CreateAsync(road, _indexName);
                    isCreate = await IndexAsyncV2(road, _indexName);
                    while (isCreate == false)
                    {
                        //isCreate = await IndexAsync(road, _indexName);
                        isCreate = await IndexAsyncV2(road, _indexName);
                        //_logService.WriteLog($"BulkAsync Err, road: {road.RoadID} - {road.RoadName}", LogLevel.Error);
                        //Console.OutputEncoding = Encoding.UTF8;
                        //Console.WriteLine($"{road.ProvinceID} - {road.RoadName}");
                    }
                })
                .Timeout(Environment.ProcessorCount)
                .ContinueAfterDroppedDocuments()
            )
            .Wait(TimeSpan.FromMinutes(15), next =>
            {
                // do something e.g. write number of pages to console
            });
            //_logService.WriteLog($"BulkAsync End", LogLevel.Info);
            return "Success";
        }
        catch (Exception ex)
        {
            //_logService.WriteLog($"BulkAsync - {ex.ToString()}", LogLevel.Error);
            return "Bulk False";
        }
    }

    public async Task<string> BulkAsyncByProvince(List<RoadNameMerge> roadPushs, int shapeid, int provinceID)
    {
        try
        {
            string indexname = $"{_indexName}-{shapeid}-{provinceID}";
            //_logService.WriteLog($"BulkAsync Start", LogLevel.Info);
            //List<RoadNameV2> roads = new List<RoadNameV2>();

            if (!roadPushs.Any())
                return "Success - No data to bulk insert";
            //roadPushs.ForEach(item => roads.Add(new RoadNameV2(item)));

            //Check xem khởi tạo index chưa, nếu chưa khởi tạo thì phải khởi tạo index mới được
            await CreateIndexByProvince(indexname);

            var bulkAllObservable = _client.BulkAll(roadPushs, b => b
                .Index(indexname)
                // how long to wait between retries
                .BackOffTime("30s")
                // how many retries are attempted if a failure occurs
                .BackOffRetries(2)
                // refresh the index once the bulk operation completes
                .RefreshOnCompleted()
                // how many concurrent bulk requests to make
                .MaxDegreeOfParallelism(Environment.ProcessorCount)
                // number of items per bulk request
                .Size(10000)
                // decide if a document should be retried in the event of a failure
                //.RetryDocumentPredicate((item, road) =>
                //{
                //    return item.Error.Index == "even-index" && person.FirstName == "Martijn";
                //})
                // if a document cannot be indexed this delegate is called
                .DroppedDocumentCallback(async (bulkResponseItem, road) =>
                {
                    bool isCreate = true;
                    //isCreate = await CreateAsync(road, _indexName);
                    isCreate = await IndexAsyncByProvince(road, indexname);
                    while (isCreate == false)
                    {
                        //isCreate = await IndexAsync(road, _indexName);
                        isCreate = await IndexAsyncByProvince(road, indexname);
                        //_logService.WriteLog($"BulkAsync Err, road: {road.RoadID} - {road.RoadName}", LogLevel.Error);
                        //Console.OutputEncoding = Encoding.UTF8;
                        //Console.WriteLine($"{road.ProvinceID} - {road.RoadName}");
                    }
                })
                .Timeout(Environment.ProcessorCount)
                .ContinueAfterDroppedDocuments()
            )
            .Wait(TimeSpan.FromMinutes(15), next =>
            {
                // do something e.g. write number of pages to console
            });
            //_logService.WriteLog($"BulkAsync End", LogLevel.Info);
            return "Success";
        }
        catch (Exception ex)
        {
            //_logService.WriteLog($"BulkAsync - {ex.ToString()}", LogLevel.Error);
            return "Bulk False";
        }
    }

    // Tìm kiếm theo từ khóa
    public async Task<List<RoadNameOut>> GetDataSuggestion(double lat, double lng, string distance, int size, string keyword, int type)
    {
        try
        {
            List<QueryContainer> must = new List<QueryContainer>();
            List<QueryContainer> filter = new List<QueryContainer>();
            var queryContainerList = new List<QueryContainer>();
            var boolQuery = new BoolQuery();
            string? keywordAscii = string.Empty;

            if (!string.IsNullOrEmpty(keyword))
            {
              
                keyword = keyword.ToLower();

                keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

                queryContainerList.Add(
                    MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList.Add(
                  MatchQuerySuggestion(keyword, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));
            }

            if (lat > 0 && lng > 0)
            {
                int provinceID = 16;
                provinceID = await GetProvinceId(lat, lng, null);

                queryContainerList.Add(new MatchQuery()
                    {
                        Field = "provinceID",
                        Query = provinceID.ToString()
                    }
                );

                filter.Add(GeoDistanceQuerySuggestion("location", lat, lng, distance));
            }

            //boolQuery.IsStrict = true;
            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList;
            boolQuery.Filter = filter;

            var searchResponse = await _client.SearchAsync<RoadName>(s => s.Index(_indexName)
                .Size(size)
                //.MinScore(5.0)
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            List<RoadNameOut> result = new List<RoadNameOut>();

            if (!searchResponse.IsValid)
                return result;

            if (searchResponse.Documents.Any())
            {
                searchResponse.Documents.OrderBy(x => x.Priority).ToList().ForEach(item => result.Add(new RoadNameOut(item)));
                //_logService.WriteLog($"GetDataSuggestion End - keyword: {keyword}", LogLevel.Info);
                return result;
            }

            var queryContainerList2 = new List<QueryContainer>();

            queryContainerList2.Add(
                    MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

            queryContainerList2.Add(
                 MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));

            boolQuery = new BoolQuery();

            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList2;
            boolQuery.Filter = filter;

            var responseTwo = await _client.SearchAsync<RoadName>(s => s.Index(_indexName)
                .Size(size)
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            if (responseTwo.IsValid)
                responseTwo.Documents.OrderBy(x => x.Priority).ToList().ForEach(item => result.Add(new RoadNameOut(item)));

            return result;
        }
        catch
        { return new List<RoadNameOut>(); }
    }

    public async Task<List<RoadNameOut>> GetRoadNameByProvince(string keyword, short provinceId)
    {
        try
        {
            List<QueryContainer> must = new List<QueryContainer>();
            List<QueryContainer> filter = new List<QueryContainer>();
            var queryContainerList = new List<QueryContainer>();
            var boolQuery = new BoolQuery();
            string? keywordAscii = string.Empty;

            if (!string.IsNullOrEmpty(keyword))
            {

                keyword = keyword.ToLower();

                keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

                queryContainerList.Add(
                    MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList.Add(
                  MatchQuerySuggestion(keyword, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));
            }

            queryContainerList.Add(new MatchQuery()
            {
                Field = Infer.Field<RoadName>(f => f.ProvinceID),
                Query = provinceId.ToString()
            }
            );

            //boolQuery.IsStrict = true;
            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList;
            boolQuery.Filter = filter;

            var searchResponse = await _client.SearchAsync<RoadName>(s => s.Index(_indexName)
                .Size(5)//size
                           //.MinScore(5.0)
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            List<RoadNameOut> result = new List<RoadNameOut>();

            if (!searchResponse.IsValid)
                return result;

            if (searchResponse.Documents.Any())
            {
                searchResponse.Documents.OrderBy(x => x.Priority).ToList().ForEach(item => result.Add(new RoadNameOut(item)));
                //_logService.WriteLog($"GetDataSuggestion End - keyword: {keyword}", LogLevel.Info);
                return result;
            }

            var queryContainerList2 = new List<QueryContainer>();

            queryContainerList2.Add(
                    MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

            queryContainerList2.Add(
                 MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));

            queryContainerList2.Add(new MatchQuery()
            {
                Field = Infer.Field<RoadName>(f => f.ProvinceID),
                Query = provinceId.ToString()
            }
            );

            boolQuery = new BoolQuery();

            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList2;
            boolQuery.Filter = filter;

            var responseTwo = await _client.SearchAsync<RoadName>(s => s.Index(_indexName)
                .Size(5)//size
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            if (responseTwo.IsValid)
                responseTwo.Documents.OrderBy(x => x.Priority).ToList().ForEach(item => result.Add(new RoadNameOut(item)));

            return result;
        }
        catch
        { return new List<RoadNameOut>(); }
    }

    public async Task<List<RoadNameOut>> GetDataSuggestionv1(double lat, double lng, string distance, int size, string keyword)
    {
        try
        {
            if (string.IsNullOrEmpty(keyword))
                return new List<RoadNameOut>();

            List<QueryContainer> must = new List<QueryContainer>();
            List<QueryContainer> filter = new List<QueryContainer>();
            var queryContainerList = new List<QueryContainer>();
            var boolQuery = new BoolQuery();
            string? keywordAscii = string.Empty;

            if (!string.IsNullOrEmpty(keyword))
            {

                keyword = keyword.ToLower();

                keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

                queryContainerList.Add(
                    MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList.Add(
                  MatchQuerySuggestion(keyword, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));
            }

            if (lat > 0 && lng > 0)
            {
                int provinceID = 16;
                provinceID = await GetProvinceId(lat, lng, null);

                queryContainerList.Add(new MatchQuery()
                {
                    Field = "provinceID",
                    Query = provinceID.ToString()
                }
                );

                filter.Add(GeoDistanceQuerySuggestion("location", lat, lng, distance));
            }

            //boolQuery.IsStrict = true;
            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList;
            boolQuery.Filter = filter;

            var searchResponse = await _client.SearchAsync<RoadName>(s => s.Index(_indexName)
                .Size(size)
                //.MinScore(5.0)
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            List<RoadNameOut> result = new List<RoadNameOut>();

            if (!searchResponse.IsValid)
                return result;

            if (searchResponse.Documents.Any())
            {
                searchResponse.Documents.OrderBy(x => x.Priority).ToList().ForEach(item => result.Add(new RoadNameOut(item)));
                //_logService.WriteLog($"GetDataSuggestion End - keyword: {keyword}", LogLevel.Info);
                return result;
            }

            var queryContainerList2 = new List<QueryContainer>();

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

            queryContainerList2.Add(
                 MatchQuerySuggestion(keywordAscii, Infer.Field<RoadName>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));

            boolQuery = new BoolQuery();

            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList2;
            boolQuery.Filter = filter;

            var responseTwo = await _client.SearchAsync<RoadName>(s => s.Index(_indexName)
                .Size(size)
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            if (responseTwo.IsValid)
                responseTwo.Documents.OrderBy(x => x.Priority).ToList().ForEach(item => result.Add(new RoadNameOut(item)));

            return result;
        }
        catch
        { return new List<RoadNameOut>(); }
    }

    public async Task<List<RoadNameOut>> GetDataSuggestion(double lat, double lng, string distance, int size, string keyword)
    {
        try
        {
            if (string.IsNullOrEmpty(keyword))
                return new List<RoadNameOut>();

            List<QueryContainer> must = new List<QueryContainer>();
            List<QueryContainer> filter = new List<QueryContainer>();
            var queryContainerList = new List<QueryContainer>();
            var boolQuery = new BoolQuery();
            string? keywordAscii = string.Empty;

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();

                keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

                queryContainerList.Add(
                    MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameV2>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameV2>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameV2>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameV2>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList.Add(
                  MatchQuerySuggestion(keyword, Infer.Field<RoadNameV2>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));
            }

            if (lat > 0 && lng > 0)
            {
                int provinceID = 16;
                provinceID = await GetProvinceId(lat, lng, null);

                queryContainerList.Add(new MatchQuery()
                {
                    //Field = "provinceID",
                    Field = Infer.Field<RoadNameV2>(f => f.ProvinceID),
                    Query = provinceID.ToString()
                }
                );

                filter.Add(GeoDistanceQuerySuggestion("location", lat, lng, distance));
            }

            //boolQuery.IsStrict = true;
            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList;
            boolQuery.Filter = filter;

            var searchResponse = await _client.SearchAsync<RoadNameV2>(s => s.Index(_indexName)
                .Size(size)
                //.MinScore(5.0)
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            List<RoadNameOut> result = new List<RoadNameOut>();

            if (!searchResponse.IsValid)
                return result;

            if (searchResponse.Documents.Any())
            {
                searchResponse.Documents.OrderBy(x => x.TypeArea).ToList().ForEach(item => result.Add(new RoadNameOut(item)));
                //_logService.WriteLog($"GetDataSuggestion End - keyword: {keyword}", LogLevel.Info);
                return result;
            }

            var queryContainerList2 = new List<QueryContainer>();

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameV2>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameV2>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameV2>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameV2>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

            queryContainerList2.Add(
                 MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameV2>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));

            boolQuery = new BoolQuery();

            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList2;
            boolQuery.Filter = filter;

            var responseTwo = await _client.SearchAsync<RoadNameV2>(s => s.Index(_indexName)
                .Size(size)
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            if (responseTwo.IsValid)
                responseTwo.Documents.OrderBy(x => x.TypeArea).ToList().ForEach(item => result.Add(new RoadNameOut(item)));

            return result;
        }
        catch
        { return new List<RoadNameOut>(); }
    }

    public async Task<List<RoadNameMerge>> GetDataSuggestionByProvince(double lat, double lng, string distance, int size, string keyword)
    {
        try
        {
            List<RoadNameMerge> result = new List<RoadNameMerge>();

            if (string.IsNullOrEmpty(keyword))
                return result;

            List<QueryContainer> must = new List<QueryContainer>();
            List<QueryContainer> filter = new List<QueryContainer>();
            var queryContainerList = new List<QueryContainer>();
            var boolQuery = new BoolQuery();
            string? keywordAscii = string.Empty;

            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    keyword = keyword.ToLower();

            //    keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

            //    queryContainerList.Add(
            //        MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

            //    //queryContainerList.Add(
            //    //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

            //    //queryContainerList.Add(
            //    //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

            //    //queryContainerList.Add(
            //    //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

            //    queryContainerList.Add(
            //      MatchQuerySuggestion(keyword, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));
            //}


            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    keyword = keyword.ToLower();

            //    keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

            //    queryContainerList.Add(
            //        MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

            //    queryContainerList.Add(
            //       MatchQuerySuggestion(keyword, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer3"));
            //}

            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    keyword = keyword.ToLower();

            //    keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

            //    queryContainerList.Add(
            //        MatchQuerySuggestion(keyword, Infer.Field<RoadNameMerge>(f => f.name), Fuzziness.Auto, "vn_analyzer3"));
            //}

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();

                keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

                queryContainerList.Add(
                    MatchQuerySuggestion(keyword, Infer.Field<RoadNameMerge>(f => f.name), Fuzziness.Auto, "vn_analyzer3"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.nameAscii), Fuzziness.EditDistance(3), "vn_analyzer"));
            }

            int provinceID = 16;
            provinceID = await GetProvinceId(lat, lng, null);

            queryContainerList.Add(new MatchQuery()
                {
                    //Field = "provinceID",
                    Field = Infer.Field<RoadNameMerge>(f => f.ProvinceID),
                    Query = provinceID.ToString()
                }
            );

            if (lat > 0 && lng > 0)
                filter.Add(GeoDistanceQuerySuggestion("location", lat, lng, distance));

            //boolQuery.IsStrict = true;
            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList;
            boolQuery.Filter = filter;

            string indexName = $"{_indexName}-{provinceID}";

            var searchResponse = await _client.SearchAsync<RoadNameMerge>(s => s.Index(indexName)
                .Size(size)
                //.MinScore(5.0)
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            if (!searchResponse.IsValid)
                return result;

            if (searchResponse.Documents.Any())
            {
                //searchResponse.Documents.ToList().ToList().ForEach(item => result.Add(new RoadNameMergeDto(item)));
                //searchResponse.Documents.ToList().ToList().ForEach(item => result.Add(new RoadNameMergeDto(item)));
                return searchResponse.Documents.ToList(); ;
            }

            //{
            //    return searchResponse.Documents.ToList();
            //    //searchResponse.Documents.OrderBy(x => x.TypeArea).ToList().ForEach(item => result.Add(new RoadNameMerge(item)));
            //    //_logService.WriteLog($"GetDataSuggestion End - keyword: {keyword}", LogLevel.Info);
            //    //return result;
            //}

            return result;
        }
        catch
        { return new List<RoadNameMerge>(); }
    }

    public async Task<ResultMerge<object>> Search1(double lat, double lng, string distance, int size, string keyword)
    {
        try
        {
            if (string.IsNullOrEmpty(keyword))
                return ResultMerge<object>.Success("No data", "0", true);

            List<QueryContainer> must = new List<QueryContainer>();
            List<QueryContainer> filter = new List<QueryContainer>();
            var queryContainerList = new List<QueryContainer>();
            var boolQuery = new BoolQuery();
            string? keywordAscii = string.Empty;

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();

                keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

                queryContainerList.Add(
                    MatchQuerySuggestion(keyword, Infer.Field<RoadNameMerge>(f => f.name), Fuzziness.Auto, "vn_analyzer3"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.nameAscii), Fuzziness.EditDistance(3), "vn_analyzer"));
            }

            int provinceID = 16;
            provinceID = await GetProvinceId(lat, lng, null);

            queryContainerList.Add(new MatchQuery()
            {
                //Field = "provinceID",
                Field = Infer.Field<RoadNameMerge>(f => f.ProvinceID),
                Query = provinceID.ToString()
            }
            );

            if (lat > 0 && lng > 0)
                filter.Add(GeoDistanceQuerySuggestion("location", lat, lng, distance));

            //boolQuery.IsStrict = true;
            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList;
            boolQuery.Filter = filter;

            string indexName = $"{_indexName}-{provinceID}";

            List<RoadNameMergeDto> roads = new List<RoadNameMergeDto>();

            var searchResponse = await _client.SearchAsync<RoadNameMerge>(s => s.Index(indexName)
                .Size(size)
                //.MinScore(5.0)
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            if (!searchResponse.IsValid)
                return ResultMerge<object>.Error("No data", "0", true);

            if (searchResponse.Documents.Any())
            {
                searchResponse.Documents.ToList().ForEach(item => roads.Add(new RoadNameMergeDto(item)));
                //searchResponse.Documents.ToList().ToList().ForEach(item => result.Add(new RoadNameMergeDto(item)));
                //return searchResponse.Documents.ToList();
                return ResultMerge<object>.Success(roads, "0", true);
            }

            var queryContainerList2 = new List<QueryContainer>();

            queryContainerList2.Add(
                    MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.name), Fuzziness.Auto, "vn_analyzer3"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.nameAscii), Fuzziness.EditDistance(3), "vn_analyzer"));

            //int provinceID = 16;
            //provinceID = await GetProvinceId(lat, lng, null);

            queryContainerList2.Add(new MatchQuery()
            {
                //Field = "provinceID",
                Field = Infer.Field<RoadNameMerge>(f => f.ProvinceID),
                Query = provinceID.ToString()
            }
            );

            if (lat > 0 && lng > 0)
                filter.Add(GeoDistanceQuerySuggestion("location", lat, lng, distance));

            boolQuery = new BoolQuery();

            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList2;
            boolQuery.Filter = filter;

            var responseTwo = await _client.SearchAsync<RoadNameMerge>(s => s.Index(indexName)
                .Size(size)
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            if (responseTwo.IsValid)
                responseTwo.Documents.OrderBy(x => x.TypeArea).ToList().ForEach(item => roads.Add(new RoadNameMergeDto(item)));

            return ResultMerge<object>.Error(roads, "0", true);
        }
        catch(Exception ex)
        {
            return ResultMerge<object>.Error(ex, "1", false);
        }
    }

    public async Task<ResultMerge<object>> Search(double lat, double lng, string distance, int size, string keyword, int shapeid)
    {
        try
        {
            if (string.IsNullOrEmpty(keyword))
                return ResultMerge<object>.Success("No data", "0", true);

            List<QueryContainer> must = new List<QueryContainer>();
            List<QueryContainer> filter = new List<QueryContainer>();
            var queryContainerList = new List<QueryContainer>();
            var boolQuery = new BoolQuery();
            string? keywordAscii = string.Empty;

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();

                keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

                //queryContainerList.Add(
                //    MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                //queryContainerList.Add(
                //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                //queryContainerList.Add(
                //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                //queryContainerList.Add(
                //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                //queryContainerList.Add(
                //  MatchQuerySuggestion(keyword, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));


                // 2
                //queryContainerList.Add(
                //    MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                //queryContainerList.Add(
                //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                //queryContainerList.Add(
                //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                //queryContainerList.Add(
                //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                //queryContainerList.Add(
                //  MatchQuerySuggestion(keyword, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));

                // 3
                ////queryContainerList.Add(
                ////    MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.nameAscii), Fuzziness.Auto, "vn_analyzer3"));

                ////queryContainerList.Add(
                ////    MatchQuerySuggestion(keyword, Infer.Field<RoadNameMerge>(f => f.name), Fuzziness.Auto, "vn_analyzer3"));

                //queryContainerList.Add(
                //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                //queryContainerList.Add(
                //   MatchQuerySuggestion(keyword, Infer.Field<RoadNameMerge>(f => f.Keywords), Fuzziness.EditDistance(0), "vn_analyzer3"));

                //4
                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                queryContainerList.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList.Add(
                  MatchQuerySuggestion(keyword, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));
            }

            int provinceID = 0;
            //provinceID = await GetProvinceId(lat, lng, null);
            provinceID = await GetProvinceIdIndex(lat, lng, null);


            if (provinceID > 0)
            {
                queryContainerList.Add(new MatchQuery()
                {
                    //Field = "provinceID",
                    Field = Infer.Field<RoadNameMerge>(f => f.ProvinceID),
                    Query = provinceID.ToString()
                }
                );
            }


            if (shapeid > 0)
            {
                queryContainerList.Add(new MatchQuery()
                {
                    //Field = "provinceID",
                    Field = Infer.Field<RoadNameMerge>(f => f.shapeid),
                    Query = shapeid.ToString()
                });
            }

            if (lat > 0 && lng > 0)
                filter.Add(GeoDistanceQuerySuggestion("location", lat, lng, distance));

            //boolQuery.IsStrict = true;
            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList;
            boolQuery.Filter = filter;

            //string indexName = $"{_indexName}-{provinceID}";
            string indexName = $"{_indexName}-{shapeid}-{provinceID}";

            List<RoadNameMergeDto> roads = new List<RoadNameMergeDto>();

            var searchResponse = await _client.SearchAsync<RoadNameMerge>(s => s.Index(indexName)
                .Size(size)
                //.MinScore(5.0)
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            if (!searchResponse.IsValid)
                return ResultMerge<object>.Error("No data", "0", true);

            if (searchResponse.Documents.Any())
            {
                searchResponse.Documents.ToList().ForEach(item => roads.Add(new RoadNameMergeDto(item)));
                //searchResponse.Documents.ToList().ToList().ForEach(item => result.Add(new RoadNameMergeDto(item)));
                //return searchResponse.Documents.ToList();
                return ResultMerge<object>.Success(roads, "0", true);
                //return ResultMerge<object>.Success(searchResponse.Documents.ToList(), "0", true);
            }

            var queryContainerList2 = new List<QueryContainer>();

            //queryContainerList2.Add(
            //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

            //queryContainerList2.Add(
            //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

            //queryContainerList2.Add(
            //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

            //queryContainerList2.Add(
            //   MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

            //queryContainerList2.Add(
            //     MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

            queryContainerList2.Add(
               MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

            queryContainerList2.Add(
                 MatchQuerySuggestion(keywordAscii, Infer.Field<RoadNameMerge>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));

            //int provinceID = 16;
            //provinceID = await GetProvinceId(lat, lng, null);

            //queryContainerList2.Add(new MatchQuery()
            //{
            //    //Field = "provinceID",
            //    Field = Infer.Field<RoadNameMerge>(f => f.ProvinceID),
            //    Query = provinceID.ToString()
            //}
            //);

            if (shapeid > 0)
            {
                queryContainerList2.Add(new MatchQuery()
                {
                    Field = Infer.Field<RoadNameMerge>(f => f.shapeid),
                    Query = shapeid.ToString()
                });
            }

           

            if (lat > 0 && lng > 0)
                filter.Add(GeoDistanceQuerySuggestion("location", lat, lng, distance));

            boolQuery = new BoolQuery();

            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList2;
            boolQuery.Filter = filter;

            var responseTwo = await _client.SearchAsync<RoadNameMerge>(s => s.Index(indexName)
                .Size(size)
                .Scroll(1)
                .Sort(s => s.Descending(SortSpecialField.Score))
                .Query(q => q
                    .Bool(b => b
                        .Must(boolQuery)
                    )
                )
            );

            if (responseTwo.IsValid)
                //responseTwo.Documents.OrderBy(x => x.TypeArea).ToList().ForEach(item => roads.Add(new RoadNameMergeDto(item)));
                responseTwo.Documents.ToList().ForEach(item => roads.Add(new RoadNameMergeDto(item)));

            return ResultMerge<object>.Error(roads, "0", true);
            //return ResultMerge<object>.Error(responseTwo.Documents.ToList(), "0", true);
        }
        catch (Exception ex)
        {
            return ResultMerge<object>.Error(ex, "1", false);
        }
    }
}
