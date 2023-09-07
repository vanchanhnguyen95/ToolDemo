using BAGeocoding.Api.Interfaces;
using BAGeocoding.Api.Models.PBD;
using BAGeocoding.Utility;
using Nest;
using System.ComponentModel;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BAGeocoding.Api.Services;

public class ProvinceService : IProvinceService
{
    private int NumberOfShards { get; set; } = 5;
    private int NumberOfReplicas { get; set; } = 1;
    private readonly ElasticClient _client;
    private readonly string _indexName;

    private string GetIndexName()
    {
        var type = typeof(Province);

        var customAttributes = (DescriptionAttribute[])type
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (customAttributes != null && customAttributes.Length > 0)
            return customAttributes[0].Description;

        throw new Exception($"{nameof(Province)} description attribute is missing.");
    }

    private async Task<bool> IndexAsync(Province item, string indexName = null)
    {
        try
        {
            if (string.IsNullOrEmpty(indexName)) indexName = _indexName;

            var response = await _client.IndexAsync(new Province(item), q => q.Index(indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.UpdateAsync<Province>(item.ProvinceID.ToString(), a => a.Index(indexName).Doc(new Province(item)));
            }
            return response.IsValid;
        }
        catch { return false; }

    }

    public ProvinceService(ElasticClient client, IConfiguration configuration)
    {
        _client = client;
        _indexName = GetIndexName();
    }

    public async Task<string> CreateIndex(string indexName)
    {
        try
        {
            if (string.IsNullOrEmpty(indexName)) indexName = _indexName;

            await _client.Indices.DeleteAsync(Indices.Index(indexName));

            var indexResponse = await _client.Indices.CreateAsync(Indices.Index(indexName), c => c
           .Map<Province>(mm => mm.AutoMap())
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

    public Task<string> AddAsync(List<Province> vietNamShapes)
    {
        throw new NotImplementedException();
    }

    public async Task<string> BulkAsync(List<Province> provinces)
    {
        try
        {
            //_logService.WriteLog($"BulkAsync Start", LogLevel.Info);
            //List<RoadNameV2> roads = new List<RoadNameV2>();

            if (!provinces.Any())
                return "Success - No data to bulk insert";
            //roadPushs.ForEach(item => roads.Add(new RoadNameV2(item)));

            //Check xem khởi tạo index chưa, nếu chưa khởi tạo thì phải khởi tạo index mới được
            await CreateIndex(_indexName);

            var bulkAllObservable = _client.BulkAll(provinces, b => b
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
                .DroppedDocumentCallback(async (bulkResponseItem, province) =>
                {
                    bool isCreate = true;
                    //isCreate = await CreateAsync(road, _indexName);
                    isCreate = await IndexAsync(province, _indexName);
                    while (isCreate == false)
                    {
                        //isCreate = await IndexAsync(road, _indexName);
                        isCreate = await IndexAsync(province, _indexName);
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

    public Task<List<Province>> GetDataSuggestion(int size, string keyword)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetProvinceId(string keyword)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetNameById(int provinceId)
    {
        try
        {
            List<QueryContainer> must = new List<QueryContainer>();
            List<QueryContainer> filter = new List<QueryContainer>();
            var queryContainerList = new List<QueryContainer>();
            var boolQuery = new BoolQuery();
            queryContainerList.Add(new MatchQuery()
                {
                    Field = Infer.Field<Province>(f => f.ProvinceID),
                    Query = provinceId.ToString()
                }
            );

            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList;
            boolQuery.Filter = filter;

            var searchResponse = await _client.SearchAsync<Province>(s => s.Index(_indexName)
                .Size(3)//size
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
                return string.Empty;

            if (searchResponse.Documents.Any())
                return searchResponse.Documents.Select(x => x?.VName??"").FirstOrDefault().ToString();

            return string.Empty;
        }
        catch (Exception ex)
        { return ex.ToString(); }
    }

    public async Task<Province> GetProvinceById(int provinceId)
    {
        try
        {
            List<QueryContainer> must = new List<QueryContainer>();
            List<QueryContainer> filter = new List<QueryContainer>();
            var queryContainerList = new List<QueryContainer>();
            var boolQuery = new BoolQuery();
            queryContainerList.Add(new MatchQuery()
            {
                Field = Infer.Field<Province>(f => f.ProvinceID),
                Query = provinceId.ToString()
            }
            );

            boolQuery.Boost = 1.1;
            boolQuery.Must = queryContainerList;
            boolQuery.Filter = filter;

            var searchResponse = await _client.SearchAsync<Province>(s => s.Index(_indexName)
                .Size(1)//size
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
                return new Province();

            if (searchResponse.Documents.Any())
                return searchResponse.Documents.FirstOrDefault();

            return new Province();
        }
        catch (Exception ex)
        { return new Province(); }
    }
}
