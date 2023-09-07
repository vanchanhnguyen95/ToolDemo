using AutoMapper;
using BAGeocoding.Api.Dto;
using BAGeocoding.Api.Interfaces;
using BAGeocoding.Api.Models;
using BAGeocoding.Api.Models.PBD;
using BAGeocoding.Bll;
using BAGeocoding.Entity.DataService;
using BAGeocoding.Entity.Enum;
using BAGeocoding.Entity.MapObj;
using BAGeocoding.Entity.Public;
using BAGeocoding.Utility;
using Nest;
using NetTopologySuite.Operation.Valid;
using OSGeo.GDAL;
using OSGeo.OGR;
using RTree.Engine.Entity;
using System.Collections;
using System.ComponentModel;
using System.Text;
using static BAGeocoding.Api.Models.PBD.RoadName;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BAGeocoding.Api.Services;

public class RegionService : IRegionService
{
    private readonly IMapper _mapper;
    private const string INIT_DATA_SUCCESS = "1";//Đã khởi tạo dữ liệu
    private const string INIT_DATA_FAIL = "2";//Chưa khởi tạo xong dữ liệu

    public int NumberOfShards { get; set; } = 5;
    public int NumberOfReplicas { get; set; } = 1;
    private readonly ElasticClient _client;
    private readonly string _indexProvince;
    private readonly string _indexDistrict;
    private IRoadNameService _roadNameService;

    public RegionService(IMapper mapper, ElasticClient client, IRoadNameService roadNameService)
    {
        _mapper = mapper;
        _client = client;
        _roadNameService = roadNameService;
    }

    private string GetIndexProvince()
    {
        var type = typeof(BAGProvinceSearchDto);

        var customAttributes = (DescriptionAttribute[])type
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (customAttributes != null && customAttributes.Length > 0)
            return customAttributes[0].Description;

        throw new Exception($"{nameof(BAGProvinceSearchDto)} description attribute is missing.");
    }

    private string GetIndexDistrict()
    {
        var type = typeof(BAGDistrictSearchDto);

        var customAttributes = (DescriptionAttribute[])type
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (customAttributes != null && customAttributes.Length > 0)
            return customAttributes[0].Description;

        throw new Exception($"{nameof(BAGDistrictSearchDto)} description attribute is missing.");
    }

    public async Task<string> CreateIndex(string indexName)
    {
        try
        {
            //if (string.IsNullOrEmpty(indexName)) indexName = _indexName;
            await _client.Indices.DeleteAsync(Indices.Index(indexName));

            var indexResponse = await _client.Indices.CreateAsync(Indices.Index(indexName), c => c
           .Map<ProvinceAnalysis>(mm => mm.AutoMap())
           .Settings(s => s
               .NumberOfReplicas(NumberOfReplicas)
               .NumberOfShards(NumberOfShards)
               .Analysis(a => a
                   .CharFilters(cf => cf
                       .Mapping("programming_language", mca => mca
                           .Mappings(new[]
                           {
                              "c# => csharp",
                              "C# => Csharp"
                           })
                       )
                     )
                   .CharFilters(cf => cf
                       .Mapping("province_name", mca => mca
                           .Mappings(new[]
                           {
                                    "hà nội => ha noi",
                                    "Hà Nội => Ha Noi"
                           })
                       )
                     )
                   .TokenFilters(tf => tf
                        .PatternCapture("pattern_capture", pt => new PatternCaptureTokenFilter
                        {
                            Patterns = new[] { @"\d+", @"\\\\" },
                            PreserveOriginal = true,
                        })
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
                           .CharFilters("html_strip")
                           .Tokenizer("vi_tokenizer")
                           .Filters("lowercase", "ascii_folding", "stop_filter")
                       )
                       .Custom("vi_analyzer2", ca => ca
                            .CharFilters("province_name")
                            .Tokenizer("vi_tokenizer")
                            .Filters("lowercase", "ascii_folding")
                        )
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
                       .Custom("number_analyzer", ca => ca
                           //.CharFilters("html_strip", "province_name")
                           .Tokenizer("standard")
                           .Filters("lowercase", "pattern_capture")
                       )
                   )
               )
            )
        );

            return indexResponse.ApiCall.HttpStatusCode.ToString() ?? "OK";
        }

        catch (Exception ex)
        {
            return ex.ToString();
        }

    }

    public async Task<string> BulkAsyncDistrict(List<BAGDistrictSearchDto> districts, string indexName)
    {
        try
        {
            if (!districts.Any())
                return "Success - No data to bulk insert";

            //Check xem khởi tạo index chưa, nếu chưa khởi tạo thì phải khởi tạo index mới được
            await CreateIndex(indexName);

            var bulkAllObservable = _client.BulkAll(districts, b => b
                .Index(indexName)
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
                .DroppedDocumentCallback(async (bulkResponseItem, item) =>
                {
                    bool isCreate = true;
                    //isCreate = await CreateAsync(road, _indexName);
                    isCreate = await IndexAsyncDistrict(item, indexName);
                    while (isCreate == false)
                    {
                        //isCreate = await IndexAsync(road, _indexName);
                        isCreate = await IndexAsyncDistrict(item, indexName);
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
        catch (Exception ex) { return ex.ToString(); };
    }

    private async Task<bool> IndexAsyncDistrict(BAGDistrictSearchDto item, string indexName)
    {
        try
        {
            //if (string.IsNullOrEmpty(indexName)) indexName = indexName;

            var response = await _client.IndexAsync(item, q => q.Index(indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.UpdateAsync<BAGDistrictSearchDto>(item.DistrictID.ToString(), a => a.Index(indexName).Doc(item));
            }
            return response.IsValid;
        }
        catch { return false; }

    }

    public async Task<string> BulkAsyncProvince(List<BAGProvinceSearchDto> provinces, string indexName)
    {
        try
        {
            if (!provinces.Any())
                return "Success - No data to bulk insert";

            //Check xem khởi tạo index chưa, nếu chưa khởi tạo thì phải khởi tạo index mới được
            await CreateIndex(indexName);

            var bulkAllObservable = _client.BulkAll(provinces, b => b
                .Index(indexName)
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
                .DroppedDocumentCallback(async (bulkResponseItem, item) =>
                {
                    bool isCreate = true;
                    //isCreate = await CreateAsync(road, _indexName);
                    isCreate = await IndexAsyncProvince(item, indexName);
                    while (isCreate == false)
                    {
                        //isCreate = await IndexAsync(road, _indexName);
                        isCreate = await IndexAsyncProvince(item, indexName);
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
        catch (Exception ex) { return ex.ToString(); };
    }

    private async Task<bool> IndexAsyncProvince(BAGProvinceSearchDto item, string indexName)
    {
        try
        {
            //if (string.IsNullOrEmpty(indexName)) indexName = indexName;

            var response = await _client.IndexAsync(item, q => q.Index(indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.UpdateAsync<BAGProvinceSearchDto>(item.ProvinceID.ToString(), a => a.Index(indexName).Doc(item));
            }
            return response.IsValid;
        }
        catch { return false; }

    }

    private short GetBuilding(string indexName, string keywordAscii, string analyzer = "vn_analyzer3")
    {
        short building = new short();
        var keyAnalyzer = _client.Indices.Analyze(a => a
           .Index(indexName)
           .Analyzer(analyzer)
           .Text(keywordAscii));
        //AnalyzeToken.Type
        foreach (var token in keyAnalyzer.Tokens)
        {
            if (token.Type == "<NUMBER>")
            {
                building = Convert.ToInt16(token.Token.ToString());
                break;
            }

        }
        return building;
    }

    private string[] GetArrayWordStand(string indexName, string keywordAscii, string analyzer = "vn_analyzer3")
    {
        string[] words = new string[] { "" };

        if (string.IsNullOrEmpty(keywordAscii))
            return words;

        var keyAnalyzer = _client.Indices.Analyze(a => a
           .Index(indexName)
           .Analyzer(analyzer)
           .Text(keywordAscii));
        //AnalyzeToken.Type
        //foreach (var token in keyAnalyzer.Tokens)
        //{
        //    Console.WriteLine(token.Token);
        //    if (token.Type == "<NUMBER>")
        //    {

        //    }

        //}

        string keySearch = string.Empty;

        if (!keyAnalyzer.IsValid)
            keySearch = keywordAscii;

        if (!keyAnalyzer.Tokens.Any())
            return words;


        StringBuilder wordsAp = new StringBuilder();

        foreach (var token in keyAnalyzer.Tokens)
        {
            //Console.WriteLine($"Token: {token.Token}");
            wordsAp.Append(token.Token);
            wordsAp.Append(" ");
        }

        //string[] words = new string[] { "" };

        words = wordsAp.ToString().Trim().Split(' ');

        return words;
    }

    /*Phân tích ra cụm từ đầu vào sẽ lấy ra được bao nhiêu từ
            1.Lấy ra số từ cần dùng đề đi tìm kiếm tên Tỉnh

            2.Chuẩn hóa lại cụm từ đầu vào (sau khi có tên tỉnh)
            3.1. Nếu tìm được Tên tỉnh:
                + Lấy ra ProvinceId: cần dùng cả Province Id để lấy huyện/quận trong tình
                    Dùng tên tình để remove trogn từ khóa chuẩn hóa
                + Remove tên tỉnh có trong từ khóa tìm kiếm ban đầu
            3.2. Nếu không tìm được Tên tỉnh:

    */

    private async Task<BAGSearchKey> GetBAGSearchKey(string indexName, string keyword, string analyzer = "vn_analyzer3")
    {
        // 1. Xây dựng từ khóa tìm kiếm
        BAGSearchKey searchKey = new BAGSearchKey();

        keyword = keyword.ToLower().Replace("tp.", "").Replace("q.", "").Replace("h.", "").Trim();

        string keywordAscii = string.Empty;
        
        if (!string.IsNullOrEmpty(keyword))
            keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

        // Get building
        short building = GetBuilding(indexName, keywordAscii);
        if (building > 0)
            keywordAscii = keywordAscii.Replace(building.ToString(), "").Trim();

        // GetArrayWordStand
        string[] words = GetArrayWordStand(indexName, keywordAscii, "vn_analyzer3");
        //string wordStand = string.Join(" ", words);

        string keySearchProvicne = string.Empty;
        // Lấy ra từ để đi tìm kiếm Tỉnh
        int cntWords = words.Count();

        if (cntWords < 4)
        {
            //keySearchProvicne = "ha noi";
            searchKey.IsValid = true;
            searchKey.Original = keyword;
            searchKey.Province = "ha noi";
            searchKey.District = "";
            searchKey.Building = building;
            searchKey.Road = keywordAscii;
            searchKey.Original = keyword.ToLower();
            searchKey.IsSpecial = (searchKey.Original.IndexOf(keywordAscii) < 0);
            return searchKey;
        }

        //return $" {words[lenWords - 4]} {words[lenWords - 3]} {words[lenWords - 2]} {words[lenWords - 1]}";
        keySearchProvicne = $" {words[cntWords - 3]} {words[cntWords - 2]} {words[cntWords - 1]}";

        // Lấy ra Province từ keySearchProvicne
        BAGProvinceSearchDto provicne = await GetIdByKeyProvince(keySearchProvicne);
        string ename = provicne?.EName?.ToLower() ?? "";
        ename = ename.Replace("tp", "").Replace(".", "").Trim();
        //searchKey.Province = ename;

        List<string> lstEname = ename.Split(' ').ToList();
        int cntEname = lstEname?.Count() ?? 0;

        /*
         Tìm được tên tỉnh
         Remove tên tỉnh đi, chuẩn hóa lại keysearch đi tìm tên đường của tỉnh
         */
        string keySearchRoad = string.Empty;
        if (cntEname == 4)
        {
            keySearchRoad = GetkeySearchDistrict(words, cntWords - 4);
        }
        else if (cntEname == 3)
        {
            keySearchRoad = GetkeySearchDistrict(words, cntWords - 3);
        }
        else if (cntEname == 2)
        {
            keySearchRoad = GetkeySearchDistrict(words, cntWords - 2);
        }

        //short pr = provicne.ProvinceID;
        /* Lấy tên đường dựa vào tỉnh */
        List<RoadNameOut> rod = await _roadNameService.GetRoadNameByProvince(keySearchRoad, provicne?.ProvinceID??0);
        string roadName1 = LatinToAscii.Latin2Ascii(rod[0]?.RoadName?.ToLower()??"");


        /*Nếu tên đường tồn tại thì tiến hành tìm theo cách chỉ phụ thuộc tên đường
         trả về key search luôn
         */

        if(!string.IsNullOrEmpty(roadName1))
            return new BAGSearchKey
            {
                Original = keyword,
                IsValid = true,
                IsSpecial = (keyword.IndexOf(roadName1) < 0),
                Building = building,
                District = "",
                Province = ename,
                Road = roadName1
            };

        /*
         Tìm được tên tỉnh
         Remove tên tỉnh đi, chuẩn hóa lại keysearch đi tìm huyện
         */
        // xóa các từ Tp. thành phố, tỉnh đi, xóa dấu chấm đi
        string keySearchDistrict = string.Empty;
        if (cntEname == 4)
        {
            keySearchDistrict = GetkeySearchDistrict(words, cntWords - 4);
        }
        else if (cntEname == 3)
        {
            keySearchDistrict = GetkeySearchDistrict(words, cntWords - 3);
        }
        else if (cntEname == 2)
        {
            keySearchDistrict = GetkeySearchDistrict(words, cntWords - 2);
        }
        
        // Lấy thông tin các huyện có trong tỉnh đó dựa trên ProvinceId
        string districtSearch = string.Empty;
        string district = string.Empty;

        if (!string.IsNullOrEmpty(keySearchDistrict))
        {
            BAGDistrictSearchDto dis = await GetIdByKeyDistrict(keySearchDistrict, provicne?.ProvinceID.ToString() ?? "");
            districtSearch = dis?.EName?.ToLower() ?? "";

            // xóa các từ quận huyện đi (nếu có)
            //if (districtSearch.Contains("tp."))
            //    districtSearch = districtSearch.Replace("tp.", "").Trim();
            //else
            //{
            //    districtSearch = districtSearch.Replace("tp.", "").Replace("q.", "").Replace("h.", "").Trim();
            //}
            districtSearch = districtSearch.Replace("tp.", "").Replace("q.", "").Replace("h.", "").Trim();
            if (keySearchDistrict.Contains(districtSearch))
                district = districtSearch;
            //searchKey.District = districtSearch;
        }

        string roadNameSearch = keySearchDistrict;
        if (keySearchDistrict.Contains(districtSearch))
            roadNameSearch = keySearchDistrict.Replace(districtSearch, "").Trim();

        //// Get building
        //short building = GetBuilding(indexName, roadNameSearch);
        //if (building > 0)
        //    roadNameSearch = roadNameSearch.Replace(building.ToString(), "").Trim();

        searchKey.Building = building;
        searchKey.District = district;
        //IsSpecial   true    bool
        searchKey.IsValid = true;
        //Original    "chu văn an"    string
        searchKey.Province = ename;
        searchKey.Road = roadNameSearch;
        searchKey.Original = keyword.ToLower();
        searchKey.IsSpecial = (searchKey.Original.IndexOf(roadNameSearch) < 0);

        return searchKey;
    }

    //private async Task<string> Sum(string indexName, string keyword, string analyzer = "vn_analyzer3")
    //{
    //    string keywordAscii = string.Empty;

    //    if (!string.IsNullOrEmpty(keyword))
    //        keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

    //    // 1. Xây dựng từ khóa tìm kiếm
    //    BAGSearchKey searchKey = new BAGSearchKey();
    //    searchKey.Original = keyword;

    //    // GetArrayWordStand
    //    string[] words = GetArrayWordStand(indexName, keywordAscii, "vn_analyzer3");
    //    string wordStand = string.Join(" ", words);

    //    string keySearchProvicne = string.Empty;
    //    // Lấy ra từ để đi tìm kiếm Tỉnh
    //    int cntWords = words.Count();

    //    //if (cntWords == 4)
    //    //{
    //    //    //return $" {words[lenWords - 3]} {words[lenWords - 2]} {words[lenWords - 1]}";
    //    //    keySearchProvicne = $" {words[cntWords - 2]} {words[cntWords - 1]}";
    //    //}    
    //    //else if (cntWords > 4)
    //    //{
    //    //    //return $" {words[lenWords - 4]} {words[lenWords - 3]} {words[lenWords - 2]} {words[lenWords - 1]}";
    //    //    keySearchProvicne = $" {words[cntWords - 3]} {words[cntWords - 2]} {words[cntWords - 1]}";
    //    //}
    //    //else
    //    //{
    //    //    keySearchProvicne = string.Join(" ", words);
    //    //}
    //    if (cntWords > 3)
    //    {
    //        //return $" {words[lenWords - 4]} {words[lenWords - 3]} {words[lenWords - 2]} {words[lenWords - 1]}";
    //        keySearchProvicne = $" {words[cntWords - 3]} {words[cntWords - 2]} {words[cntWords - 1]}";
    //    }
    //    else
    //    {
    //        keySearchProvicne = "ha noi";
    //    }

    //    // Lấy ra Province từ keySearchProvicne
    //    BAGProvinceSearchDto provicne =  await GetIdByKeyProvince(keySearchProvicne);
    //    string ename = provicne?.EName?.ToLower()??"";
    //    ename = ename.Replace("tp", "").Replace(".", "").Trim();
    //    searchKey.Province = ename;

    //    List<string> lstEname = ename.Split(' ').ToList();
    //    int cntEname = lstEname?.Count()??0;
    //    /*
    //     Tìm được tên tỉnh
    //     Remove tên tỉnh đi, chuẩn hóa lại keysearch đi tìm huyện
    //     */
    //    // xóa các từ Tp. thành phố, tỉnh đi, xóa dấu chấm đi
    //    string keySearchDistrict = string.Empty;
    //    if (cntEname == 4)
    //    {
    //        keySearchDistrict = GetkeySearchDistrict(words, cntWords - 4);
    //    }
    //    else if (cntEname == 3)
    //    {
    //        keySearchDistrict = GetkeySearchDistrict(words, cntWords - 3);
    //    }
    //    else if (cntEname == 2)
    //    {
    //        keySearchDistrict = GetkeySearchDistrict(words, cntWords - 2);
    //    }

    //    // Lấy thông tin các huyện có trong tỉnh đó dựa trên ProvinceId
    //    string districtSearch = string.Empty;

    //    if (!string.IsNullOrEmpty(keySearchDistrict))
    //    {
    //        BAGDistrictSearchDto dis = await GetIdByKeyDistrict(keySearchDistrict, provicne?.ProvinceID.ToString()??"");
    //        districtSearch = dis?.EName?.ToLower() ?? "";

    //        // xóa các từ quận huyện đi
    //        if(districtSearch.Contains("tp."))
    //            districtSearch = districtSearch.Replace("tp.", "").Trim();
    //        else
    //        {
    //            districtSearch = districtSearch.Replace("tp.", "").Replace("q.", "").Replace("h.", "").Trim();
    //        }
    //        //searchKey.District = districtSearch;
    //    }

    //    string roadNameSearch = keySearchDistrict;
    //    if (keySearchDistrict.Contains(districtSearch))
    //        roadNameSearch = keySearchDistrict.Replace(districtSearch, "").Trim();

    //    // Get building
    //    short building = GetBuilding(indexName, roadNameSearch);
    //    if (building > 0)
    //        roadNameSearch = roadNameSearch.Replace(building.ToString(), "").Trim();

    //    //string roadNameSearch = 

    //    /*
    //        Không tìm được tên tỉnh
    //        */




    //    return "";
    //}

    private string GetkeySearchDistrict(string[] words, int cntWords)
    {
        string keySearchDistrict = string.Empty;
        StringBuilder districtBd = new StringBuilder();
        for (short i = 0; i < cntWords; i++)
        {
            districtBd.Append(words[i]);
            districtBd.Append(" ");
        }
        keySearchDistrict = districtBd.ToString().Trim();

        return keySearchDistrict;
    }

    private async Task<BAGProvinceSearchDto> GetIdByKeyProvince(string keySearchProvicne)
    {
        var result = await _client.SearchAsync<BAGProvinceSearchDto>(s => s.Index(GetIndexProvince())
                .Size(1)
                .Query(q => q.Bool(
                    b => b.Must(mu =>
                     mu.Match(ma =>
                        ma.Field(f => f.EName).Name("named_query").Analyzer("vn_analyzer3")
                        .Query(keySearchProvicne).Fuzziness(Fuzziness.EditDistance(0))
                        .AutoGenerateSynonymsPhraseQuery()
                    )
                    )
                 //.MinimumShouldMatch(98)
                 )
                )
               //.MinScore(5.0)
               .Sort(s => s.Descending(SortSpecialField.Score))
               .Scroll(1));
        if (result.Documents.Any())
            return result.Documents.ToList().FirstOrDefault()?? new BAGProvinceSearchDto();

        return new BAGProvinceSearchDto();
    }

    private async Task<BAGDistrictSearchDto> GetIdByKeyDistrict(string keySearchDistrict, string provinceId)
    {
        var result = await _client.SearchAsync<BAGDistrictSearchDto>(s => s.Index(GetIndexDistrict())
                .Size(1)
                .Query(q => q.Bool(
                    b => b.Must(mu =>
                     mu.Match(ma =>
                        ma.Field(f => f.EName).Name("named_query").Analyzer("vn_analyzer3")
                        .Query(keySearchDistrict).Fuzziness(Fuzziness.EditDistance(0))
                        .AutoGenerateSynonymsPhraseQuery()
                    )
                     //&& mu.Match( ma =>
                     //   ma.Field(f => f.ProvinceID).Query(provinceId)
                     //   )
                    )
                 //.MinimumShouldMatch(98)
                 )
                )
               //.MinScore(5.0)
               .Sort(s => s.Descending(SortSpecialField.Score))
               .Scroll(1));
        if (result.Documents.Any())
            return result.Documents.ToList().FirstOrDefault() ?? new BAGDistrictSearchDto();

        return new BAGDistrictSearchDto();
    }

    private async Task<string> AnalysisKeySearch(string indexName, string keywordAscii, string analyzer = "vn_analyzer3")
    {

         //string abc = await Sum(indexName, keywordAscii, "vn_analyzer3");
        // GetArrayWordStand
        string[] words = GetArrayWordStand(indexName, keywordAscii, "vn_analyzer3");

        int lenWords = words.Count();

        if (lenWords == 4)
            //return $" {words[lenWords - 3]} {words[lenWords - 2]} {words[lenWords - 1]}";
            return $" {words[lenWords - 2]} {words[lenWords - 1]}";

        if (lenWords > 4)
            //return $" {words[lenWords - 4]} {words[lenWords - 3]} {words[lenWords - 2]} {words[lenWords - 1]}";
            return $" {words[lenWords - 3]} {words[lenWords - 2]} {words[lenWords - 1]}";

        return string.Join(" ", words);
        //return abc;
    }

    //private async Task<List<BAGProvinceSearchDto>> GetDataByKeyWord(int size, string keyword, string indexName)
    //{
    //    try
    //    {
    //        string keywordAscii = string.Empty;

    //        if (!string.IsNullOrEmpty(keyword))
    //            keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

    //        string key = await GetKeySearch(keywordAscii);

    //        var result = await _client.SearchAsync<BAGProvinceSearchDto>(s => s.Index(indexName)
    //            .Size(size)
    //            .Query(q => q.Bool(
    //                b => b.Must(mu =>
    //                 mu.Match(ma =>
    //                    ma.Field(f => f.EName).Name("named_query").Analyzer("vn_analyzer3").Query(key).Fuzziness(Fuzziness.EditDistance(0))
    //                    .AutoGenerateSynonymsPhraseQuery()
    //                )
    //                //mu.Match(ma =>
    //                //ma.Field(f => f.ENameLower).Name("named_query").Analyzer("vn_analyzer3").Query(keywordAscii).Fuzziness(Fuzziness.Auto)
    //                //.AutoGenerateSynonymsPhraseQuery()
    //                //)
    //                //&&
    //                //mu.Match(ma =>
    //                //ma.Field(f => f.EName).Name("named_query").Analyzer("vn_analyzer").Query(keywordAscii).Fuzziness(Fuzziness.EditDistance(0))
    //                //.AutoGenerateSynonymsPhraseQuery()
    //                //)
    //                //&& mu.Match(ma =>
    //                //ma.Field(f => f.VName).Name("named_query").Analyzer("vn_analyzer").Query(keyword).Fuzziness(Fuzziness.EditDistance(1))
    //                //.AutoGenerateSynonymsPhraseQuery()
    //                //)

    //                )
    //             //.MinimumShouldMatch(98)

    //             )
    //            )
    //           //.MinScore(5.0)
    //           .Sort(s => s.Descending(SortSpecialField.Score))
    //           .Scroll(1));

    //        return result.Documents.ToList();
    //    }
    //    catch
    //    {
    //        return new List<BAGProvinceSearchDto>();
    //    }
    //}

    /// <summary>
    /// Tìm kiếm tọa độ theo địa chỉ V2
    /// </summary>
    public async Task<RPBLAddressResultV2> GeoByAddress(string keyStr, string lanStr)
    {
        try
        {
            // 1. Xây dựng từ khóa tìm kiếm
            BAGSearchKey keySearch = await GetBAGSearchKey(GetIndexProvince(), keyStr.Trim());

            //BAGSearchKey keySearch = new BAGSearchKey(2, keyStr.Trim(), LatinToAscii.Latin2Ascii(keyStr.Trim().ToLower()));
            if (keySearch.IsValid == false)
                return null;
            else if (keySearch.IsSpecial == true)
                keySearch.IsSpecial = RunningParams.RoadSpecial.ContainsKey(keySearch.Road);

            // 2. Tìm kiếm vùng
            // 2.1 Tìm kiếm tỉnh
            short provinceID = BAGDecoding.SearchProvinceByName(keySearch.Province);
            if (provinceID < 0)
                return null;
            // 2.2 Tìm kiếm quận/huyện
            short districtID = BAGDecoding.SearchDistrictByName(keySearch.District, (byte)provinceID);

            // 3. Tìm kiếm đường
            // 3.1 Lấy các thông tin
            DTSSegment gsSegment = (DTSSegment)RunningParams.ProvinceData.Segm[provinceID];
            EnumBAGLanguage language = (lanStr == "vn") ? EnumBAGLanguage.Vn : EnumBAGLanguage.En;
            // 3.2 Trả về kết quả
            //if (provinceID == 15 || provinceID == 16 || provinceID == 17 || provinceID == 19 || provinceID == 26)
            if (RunningParams.HTProvincePriority.ContainsKey(provinceID) == true)
                return GeoByAddressHaNoi(keySearch, gsSegment, districtID, language);
            else
                return GeoByAddress(keySearch, gsSegment, districtID, language);
        }
        catch (Exception ex)
        {
            LogFile.WriteError(string.Format("MainProcessing.GeoByAddress({0}, {1}), ex: {2}", keyStr, lanStr, ex.ToString()));
            return null;
        }
    }

    /// <summary>
    /// Tìm kiếm tọa độ qua địa chỉ V2
    /// </summary>
    public static RPBLAddressResultV2 GeoByAddress(BAGSearchKey keySearch, DTSSegment gsSegment, short districtID, EnumBAGLanguage language)
    {
        try
        {
            //var lstBAGDistrict = (IEnumerable)RunningParams.Objs.Values.Cast<BAGDistrict>().ToList();

            // 1. Tìm kiếm đường
            // 1.1 Tìm kiếm đường ưu tiên ở Hà Nội cũ
            RPBLAddressResultV2 resultRoad = BAGDecoding.SearchRoadByNameV2(gsSegment, keySearch, districtID);
            if (resultRoad == null)
                return null;

            // 2. Tiến hành tìm kiếm vùng
            // 2.1 Tìm kiếm thông tin vùng
            RTRectangle rec = new RTRectangle(resultRoad.Lng - Constants.DISTANCE_INTERSECT_ROAD, resultRoad.Lat - Constants.DISTANCE_INTERSECT_ROAD, resultRoad.Lng + Constants.DISTANCE_INTERSECT_ROAD, resultRoad.Lat + Constants.DISTANCE_INTERSECT_ROAD, 0.0f, 0.0f);
            RPBLAddressResultV2 resultRegion = BAGEncoding.RegionByGeoV2(rec, new BAGPointV2(resultRoad.Lng, resultRoad.Lat), language);
            if (resultRegion == null)
                return null;
            // 2.2 Bổ sung thông tin và trả về kết quả
            resultRegion.Lng = resultRoad.Lng;
            resultRegion.Lat = resultRoad.Lat;
            resultRegion.Building = resultRoad.Building;
            resultRegion.Road = resultRoad.Road;
            return resultRegion;
        }
        catch (Exception ex)
        {
            LogFile.WriteError(string.Format("MainProcessing.GeoByAddress, ex: {0}", ex.ToString()));
            return null;
        }
    }

    /// <summary>
    /// Tìm kiếm tọa độ qua địa chỉ (Áp dụng cho Hà Nội) V2
    /// </summary>
    private static RPBLAddressResultV2 GeoByAddressHaNoi(BAGSearchKey keySearch, DTSSegment gsSegment, short districtID, EnumBAGLanguage language)
    {
        try
        {
            // 1. Tìm kiếm đường
            // 1.1 Tìm kiếm đường ưu tiên ở Hà Nội cũ
            RPBLAddressResultV2 resultRoad = BAGDecoding.SearchRoadByNameHaNoiV2(gsSegment, keySearch, districtID);
            // 1.2 Nếu không có kết quả thì tìm ra Ha Tây cũ
            if (resultRoad == null)
            {
                // 1.2.1 Chỉ tìm kiếm khi có giới hạn quận huyện và quận huyện đó không thuộc Hà Tây cũ
                if (districtID == -1 || RunningParams.DistrictPriority.ContainsKey(districtID) == false)
                    resultRoad = BAGDecoding.SearchRoadByNameHaNoiV2(gsSegment, keySearch, -1, true);
                // 1.2.2 Kiểm tra kết quả
                if (resultRoad == null)
                    return null;
            }

            // 2. Tiến hành tìm kiếm vùng
            // 2.1 Tìm kiếm thông tin vùng
            RTRectangle rec = new RTRectangle(resultRoad.Lng - Constants.DISTANCE_INTERSECT_ROAD, resultRoad.Lat - Constants.DISTANCE_INTERSECT_ROAD, resultRoad.Lng + Constants.DISTANCE_INTERSECT_ROAD, resultRoad.Lat + Constants.DISTANCE_INTERSECT_ROAD, 0.0f, 0.0f);
            RPBLAddressResultV2 resultRegion = BAGEncoding.RegionByGeoV2(rec, new BAGPointV2(resultRoad.Lng, resultRoad.Lat), language);
            if (resultRegion == null)
                return null;
            // 2.2 Bổ sung thông tin và trả về kết quả
            resultRegion.Lng = resultRoad.Lng;
            resultRegion.Lat = resultRoad.Lat;
            resultRegion.Building = resultRoad.Building;
            resultRegion.Road = resultRoad.Road;
            resultRegion.MinSpeed = resultRoad.MinSpeed;
            resultRegion.MaxSpeed = resultRoad.MaxSpeed;
            resultRegion.DataExt = resultRoad.DataExt;
            return resultRegion;
        }
        catch (Exception ex)
        {
            LogFile.WriteError(string.Format("MainProcessing.GeoByAddressHaNoi, ex: {0}", ex.ToString()));
            return null;
        }
    }

    public async Task<string> BulkAsyncDistrict()
    {
        try
        {
            if (RunningParams.ProcessState != EnumProcessState.Success)
                return $"{INIT_DATA_FAIL}, Chưa khởi tạo xong dữ liệu, {RunningParams.ProcessState.ToString()}";
            var lstBAGDistrict = (IEnumerable)RunningParams.DistrictData.Objs.Values.Cast<BAGDistrict>().ToList();//List<BAGDistrict>

            var districts = _mapper.Map<List<BAGDistrictSearchDto>>(lstBAGDistrict);

            return await BulkAsyncDistrict(districts, GetIndexDistrict());
        }
        catch (Exception ex)
        { return ex.ToString(); }
    }

    public async Task<string> BulkAsyncProvince()
    {
        try
        {
            if (RunningParams.ProcessState != EnumProcessState.Success)
                return $"{INIT_DATA_FAIL}, Chưa khởi tạo xong dữ liệu, {RunningParams.ProcessState.ToString()}";

            var lstBAGProvince = (IEnumerable)RunningParams.ProvinceDataV2.Objs.Values.Cast<BAGProvince>().ToList();//List<BAGProvince>

            List<BAGProvinceSearchDto> provinces = _mapper.Map<List<BAGProvinceSearchDto>>(lstBAGProvince);

            return await BulkAsyncProvince(provinces, GetIndexProvince());
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }

    }

    //public  async Task<string> GetKeySearch(string keyword)
    //{
    //    string keywordAscii = string.Empty;

    //    if (!string.IsNullOrEmpty(keyword))
    //        keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

    //    string key = await AnalysisKeySearch(GetIndexProvince(), keywordAscii);

    //    //var result = await _client.SearchAsync<BAGProvinceSearchDto>(s => s.Index("provincesearch")
    //    //        .Size(1)
    //    //        .Query(q => q.Bool(
    //    //            b => b.Must(mu =>
    //    //             mu.Match(ma =>
    //    //                ma.Field(f => f.EName).Name("named_query").Analyzer("vn_analyzer3").Query(key).Fuzziness(Fuzziness.EditDistance(0))
    //    //                .AutoGenerateSynonymsPhraseQuery()
    //    //            )
    //    //            )
    //    //         //.MinimumShouldMatch(98)

    //    //         )
    //    //        )
    //    //       //.MinScore(5.0)
    //    //       .Sort(s => s.Descending(SortSpecialField.Score))
    //    //       .Scroll(1));
    //    //if (result.Documents.Any())
    //    //    return result.Documents.ToList()[0].EName?.ToString()??"";
    //    BAGSearchKey searchKey = await GetBAGSearchKey(GetIndexProvince(), keyword);

    //    return await Sum(GetIndexProvince(), keyword);
    //}

    
    public async Task<Result<object>> GetProvince(string keyword)
    {
        string keywordAscii = string.Empty;

        if (!string.IsNullOrEmpty(keyword))
            keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

        string key = await AnalysisKeySearch(GetIndexProvince(), keywordAscii);

        var result = await _client.SearchAsync<BAGProvinceSearchDto>(s => s.Index("provincesearch")
                .Size(1)
                .Query(q => q.Bool(
                    b => b.Must(mu =>
                     mu.Match(ma =>
                        ma.Field(f => f.EName).Name("named_query").Analyzer("vn_analyzer3").Query(key).Fuzziness(Fuzziness.EditDistance(0))
                        .AutoGenerateSynonymsPhraseQuery()
                    )
                    )
                 //.MinimumShouldMatch(98)

                 )
                )
               //.MinScore(5.0)
               .Sort(s => s.Descending(SortSpecialField.Score))
               .Scroll(1));
        //var data = new List<RPBLAddressResultV2>(ret);
        if (result.Documents.Any())
            return Result<object>.Success(result?.Documents?.ToList().FirstOrDefault()??null, INIT_DATA_SUCCESS, "Đã khởi tạo dữ liệu", RunningParams.ProcessState.ToString());
        return Result<object>.Success(null, INIT_DATA_SUCCESS, "Đã khởi tạo dữ liệu", RunningParams.ProcessState.ToString());
    }

    //public async Task<Result<object>> GetBAGSearchKey(string keyword)
    //{
    //    string keywordAscii = string.Empty;

    //    if (!string.IsNullOrEmpty(keyword))
    //        keywordAscii = LatinToAscii.Latin2Ascii(keyword.ToLower());

    //    BAGSearchKey searchKey = await GetBAGSearchKey(GetIndexProvince(), keyword);

    //    return Result<object>.Success(searchKey, INIT_DATA_SUCCESS, "Đã khởi tạo dữ liệu", RunningParams.ProcessState.ToString());
    //}

    public async Task<Result<object>> GeoByAddressAsync(string? keyStr, string? lanStr)
    {
        if (RunningParams.ProcessState != EnumProcessState.Success)
        {
            MainProcessing.InitData();
            return Result<object>.Error(INIT_DATA_FAIL, "Chưa khởi tạo xong dữ liệu", RunningParams.ProcessState.ToString());
        }

        var ret = await Task.Run(() => GeoByAddress(keyStr ?? "", lanStr ?? ""));

        if (ret == null)
        {
            LogFile.WriteNoDataGeobyAddress($"GeoByAddress, [ key:{keyStr} ]");
            return Result<object>.Success(new PBLAddressResultV3(), INIT_DATA_SUCCESS, "Đã khởi tạo dữ liệu", RunningParams.ProcessState.ToString());
        }

        var data = new PBLAddressResultV3(ret);
        return Result<object>.Success(data, INIT_DATA_SUCCESS, "Đã khởi tạo dữ liệu", RunningParams.ProcessState.ToString());
    }
}
