using BAGeocoding.Api.Dto;
using BAGeocoding.Api.Interfaces;
using BAGeocoding.Api.Models;
using BAGeocoding.Api.Models.PBD;
using BAGeocoding.Bll;
using BAGeocoding.Entity.Enum;
using BAGeocoding.Entity.Public;
using BAGeocoding.Utility;
using Microsoft.Extensions.FileSystemGlobbing;
using Nest;
using NetTopologySuite.Operation.Distance;
using System;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static System.Net.Mime.MediaTypeNames;

namespace BAGeocoding.Api.Services
{
    public class RoadElasticService : IRoadElasticService
    {
        private double DISTANCE_DEFAULT = 300000;

        private int NumberOfShards { get; set; } = 5;
        private int NumberOfReplicas { get; set; } = 1;
        private readonly ElasticClient _client;
        private readonly string _indexName;
        private readonly string _indexByProvince;
        private const string INIT_DATA_SUCCESS = "1";//Đã khởi tạo dữ liệu
        private const string INIT_DATA_FAIL = "2";//Chưa khởi tạo xong dữ liệu
        private const string ANO_DATA_FAIL = "3";//Lỗi khác

        private readonly IVietNamShapeService _vnShapeService;
        private readonly IRegionService _regionService;

        private string GetIndexName()
        {
            var type = typeof(BGCElasticRequestCreate);

            var customAttributes = (DescriptionAttribute[])type
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (customAttributes != null && customAttributes.Length > 0)
                return customAttributes[0].Description;

            throw new Exception($"{nameof(BGCElasticRequestCreate)} description attribute is missing.");
        }

        public RoadElasticService (ElasticClient client, IVietNamShapeService vnShapeService, IRegionService regionService)
        {
            _client = client;
            _indexName = GetIndexName();
            //_indexName = "roadname-extv2";
            _vnShapeService = vnShapeService;
            _regionService = regionService;
        }

        private async Task<bool> IndexAsyncByProvince(BGCElasticRequestCreate item, string indexName = null)
        {
            try
            {
                if (string.IsNullOrEmpty(indexName)) indexName = _indexName;

                var response = await _client.IndexAsync(new BGCElasticRequestCreate(item), q => q.Index(indexName));
                if (response.ApiCall?.HttpStatusCode == 409)
                {
                    await _client.UpdateAsync<BGCElasticRequestCreate>(item.provinceid.ToString(), a => a.Index(indexName).Doc(new BGCElasticRequestCreate(item)));
                }
                return response.IsValid;
            }
            catch { return false; }

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

        private GeoDistanceQuery GeoDistanceQuerySuggestionV2(string field, double lat, double lng,double distance)
        {
            return new GeoDistanceQuery
            {
                Boost = 2.0,
                Name = "named_query",
                DistanceType = GeoDistanceType.Plane,
                Field = field,
                Location = new GeoLocation(lat, lng),
                Distance = new Distance(distance, DistanceUnit.Meters),
                ValidationMethod = GeoValidationMethod.IgnoreMalformed
            };
        }

        private BuildingInfo GetBuildInfo(string indexName, string keywordAscii, string analyzer = "vn_analyzer3")
        {
            BuildingInfo buildingInfo = new BuildingInfo();

            try
            {
                var keyAnalyzer = _client.Indices.Analyze(a => a
               .Index(indexName)
               .Analyzer(analyzer)
               .Text(keywordAscii));
                //AnalyzeToken.Type
                short index = 0;

                var keyArray = keywordAscii.Split(' ');

                //buildingInfo.count = keyAnalyzer.Tokens.Count();
                buildingInfo.count = keyArray.Count();

                foreach (var token in keyAnalyzer.Tokens)
                {
                    index++;
                    if (token.Type == "<NUMBER>" || token.Type == "<NUM>")
                    {
                        buildingInfo.building = Convert.ToInt32(token.Token.ToString());
                        if (buildingInfo.building > 39999)
                            buildingInfo.building = 0;

                        buildingInfo.index = index;
                        break;
                    }
                }
            }
            catch
            {
                return new BuildingInfo() { building = 0, index = 0 };
            }
            
            return buildingInfo;
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

        public async Task<string> BulkAsyncByProvinceNoShape(List<BGCElasticRequestCreate> roadPushs, int provinceID)
        {
            try
            {
                string indexname = $"{_indexName}-{provinceID}";
                //_logService.WriteLog($"BulkAsync Start", LogLevel.Info);
                //List<RoadNameV2> roads = new List<RoadNameV2>();

                int sizeBulk = roadPushs.Count > 10000 ? 10000 : roadPushs.Count;

                if (!roadPushs.Any())
                    return "Success - No data to bulk insert";
                //roadPushs.ForEach(item => roads.Add(new RoadNameV2(item)));

                //Check xem khởi tạo index chưa, nếu chưa khởi tạo thì phải khởi tạo index mới được
                await CreateIndexByProvince(indexname);

                var bulkAllObservable = _client.BulkAll(roadPushs, b => b
                    .Index(indexname)
                    // refresh the index once the bulk operation completes 
                    .RefreshOnCompleted()
                    // how long to wait between retries
                    .RefreshIndices(indexname)
                    .BackOffRetries(5)
                    .BackOffTime("30s")
                    // how many retries are attempted if a failure occurs
                    //.BackOffRetries(2)              
                    // how many concurrent bulk requests to make
                    .MaxDegreeOfParallelism(Environment.ProcessorCount)
                    // number of items per bulk request
                    .Size(sizeBulk)
                    //.RefreshIndices(indexname)
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
                        await _client.Indices.RefreshAsync(indexname);
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

        public async Task<string> BulkAsyncByProvince(List<BGCElasticRequestCreate> roadPushs, int shapeid, int provinceID)
        {
            try
            {
                string indexname = $"{_indexName}-{shapeid}-{provinceID}";
                //_logService.WriteLog($"BulkAsync Start", LogLevel.Info);
                //List<RoadNameV2> roads = new List<RoadNameV2>();

                int sizeBulk = roadPushs.Count > 10000 ? 10000 : roadPushs.Count;

                if (!roadPushs.Any())
                    return "Success - No data to bulk insert";
                //roadPushs.ForEach(item => roads.Add(new RoadNameV2(item)));

                //Check xem khởi tạo index chưa, nếu chưa khởi tạo thì phải khởi tạo index mới được
                await CreateIndexByProvince(indexname);


                var bulkAllObservable = _client.BulkAll(roadPushs, b => b
                .Index(indexname)
                // refresh the index once the bulk operation completes 
                .RefreshOnCompleted()
                // how long to wait between retries
                .RefreshIndices(indexname)
                .BackOffRetries(2)
                .BackOffTime("30s")
                .MaxDegreeOfParallelism(Environment.ProcessorCount)
                // number of items per bulk request
                .Size(sizeBulk)
                //.BufferToBulk((descriptor, buffer) =>
                //{
                //    foreach (var person in buffer)
                //    {
                //        descriptor.Index<Person>(bi => bi
                //            .Index(person.Id % 2 == 0 ? "even-index" : "odd-index")
                //            .Document(person)
                //        );
                //    }
                //})
                //.RetryDocumentPredicate((bulkResponseItem, person) =>
                //{
                //    return bulkResponseItem.Error.Index == "even-index" && person.FirstName == "Martijn";
                //})
                .DroppedDocumentCallback( async (bulkResponseItem, road) =>
                {
                    //Console.WriteLine($"Unable to index: {bulkResponseItem} {person}");
                    bool isCreate = true;
                    //isCreate = await CreateAsync(road, _indexName);
                    isCreate = await IndexAsyncByProvince(road, indexname);
                    await _client.Indices.RefreshAsync(indexname);
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
                );

                var waitHandle = new ManualResetEvent(false);
                ExceptionDispatchInfo exceptionDispatchInfo = null;

                var observer = new BulkAllObserver(
                    onNext: response =>
                    {
                        // do something e.g. write number of pages to console
                    },
                    onError: exception =>
                    {
                        exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
                        waitHandle.Set();
                    },
                    onCompleted: () => waitHandle.Set());

                bulkAllObservable.Subscribe(observer);

                waitHandle.WaitOne();

                exceptionDispatchInfo?.Throw();
                if (exceptionDispatchInfo != null)
                {
                    return exceptionDispatchInfo.ToString()??"";
                }


                //var bulkAllObservable = _client.BulkAll(roadPushs, b => b
                //    .Index(indexname)
                //    // refresh the index once the bulk operation completes 
                //    .RefreshOnCompleted()
                //    // how long to wait between retries
                //    .RefreshIndices(indexname)
                //    .BackOffRetries(5)
                //    .BackOffTime("30s")
                //    // how many retries are attempted if a failure occurs
                //    //.BackOffRetries(2)              
                //    // how many concurrent bulk requests to make
                //    .MaxDegreeOfParallelism(Environment.ProcessorCount)
                //    // number of items per bulk request
                //    .Size(sizeBulk)
                //    //.RefreshIndices(indexname)
                //    // decide if a document should be retried in the event of a failure
                //    //.RetryDocumentPredicate((item, road) =>
                //    //{
                //    //    return item.Error.Index == "even-index" && person.FirstName == "Martijn";
                //    //})
                //    // if a document cannot be indexed this delegate is called
                //    .DroppedDocumentCallback(async (bulkResponseItem, road) =>
                //    {
                //        bool isCreate = true;
                //        //isCreate = await CreateAsync(road, _indexName);
                //        isCreate = await IndexAsyncByProvince(road, indexname);
                //        await _client.Indices.RefreshAsync(indexname);
                //        while (isCreate == false)
                //        {
                //            //isCreate = await IndexAsync(road, _indexName);
                //            isCreate = await IndexAsyncByProvince(road, indexname);
                //            //_logService.WriteLog($"BulkAsync Err, road: {road.RoadID} - {road.RoadName}", LogLevel.Error);
                //            //Console.OutputEncoding = Encoding.UTF8;
                //            //Console.WriteLine($"{road.ProvinceID} - {road.RoadName}");
                //        }
                //    })
                //    .Timeout(Environment.ProcessorCount)
                //    .ContinueAfterDroppedDocuments()
                //)
                //.Wait(TimeSpan.FromMinutes(15), next =>
                //{
                //    // do something e.g. write number of pages to console
                //});
                ////_logService.WriteLog($"BulkAsync End", LogLevel.Info);


                return "Success";
            }
            catch (Exception ex)
            {
                //_logService.WriteLog($"BulkAsync - {ex.ToString()}", LogLevel.Error);
                return "Bulk False";
            }
        }

        public List<BGCElasticRequestCreate> GenerateBulkInsertData(int currentIndex, int batchSize)
        {
            var data = new List<BGCElasticRequestCreate>();
            for (int i = currentIndex; i < currentIndex + batchSize; i++)
            {
                var document = new BGCElasticRequestCreate
                {
                    indexid = i,
                    // Populate your document properties here
                };
                data.Add(document);
            }
            return data;
        }

        public async Task<string> BulkAsyncByProvince2(List<BGCElasticRequestCreate> roadPushs, int shapeid, int provinceID)
        {
            try
            {
                string indexname = $"{_indexName}-{shapeid}-{provinceID}";
                //_logService.WriteLog($"BulkAsync Start", LogLevel.Info);
                //List<RoadNameV2> roads = new List<RoadNameV2>();

                int sizeBulk = roadPushs.Count > 10000 ? 10000 : roadPushs.Count;

                if (!roadPushs.Any())
                    return "Success - No data to bulk insert";
                //roadPushs.ForEach(item => roads.Add(new RoadNameV2(item)));

                //Check xem khởi tạo index chưa, nếu chưa khởi tạo thì phải khởi tạo index mới được
                await CreateIndexByProvince(indexname);

                var bulkAllObservable = _client.BulkAll(roadPushs, b => b
                    .Index(indexname)
                    // refresh the index once the bulk operation completes 
                    .RefreshOnCompleted()
                    // how long to wait between retries
                    .RefreshIndices(indexname)
                    .BackOffRetries(5)
                    .BackOffTime("30s")
                    // how many retries are attempted if a failure occurs
                    //.BackOffRetries(2)              
                    // how many concurrent bulk requests to make
                    .MaxDegreeOfParallelism(Environment.ProcessorCount)
                    // number of items per bulk request
                    .Size(sizeBulk)
                    //.RefreshIndices(indexname)
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
                        await _client.Indices.RefreshAsync(indexname);
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

        public async Task<ResultMerge<object>> Search(double lat, double lng, string distance, int size, string keyword, int shapeid)
        {
            try
            {
                keyword = keyword.Trim();
                if (string.IsNullOrEmpty(keyword))
                    return ResultMerge<object>.Success("No data", "0", true);

                List<QueryContainer> must = new List<QueryContainer>();
                List<QueryContainer> filter = new List<QueryContainer>();
                var queryContainerList = new List<QueryContainer>();
                var boolQuery = new BoolQuery();
                string? keywordAscii = string.Empty;

                //provinceID = await GetProvinceId(lat, lng, null);
                int provinceID = 16;
                provinceID = await GetProvinceIdIndex(lat, lng, null);

                //string indexName = $"{_indexName}-{provinceID}";
                string indexName = $"{_indexName}-{shapeid}-{provinceID}";

                // Get building
                BuildingInfo buildInf = GetBuildInfo(indexName, keyword);

                if (buildInf.building > 0 && buildInf.index == 1)
                {
                    string wordOne = keyword.Substring(0, keyword.IndexOf(' '));
                    //keyword = keyword.Replace(buildInf.building.ToString(), "").Trim();
                    keyword = keyword.Replace(wordOne, "").Trim();
                }    
                    
                keyword = keyword.ToLower();
                keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

                if (!string.IsNullOrEmpty(keyword))
                {
                    queryContainerList.Add(
                       MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                    queryContainerList.Add(
                       MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                    queryContainerList.Add(
                       MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                    queryContainerList.Add(
                       MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                    queryContainerList.Add(
                      MatchQuerySuggestion(keyword, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));
                }

                if (provinceID > 0)
                {
                    queryContainerList.Add(new MatchQuery()
                    {
                        //Field = "provinceID",
                        Field = Infer.Field<BGCElasticRequestCreate>(f => f.provinceid),
                        Query = provinceID.ToString()
                    }
                    );
                }

                if (shapeid > 0)
                {
                    queryContainerList.Add(new MatchQuery()
                    {
                        //Field = "provinceID",
                        Field = Infer.Field<BGCElasticRequestCreate>(f => f.shapeid),
                        Query = shapeid.ToString()
                    });
                }

                if (lat > 0 && lng > 0)
                    filter.Add(GeoDistanceQuerySuggestion("location", lat, lng, distance));

                //boolQuery.IsStrict = true;
                boolQuery.Boost = 1.1;
                boolQuery.Must = queryContainerList;
                boolQuery.Filter = filter;

                List<BGCElasticRequestCreateDto> roads = new List<BGCElasticRequestCreateDto>();

                var searchResponse = await _client.SearchAsync<BGCElasticRequestCreate>(s => s.Index(indexName)
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
                    searchResponse.Documents.OrderByDescending(x => x.shapeid).ThenBy(x => x.TypeArea).ToList().ForEach(item => roads.Add(new BGCElasticRequestCreateDto(item, buildInf)));
                    return ResultMerge<object>.Success(roads, "0", true);
                    //searchResponse.Documents.ToList().ToList().ForEach(item => result.Add(new RoadNameMergeDto(item)));
                    //return searchResponse.Documents.ToList();
                    //return ResultMerge<object>.Success(searchResponse.Documents.ToList(), "0", true);
                }

                var queryContainerList2 = new List<QueryContainer>();

                queryContainerList2.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                queryContainerList2.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList2.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                queryContainerList2.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList2.Add(
                     MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));

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
                        Field = Infer.Field<BGCElasticRequestCreate>(f => f.shapeid),
                        Query = shapeid.ToString()
                    });
                }

                if (lat > 0 && lng > 0)
                    filter.Add(GeoDistanceQuerySuggestion("location", lat, lng, distance));

                boolQuery = new BoolQuery();

                boolQuery.Boost = 1.1;
                boolQuery.Must = queryContainerList2;
                boolQuery.Filter = filter;

                var responseTwo = await _client.SearchAsync<BGCElasticRequestCreate>(s => s.Index(indexName)
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
                    responseTwo.Documents.OrderByDescending(x => x.shapeid).ThenBy(x => x.TypeArea).ToList().ForEach(item => roads.Add(new BGCElasticRequestCreateDto(item, buildInf)));

                return ResultMerge<object>.Error(roads, "0", true);
                //return ResultMerge<object>.Error(responseTwo.Documents.ToList(), "0", true);
            }
            catch (Exception ex)
            {
                return ResultMerge<object>.Error(ex, "1", false);
            }
        }

        public async Task<ResultMerge<object>> Search3(double lat, double lng, string distance, int size, string keyword, int shapeid)
        {
            try
            {
                keyword = keyword.Trim();
                if (string.IsNullOrEmpty(keyword))
                    return ResultMerge<object>.Success("No data", "0", true);

                List<QueryContainer> must = new List<QueryContainer>();
                List<QueryContainer> filter = new List<QueryContainer>();
                var queryContainerList = new List<QueryContainer>();
                var boolQuery = new BoolQuery();
                string? keywordAscii = string.Empty;

                //provinceID = await GetProvinceId(lat, lng, null);
                int provinceID = 16;
                provinceID = await GetProvinceIdIndex(lat, lng, null);

                string indexName = $"{_indexName}-{0}";
                //string indexName = $"{_indexName}-{provinceID}";
                //string indexName = $"{_indexName}-{shapeid}-{provinceID}";

                // Get building
                BuildingInfo buildInf = GetBuildInfo(indexName, keyword);

                if (buildInf.building > 0 && buildInf.index == 1)
                {
                    string wordOne = keyword.Substring(0, keyword.IndexOf(' '));
                    //keyword = keyword.Replace(buildInf.building.ToString(), "").Trim();
                    keyword = keyword.Replace(wordOne, "").Trim();
                }

                keyword = keyword.ToLower();
                keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

                if (!string.IsNullOrEmpty(keyword))
                {
                    queryContainerList.Add(
                       MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                    queryContainerList.Add(
                       MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                    queryContainerList.Add(
                       MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                    queryContainerList.Add(
                       MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                    queryContainerList.Add(
                      MatchQuerySuggestion(keyword, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));
                }

                if (provinceID > 0)
                {
                    queryContainerList.Add(new MatchQuery()
                    {
                        //Field = "provinceID",
                        Field = Infer.Field<BGCElasticRequestCreate>(f => f.provinceid),
                        Query = provinceID.ToString()
                    }
                    );
                }

                if (shapeid > 2)
                    shapeid = 0;

                if (shapeid > 0)
                {
                    queryContainerList.Add(new MatchQuery()
                    {
                        //Field = "provinceID",
                        Field = Infer.Field<BGCElasticRequestCreate>(f => f.shapeid),
                        Query = shapeid.ToString()
                    });
                }

                if (lat > 0 && lng > 0)
                    filter.Add(GeoDistanceQuerySuggestion("location", lat, lng, distance));

                //boolQuery.IsStrict = true;
                boolQuery.Boost = 1.1;
                boolQuery.Must = queryContainerList;
                boolQuery.Filter = filter;

                List<BGCElasticRequestCreateDto> roads = new List<BGCElasticRequestCreateDto>();

                var searchResponse = await _client.SearchAsync<BGCElasticRequestCreate>(s => s.Index(indexName)
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
                    searchResponse.Documents.OrderByDescending(x => x.shapeid).ThenBy(x => x.TypeArea).ToList().ForEach(item => roads.Add(new BGCElasticRequestCreateDto(item, buildInf)));
                    return ResultMerge<object>.Success(roads, "0", true);
                    //searchResponse.Documents.ToList().ToList().ForEach(item => result.Add(new RoadNameMergeDto(item)));
                    //return searchResponse.Documents.ToList();
                    //return ResultMerge<object>.Success(searchResponse.Documents.ToList(), "0", true);
                }

                var queryContainerList2 = new List<QueryContainer>();

                queryContainerList2.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                queryContainerList2.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList2.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                queryContainerList2.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList2.Add(
                     MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));

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
                        Field = Infer.Field<BGCElasticRequestCreate>(f => f.shapeid),
                        Query = shapeid.ToString()
                    });
                }

                if (lat > 0 && lng > 0)
                    filter.Add(GeoDistanceQuerySuggestion("location", lat, lng, distance));

                boolQuery = new BoolQuery();

                boolQuery.Boost = 1.1;
                boolQuery.Must = queryContainerList2;
                boolQuery.Filter = filter;

                var responseTwo = await _client.SearchAsync<BGCElasticRequestCreate>(s => s.Index(indexName)
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
                    responseTwo.Documents.OrderByDescending(x => x.shapeid).ThenBy(x => x.TypeArea).ToList().ForEach(item => roads.Add(new BGCElasticRequestCreateDto(item, buildInf)));

                return ResultMerge<object>.Error(roads, "0", true);
                //return ResultMerge<object>.Error(responseTwo.Documents.ToList(), "0", true);
            }
            catch (Exception ex)
            {
                return ResultMerge<object>.Error(ex, "1", false);
            }
        }

        public async Task<ResultMerge<object>> SearchV4(double lat, double lng, double distance, int size, string keyword, int shapeid)
        {
            try
            {
                keyword = keyword.Trim();
                if (string.IsNullOrEmpty(keyword))
                    return ResultMerge<object>.Success("No data", "0", true);

                string[] keywordArray = keyword.Split(' '); // Tách các từ trong chuỗi

                string wordCheckLen = string.Join("", keywordArray);

                if (wordCheckLen.Length < 3)
                    return ResultMerge<object>.Success("No data", "2", false);//2: cần nhập đủ 3 ký tự mới chấp nhận

                // Không nhập khoảng cách, mặc định 300 Km
                //if (distance == 0) distance = 300000;
                distance = distance == 0 ? 300000 : distance;

                // Không nhập size mặc định 10
                //if (size == 0) size = 10;
                size = size == 0 ? 10 : size;
               
                // mặc định shapeid có 3 loại thôi: 0,1,2
                //if (shapeid > 2) shapeid = 0;
                shapeid = shapeid > 2 ? 0 : shapeid;

                //List<QueryContainer> must = new List<QueryContainer>();
                List<QueryContainer> filter = new List<QueryContainer>();
                var queryContainerList = new List<QueryContainer>();
                var boolQuery = new BoolQuery();
                string? keywordAscii = string.Empty;

                //provinceID = await GetProvinceId(lat, lng, null);
                //int provinceID = 16;
                //provinceID = await GetProvinceIdIndex(lat, lng, null);

                string indexName = $"{_indexName}-{0}";
                //string indexName = $"{_indexName}-{provinceID}";

                // Get building
                BuildingInfo buildInf = GetBuildInfo(indexName, keyword);

                if (buildInf.building > 0 && buildInf.index == 1 && buildInf.count > 1)
                {
                    string wordOne = keyword.Substring(0, keyword.IndexOf(' '));
                    //keyword = keyword.Replace(buildInf.building.ToString(), "").Trim();
                    keyword = keyword.Replace(wordOne, "").Trim();
                }

                keyword = keyword.ToLower();
                keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

                if (!string.IsNullOrEmpty(keyword))
                {
                    queryContainerList.Add(
                       MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                    queryContainerList.Add(
                       MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                    queryContainerList.Add(
                       MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                    queryContainerList.Add(
                       MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                    queryContainerList.Add(
                      MatchQuerySuggestion(keyword, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));
                }

                //if (provinceID > 0)
                //{
                //    queryContainerList.Add(new MatchQuery()
                //    {
                //        //Field = "provinceID",
                //        Field = Infer.Field<BGCElasticRequestCreate>(f => f.provinceid),
                //        Query = provinceID.ToString()
                //    }
                //    );
                //}

                if (shapeid > 0)
                {
                    queryContainerList.Add(new MatchQuery()
                    {
                        //Field = "provinceID",
                        Field = Infer.Field<BGCElasticRequestCreate>(f => f.shapeid),
                        Query = shapeid.ToString()
                    });
                }

                if (lat > 0 && lng > 0)
                    filter.Add(GeoDistanceQuerySuggestionV2("location", lat, lng, distance));

                //boolQuery.IsStrict = true;
                boolQuery.Boost = 1.1;
                boolQuery.Must = queryContainerList;
                boolQuery.Filter = filter;

                List<BGCElasticRequestCreateDto> roads = new List<BGCElasticRequestCreateDto>();

                var searchResponse = await _client.SearchAsync<BGCElasticRequestCreate>(s => s.Index(indexName)
                    .Size(size)
                    //.MinScore(5.0)
                    .Scroll(1)
                    .Sort(s => s.Descending(SortSpecialField.Score))
                    .Query(q => q
                        .Bool(b => b
                            .Must(boolQuery)
                        )
                    ).Timeout("1s")
                );

                if (!searchResponse.IsValid)
                    return ResultMerge<object>.Error("No data", "0", true);

                if (searchResponse.Documents.Any())
                {
                    searchResponse.Documents.OrderByDescending(x => x.shapeid).ThenBy(x => x.TypeArea).ToList().ForEach(item => roads.Add(new BGCElasticRequestCreateDto(item, buildInf)));
                    return ResultMerge<object>.Success(roads, "0", true);
                    //searchResponse.Documents.ToList().ToList().ForEach(item => result.Add(new RoadNameMergeDto(item)));
                    //return searchResponse.Documents.ToList();
                    //return ResultMerge<object>.Success(searchResponse.Documents.ToList(), "0", true);
                }

                var queryContainerList2 = new List<QueryContainer>();
                List<QueryContainer> filter2 = new List<QueryContainer>();

                queryContainerList2.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer3"));

                queryContainerList2.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAsciiNoExt), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList2.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.EditDistance(0), "vn_analyzer"));

                queryContainerList2.Add(
                   MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.Auto, "vn_analyzer"));

                queryContainerList2.Add(
                     MatchQuerySuggestion(keywordAscii, Infer.Field<BGCElasticRequestCreate>(f => f.KeywordsAscii), Fuzziness.EditDistance(1), "vn_analyzer"));

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
                        Field = Infer.Field<BGCElasticRequestCreate>(f => f.shapeid),
                        Query = shapeid.ToString()
                    });
                }

                if (lat > 0 && lng > 0)
                    //distance = DISTANCE_DEFAULT;
                    //filter2.Add(GeoDistanceQuerySuggestionV2("location", lat, lng, distance));
                    filter2.Add(GeoDistanceQuerySuggestionV2("location", lat, lng, DISTANCE_DEFAULT));

                BoolQuery boolQuery2 = new BoolQuery();

                boolQuery2.Boost = 1.1;
                boolQuery2.Must = queryContainerList2;
                boolQuery2.Filter = filter2;

                var responseTwo = await _client.SearchAsync<BGCElasticRequestCreate>(s => s.Index(indexName)
                    .Size(size)
                    //.Scroll(1)
                    .Sort(s => s.Descending(SortSpecialField.Score))
                    .Query(q => q
                        .Bool(b => b
                            .Must(boolQuery2)
                        )
                    ).Timeout("2s")
                );

                if (responseTwo.IsValid)
                    //responseTwo.Documents.OrderBy(x => x.TypeArea).ToList().ForEach(item => roads.Add(new RoadNameMergeDto(item)));
                    responseTwo.Documents.OrderByDescending(x => x.shapeid).ThenBy(x => x.TypeArea).ToList().ForEach(item => roads.Add(new BGCElasticRequestCreateDto(item, buildInf)));

                return ResultMerge<object>.Error(roads, "0", true);
                //return ResultMerge<object>.Error(responseTwo.Documents.ToList(), "0", true);
            }
            catch (Exception ex)
            {
                return ResultMerge<object>.Error(ex, "1", false);
            }
        }

        public async Task<ResultMerge<object>> Add2Geo(string? keyStr, string? lanStr)
        {
            try
            {
                List<BGCElasticRequestCreateDto> roads = new List<BGCElasticRequestCreateDto>();

                if (RunningParams.ProcessState != EnumProcessState.Success)
                {
                    MainProcessing.InitData();
                    return ResultMerge<object>.Error(roads, INIT_DATA_FAIL, true);
                    //return ResultMerge<object>.Error("Chưa khởi tạo xong dữ liệu", INIT_DATA_FAIL, false);
                    //return Result<object>.Error(INIT_DATA_FAIL, "Chưa khởi tạo xong dữ liệu", RunningParams.ProcessState.ToString());
                }

                var ret = await Task.Run(() => _regionService.GeoByAddress(keyStr ?? "", lanStr ?? ""));

                if (ret == null)
                {
                    //LogFile.WriteNoDataGeobyAddress($"GeoByAddress, [ key:{keyStr} ]");
                    return ResultMerge<object>.Error(roads, INIT_DATA_SUCCESS, true);
                    //return ResultMerge<object>.Error("Đã khởi tạo dữ liệu", INIT_DATA_SUCCESS, false);
                    //return Result<object>.Success(new PBLAddressResultV3(), INIT_DATA_SUCCESS, "Đã khởi tạo dữ liệu", RunningParams.ProcessState.ToString());
                }
                roads.Add(new BGCElasticRequestCreateDto(ret));
                var data = new PBLAddressResultV3(ret);
                return ResultMerge<object>.Error(roads, "0", true);
            }
            catch(Exception ex)
            {
                return ResultMerge<object>.Error(null, "3", true);
            }
               
            
            
        }
    }
}
