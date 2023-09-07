using Amazon.Auth.AccessControlPolicy;
using AutoMapper;
using BAGeocoding.Api.Dto;
using BAGeocoding.Api.Interfaces;
using BAGeocoding.Api.Models.PBD;
using BAGeocoding.Api.ViewModels;
using BAGeocoding.Utility;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Nest;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Json;
using ThirdParty.Json.LitJson;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BAGeocoding.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class RoadNameController : BaseController
    {
        private readonly IRoadNameService _roadNameService;
        private readonly IVietNamShapeService _vietNamShapeService;
        private readonly IProvinceService _provinceService;
        private readonly IRoadElasticService _roadElasticService;
        private readonly IMapper _mapper;

        public RoadNameController(IMapper mapper, IRoadNameService roadNameService, IVietNamShapeService vietNamShapeService, IProvinceService provinceService,
            IRoadElasticService roadElasticService)
        {
            _roadNameService = roadNameService;
            _vietNamShapeService = vietNamShapeService;
            _provinceService = provinceService;
            _roadElasticService = roadElasticService;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("BulkAsync")]
        public async Task<IActionResult> BulkAsync(string path = @"D:\Project\Els01\Db\pastedText.json")
        {

            if (string.IsNullOrEmpty(path)) path = @"D:\Project\Els01\Db\pastedText.json";
            var jsonData = System.IO.File.ReadAllText(path);
            var roadPushs = JsonConvert.DeserializeObject<List<RoadNamePush>>(jsonData);

            return Ok(await _roadNameService.BulkAsync(roadPushs ?? new List<RoadNamePush>()));
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("BulkVTPost")]
        public async Task<IActionResult> BulkVTPost(string path = @"D:\Geo\data.json")
        {

            if (string.IsNullOrEmpty(path)) path = @"D:\Geo\data.json";
            //string[] lines;
            //lines = System.IO.File.ReadAllLines(path);

            //int totalRows = lines.Length;

            //for(int i = 3; i < 103; i++)
            //{
            //    var a = lines[i];
            //}

            //using (StreamReader? file = new StreamReader(path))
            //{
            //    string? redend = await file?.ReadToEndAsync()??"";
            //}

            //string json = System.IO.File.ReadAllText(path);
            //var lineEnd = System.IO.File.ReadAllLinesAsync(path);
            //var roadPushs = JsonConvert.DeserializeObject<List<RoadNameV2>>(jsonData);
            // Deserialize the JSON string into objects
            //var places = JsonConvert.DeserializeObject<RoadNameVT[]>(json);

            // Read the JSON file line by line
            //using (StreamReader file = new StreamReader("path/to/your/file.json"))
            List<RoadNameVT> roadNameVTs = new List<RoadNameVT>();
            try
            {
                using (StreamReader r = new StreamReader(@"D:\Geo\data.json"))
                {
                    var jsonddd = r.ReadToEnd();
                    List<DataVT> src = JsonConvert.DeserializeObject<List<DataVT>>(jsonddd);
                }

                using (StreamReader file = new StreamReader(path))
                {
                    string line;
                    int i = 0;
                    while ((line = file.ReadLine()) != null)
                    {
                       // Parse the JSON string into a JsonDocument
                       JsonDocument document = JsonDocument.Parse(line);

                        // Deserialize the JSON object in each line
                        var dataVT = JsonConvert.DeserializeObject<DataVT>(line);

                        if (dataVT._source != null)
                        {
                            // Kiểm tra xem điểm này thuộc tỉnh nào
                            // GetProvinceId
                            List<VietNamShape> lst;
                            lst = await _vietNamShapeService.GetDataSuggestion(dataVT._source.latitude, dataVT._source.longitude, GeoDistanceType.Arc, "50km", 5, null, GeoShapeRelation.Intersects);

                            if (lst.Any())
                                //lst.Where(x => x.provinceid > 0).Select(x => x.provinceid).FirstOrDefault();
                                dataVT._source.provinceid = lst.Where(x => x.provinceid > 0).Select(x => x.provinceid).FirstOrDefault();

                            //dataVT._source.provinceid = lst[0].provinceid;

                            roadNameVTs.Add(new RoadNameVT(dataVT._source));
                        }    
                            
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            
            //return Ok(await _roadNameService.BulkAsync(roadPushs ?? new List<RoadNamePush>()));
            return Ok();
        }

        [HttpPost]
        [Route("AddDataVietNamShape")]
        public async Task<IActionResult> PostMultiFile2()
        {
            List<string> paths = new List<string>();
            paths.Add(@"D:\GeoJson\diaphantinh.geojson");
            paths.Add(@"D:\GeoJson\district.geojson");
            paths.Add(@"D:\GeoJson\giaothong.geojson");

            try
            {
                List<VietNamShape> vnShape = new List<VietNamShape>();

                //if (haNoiShapePush.Any())
                //    haNoiShapePush.ForEach(item => vnShape.Add(new VietNamShape(item)));

                int i = 1;
                foreach (string path in paths)
                {
                    var jsonData = System.IO.File.ReadAllText(path);
                    var vnCheck = JsonConvert.DeserializeObject<GeoJsonVietNam>(jsonData);

                    var geoJsonReader = new GeoJsonReader();
                    var featureCollection = geoJsonReader.Read<NetTopologySuite.Features.FeatureCollection>(jsonData);

                    if (featureCollection == null) Ok("Fail");

                    // Lặp qua từng đối tượng Feature trong FeatureCollection và trích xuất thông tin về đối tượng địa lý
                    foreach (var feature in featureCollection)
                    {
                        //VietNamShape item = GetNamShape(feature, type);
                        vnShape.Add(GetNamShape(feature, vnCheck.typedata, i));
                        i++;
                    }
                }

                return Ok(await _vietNamShapeService.BulkAsync(vnShape));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost]
        [Route("PostFileRoad")]
        public async Task<IActionResult> PostFileRoad(List<IFormFile> files)
        {
            try
            {
                if (files.Any())
                {
                    foreach (IFormFile file in files)
                    {
                        
                    }    
                }else
                {
                    //List<string> paths = new List<string>();
                    //paths.Add(@"D:\Geo\PbdRoadName.json");
                    //paths.Add(@"D:\Geo\PdbPointOfInterest.json");

                    List<RoadNameMerge> data = new List<RoadNameMerge>();

                    var jsonDataRoad = System.IO.File.ReadAllText(@"D:\Geo\PbdRoadName.json");
                    var dataRoad = JsonConvert.DeserializeObject<List<RoadNamePush>>(jsonDataRoad);

                    if (dataRoad.Any())
                    {
                        foreach (RoadNamePush dataR in dataRoad)
                        {
                            // Lấy ra ProvinName
                            //Province province = await _provinceService.GetProvinceById(dataR.ProvinceID);

                            dataR.ProvinceName = await _provinceService.GetNameById(dataR.ProvinceID);
                            //dataR.ProvinceNameAscii  = province.EName;

                            //string roadNameAscii = LatinToAscii.Latin2Ascii(dataR.RoadName.ToLower());
                            // Phân biệt ngõ, ngách, hẻm
                            if (dataR?.RoadName?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataR.TypeArea = 1;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataR.TypeArea = 2;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataR.TypeArea = 3;
                            }
                            data.Add(new RoadNameMerge(dataR));
                        }
                    }
                    //dataRoad.ForEach(item => data.Add(new RoadNameMerge(item)));

                    var jsonDataPoint = System.IO.File.ReadAllText(@"D:\Geo\PdbPointOfInterest.json");
                    var dataPoint = JsonConvert.DeserializeObject<List<PointOfInterest>>(jsonDataPoint);

                    if (dataPoint.Any())
                    {
                        foreach (PointOfInterest dataP in dataPoint)
                        {
                            // Lấy ra ProvinName

                            if (string.IsNullOrEmpty(dataP.Road))
                                continue;

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataP?.Road?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataP.TypeArea = 1;
                            }
                            else if (dataP?.Road?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataP.TypeArea = 2;
                            }
                            else if (dataP?.Road?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataP.TypeArea = 3;
                            }

                            data.Add(new RoadNameMerge(dataP));
                        }
                    }    
                        //dataPoint.ForEach(item => data.Add(new RoadNameMerge(item)));

                }    

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("PostRoadByProvince")]
        public async Task<IActionResult> PostRoadByProvince(List<IFormFile> files,int shapeid = 0, int provinceID = 0)
        {
            try
            {
                if(provinceID < 0)
                    return Ok("Vui lòng chọn mã tỉnh để import dữ liệu");

                List<RoadNameMerge> data = new List<RoadNameMerge>();
                if (files.Any())
                {
                    foreach (IFormFile file in files)
                    {

                    }
                }
                else
                {
                    //List<string> paths = new List<string>();
                    //paths.Add(@"D:\Geo\PbdRoadName.json");
                    //paths.Add(@"D:\Geo\PdbPointOfInterest.json");

                    var jsonDataPoint = System.IO.File.ReadAllText(@"D:\Geo\PdbPointOfInterest2.json");
                    var dataPoint = JsonConvert.DeserializeObject<List<PointOfInterest>>(jsonDataPoint);

                    // index cho điểm, riêng cho từng tỉnh
                    // index cho điểm hoặc cho cả đường cả điểm,cho từng tỉnh
                    //if (dataPoint.Any() && shapeid == 1 && provinceID > 0)
                    if (dataPoint.Any() && (shapeid == 0 || shapeid == 1) && provinceID > 0)
                    {
                        // Lấy ra ProvinName
                        //string provinceName = await _provinceService.GetNameById(provinceID);

                        foreach (PointOfInterest dataP in dataPoint.Where(x => x.ProvinceID == provinceID))
                        {
                            if (string.IsNullOrEmpty(dataP.Name))
                                continue;

                            // Lấy ra ProvinName
                            dataP.ProvinceName = dataP?.ProvinceName?.Replace("TP.","").Trim();

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataP?.Name?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataP.TypeArea = 1;
                            }
                            else if (dataP?.Name?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataP.TypeArea = 2;
                            }
                            else if (dataP?.Name?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataP.TypeArea = 3;
                            }

                            data.Add(new RoadNameMerge(dataP));
                        }
                    }
                    // index cho điểm hoặc cho cả điểm cả đường,cho toàn bộ VN
                    else if (dataPoint.Any() && (shapeid == 0 || shapeid == 1 )&& provinceID == 0)
                    {
                        foreach (PointOfInterest dataP in dataPoint)
                        {
                            if (string.IsNullOrEmpty(dataP.Name))
                                continue;

                            // Lấy ra ProvinName
                            dataP.ProvinceName = dataP?.ProvinceName?.Replace("TP.","").Trim(); ;

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataP?.Name?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataP.TypeArea = 1;
                            }
                            else if (dataP?.Name?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataP.TypeArea = 2;
                            }
                            else if (dataP?.Name?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataP.TypeArea = 3;
                            }

                            data.Add(new RoadNameMerge(dataP));
                        }
                    }
                   
                    //else if (dataPoint.Any() && (shapeid == 0) && provinceID > 0)
                    //{
                    //    foreach (PointOfInterest dataP in dataPoint)
                    //    {
                    //        // Lấy ra ProvinName

                    //        if (string.IsNullOrEmpty(dataP.Name))
                    //            continue;

                    //        // Phân biệt ngõ, ngách, hẻm
                    //        if (dataP?.Name?.ToLower().IndexOf("ngõ") == 0)
                    //        {
                    //            dataP.TypeArea = 1;
                    //        }
                    //        else if (dataP?.Name?.ToLower().IndexOf("ngách") == 0)
                    //        {
                    //            dataP.TypeArea = 2;
                    //        }
                    //        else if (dataP?.Name?.ToLower().IndexOf("hẻm") == 0)
                    //        {
                    //            dataP.TypeArea = 3;
                    //        }

                    //        data.Add(new RoadNameMerge(dataP));
                    //    }
                    //}

                    //dataPoint.ForEach(item => data.Add(new RoadNameMerge(item)));


                    var jsonDataRoad = System.IO.File.ReadAllText(@"D:\Geo\PbdRoadName.json");
                    var dataRoad = JsonConvert.DeserializeObject<List<RoadNamePush>>(jsonDataRoad);

                    // index cho đường, riêng cho từng tỉnh
                    // index cho đường hoặc cho cả đường cả điểm,cho từng tỉnh
                    //if (dataRoad.Any() && shapeid == 2 && provinceID > 0)
                    if (dataRoad.Any() && (shapeid == 0 || shapeid == 2) && provinceID > 0)
                    {
                        // Lấy ra ProvinName
                        //Province province = await _provinceService.GetProvinceById(dataR.ProvinceID);
                        //string provinceName = await _provinceService.GetNameById(provinceID);

                        foreach (RoadNamePush dataR in dataRoad.Where(x => x.ProvinceID == provinceID))
                        {
                            if (string.IsNullOrEmpty(dataR.RoadName))
                                continue;

                            dataR.ProvinceName = dataR?.Address?.Replace("TP.","").Trim(); ;

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataR?.RoadName?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataR.TypeArea = 1;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataR.TypeArea = 2;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataR.TypeArea = 3;
                            }
                            data.Add(new RoadNameMerge(dataR));
                        }
                    }
                    // index cho đường hoặc cho cả đường cả điểm,cho toàn bộ VN
                  
                    else if (dataRoad.Any() && (shapeid == 0 || shapeid == 2) && provinceID == 0)
                    {
                        foreach (RoadNamePush dataR in dataRoad)
                        {
                            if (string.IsNullOrEmpty(dataR.RoadName))
                                continue;
                            dataR.ProvinceName = dataR?.Address?.Replace("TP.","").Trim();

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataR?.RoadName?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataR.TypeArea = 1;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataR.TypeArea = 2;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataR.TypeArea = 3;
                            }
                            data.Add(new RoadNameMerge(dataR));
                        }
                    }
                    //dataRoad.ForEach(item => data.Add(new RoadNameMerge(item)));
                }

                return Ok(await _roadNameService.BulkAsyncByProvince(data,shapeid, provinceID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("PostRoadElasticNoShape")]
        public async Task<IActionResult> PostRoadElasticNoShape(List<IFormFile> files, int provinceID = 0)
        {
            try
            {
                if (provinceID < 0)
                    return Ok("Vui lòng chọn mã tỉnh để import dữ liệu");

                List<BGCElasticRequestCreate> data = new List<BGCElasticRequestCreate>();
                if (files.Any())
                {
                    foreach (IFormFile file in files)
                    {

                    }
                }
                else
                {
                    var jsonDataPoint = System.IO.File.ReadAllText(@"D:\Geo\BGCPointOfInterest.json");
                    var dataPoint = JsonConvert.DeserializeObject<List<BGCPointOfInterest>>(jsonDataPoint);

                    // index cho điểm, riêng cho từng tỉnh
                    // index cho điểm hoặc cho cả đường cả điểm,cho từng tỉnh
                    //if (dataPoint.Any() && shapeid == 1 && provinceID > 0)
                    if (dataPoint.Any() && provinceID > 0)
                    {
                        // Lấy ra ProvinName
                        //string provinceName = await _provinceService.GetNameById(provinceID);

                        foreach (BGCPointOfInterest dataP in dataPoint.Where(x => x.ProvinceID == provinceID))
                        {
                            if (string.IsNullOrEmpty(dataP.Name))
                                continue;

                            // Lấy ra ProvinName
                            dataP.ProvinceName = dataP?.ProvinceName?.Replace("TP.", "").Trim();

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataP?.Name?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataP.TypeArea = 1;
                            }
                            else if (dataP?.Name?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataP.TypeArea = 2;
                            }
                            else if (dataP?.Name?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataP.TypeArea = 3;
                            }

                            data.Add(new BGCElasticRequestCreate(dataP));
                        }
                    }
                    // index cho điểm hoặc cho cả điểm cả đường,cho toàn bộ VN
                    else if (dataPoint.Any() && provinceID == 0)
                    {
                        foreach (BGCPointOfInterest dataP in dataPoint)
                        {
                            if (string.IsNullOrEmpty(dataP.Name))
                                continue;

                            // Lấy ra ProvinName
                            dataP.ProvinceName = dataP?.ProvinceName?.Replace("TP.", "").Trim(); ;

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataP?.Name?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataP.TypeArea = 1;
                            }
                            else if (dataP?.Name?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataP.TypeArea = 2;
                            }
                            else if (dataP?.Name?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataP.TypeArea = 3;
                            }

                            data.Add(new BGCElasticRequestCreate(dataP));
                        }
                    }

                    var jsonDataRoad = System.IO.File.ReadAllText(@"D:\Geo\PbdRoadName.json");
                    var dataRoad = JsonConvert.DeserializeObject<List<BGCRoadName>>(jsonDataRoad);

                    // index cho đường, riêng cho từng tỉnh
                    // index cho đường hoặc cho cả đường cả điểm,cho từng tỉnh
                    //if (dataRoad.Any() && shapeid == 2 && provinceID > 0)
                    if (dataRoad.Any() && provinceID > 0)
                    {
                        // Lấy ra ProvinName
                        //Province province = await _provinceService.GetProvinceById(dataR.ProvinceID);
                        //string provinceName = await _provinceService.GetNameById(provinceID);

                        foreach (BGCRoadName dataR in dataRoad.Where(x => x.ProvinceID == provinceID))
                        {
                            if (string.IsNullOrEmpty(dataR.RoadName))
                                continue;

                            dataR.ProvinceName = dataR?.Address?.Replace("TP.", "").Trim(); ;

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataR?.RoadName?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataR.TypeArea = 1;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataR.TypeArea = 2;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataR.TypeArea = 3;
                            }
                            data.Add(new BGCElasticRequestCreate(dataR));
                        }
                    }
                    // index cho đường hoặc cho cả đường cả điểm,cho toàn bộ VN

                    else if (dataRoad.Any() && provinceID == 0)
                    {
                        foreach (BGCRoadName dataR in dataRoad)
                        {
                            if (string.IsNullOrEmpty(dataR.RoadName))
                                continue;
                            dataR.ProvinceName = dataR?.Address?.Replace("TP.", "").Trim();

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataR?.RoadName?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataR.TypeArea = 1;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataR.TypeArea = 2;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataR.TypeArea = 3;
                            }
                            data.Add(new BGCElasticRequestCreate(dataR));
                        }
                    }
                    //dataRoad.ForEach(item => data.Add(new RoadNameMerge(item)));
                }

                return Ok(await _roadElasticService.BulkAsyncByProvinceNoShape(data, provinceID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("PostRoadElastic")]
        public async Task<IActionResult> PostRoadElastic(List<IFormFile> files, int shapeid = 0, int provinceID = 0)
        {
            try
            {
                if (provinceID < 0)
                    return Ok("Vui lòng chọn mã tỉnh để import dữ liệu");

                List<BGCElasticRequestCreate> data = new List<BGCElasticRequestCreate>();
                if (files.Any())
                {
                    foreach (IFormFile file in files)
                    {

                    }
                }
                else
                {
                    var jsonDataPoint = System.IO.File.ReadAllText(@"D:\Geo\BGCPointOfInterest.json");
                    var dataPoint = JsonConvert.DeserializeObject<List<BGCPointOfInterest>>(jsonDataPoint);

                    // index cho điểm, riêng cho từng tỉnh
                    // index cho điểm hoặc cho cả đường cả điểm,cho từng tỉnh
                    //if (dataPoint.Any() && shapeid == 1 && provinceID > 0)
                    if (dataPoint.Any() && (shapeid == 0 || shapeid == 1) && provinceID > 0)
                    {
                        // Lấy ra ProvinName
                        //string provinceName = await _provinceService.GetNameById(provinceID);

                        foreach (BGCPointOfInterest dataP in dataPoint.Where(x => x.ProvinceID == provinceID))
                        {
                            if (string.IsNullOrEmpty(dataP.Name))
                                continue;

                            // Lấy ra ProvinName
                            dataP.ProvinceName = dataP?.ProvinceName?.Replace("TP.", "").Trim();

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataP?.Name?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataP.TypeArea = 1;
                            }
                            else if (dataP?.Name?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataP.TypeArea = 2;
                            }
                            else if (dataP?.Name?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataP.TypeArea = 3;
                            }

                            data.Add(new BGCElasticRequestCreate(dataP));
                        }
                    }
                    // index cho điểm hoặc cho cả điểm cả đường,cho toàn bộ VN
                    else if (dataPoint.Any() && (shapeid == 0 || shapeid == 1) && provinceID == 0)
                    {
                        foreach (BGCPointOfInterest dataP in dataPoint)
                        {
                            if (string.IsNullOrEmpty(dataP.Name))
                                continue;

                            // Lấy ra ProvinName
                            dataP.ProvinceName = dataP?.ProvinceName?.Replace("TP.", "").Trim(); ;

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataP?.Name?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataP.TypeArea = 1;
                            }
                            else if (dataP?.Name?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataP.TypeArea = 2;
                            }
                            else if (dataP?.Name?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataP.TypeArea = 3;
                            }

                            data.Add(new BGCElasticRequestCreate(dataP));
                        }
                    }

                    var jsonDataRoad = System.IO.File.ReadAllText(@"D:\Geo\PbdRoadName.json");
                    var dataRoad = JsonConvert.DeserializeObject<List<BGCRoadName>>(jsonDataRoad);

                    // index cho đường, riêng cho từng tỉnh
                    // index cho đường hoặc cho cả đường cả điểm,cho từng tỉnh
                    //if (dataRoad.Any() && shapeid == 2 && provinceID > 0)
                    if (dataRoad.Any() && (shapeid == 0 || shapeid == 2) && provinceID > 0)
                    {
                        // Lấy ra ProvinName
                        //Province province = await _provinceService.GetProvinceById(dataR.ProvinceID);
                        //string provinceName = await _provinceService.GetNameById(provinceID);

                        foreach (BGCRoadName dataR in dataRoad.Where(x => x.ProvinceID == provinceID))
                        {
                            if (string.IsNullOrEmpty(dataR.RoadName))
                                continue;

                            dataR.ProvinceName = dataR?.Address?.Replace("TP.", "").Trim(); ;

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataR?.RoadName?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataR.TypeArea = 1;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataR.TypeArea = 2;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataR.TypeArea = 3;
                            }
                            data.Add(new BGCElasticRequestCreate(dataR));
                        }
                    }
                    // index cho đường hoặc cho cả đường cả điểm,cho toàn bộ VN

                    else if (dataRoad.Any() && (shapeid == 0 || shapeid == 2) && provinceID == 0)
                    {
                        foreach (BGCRoadName dataR in dataRoad)
                        {
                            if (string.IsNullOrEmpty(dataR.RoadName))
                                continue;
                            dataR.ProvinceName = dataR?.Address?.Replace("TP.", "").Trim();

                            // Phân biệt ngõ, ngách, hẻm
                            if (dataR?.RoadName?.ToLower().IndexOf("ngõ") == 0)
                            {
                                dataR.TypeArea = 1;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("ngách") == 0)
                            {
                                dataR.TypeArea = 2;
                            }
                            else if (dataR?.RoadName?.ToLower().IndexOf("hẻm") == 0)
                            {
                                dataR.TypeArea = 3;
                            }
                            data.Add(new BGCElasticRequestCreate(dataR));
                        }
                    }
                    //dataRoad.ForEach(item => data.Add(new RoadNameMerge(item)));
                }

                return Ok(await _roadElasticService.BulkAsyncByProvince(data, shapeid, provinceID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        //[HttpPost]
        //[Route("PostMultiFile")]
        //public async Task<IActionResult> PostMultiFile(List<IFormFile> files)
        //{
        //    if (!files.Any()) return BadRequest("Invalid file");

        //    List<string> paths = new List<string>();
        //    //paths.Add(@"D:\GeoJson\airport.geojson");
        //    //paths.Add(@"D:\GeoJson\atm_rice_covid-19.geojson");
        //    paths.Add(@"D:\GeoJson\diaphantinh.geojson");
        //    paths.Add(@"D:\GeoJson\district.geojson");
        //    //paths.Add(@"D:\GeoJson\ga.geojson");
        //    paths.Add(@"D:\GeoJson\giaothong.geojson");

        //    try
        //    {
        //        List<VietNamShape> vnShape = new List<VietNamShape>();

        //        // Lấy dữ liệu từ index hanoishape
        //        //if (haNoiShapePush.Any())
        //        //    haNoiShapePush.ForEach(item => vnShape.Add(new VietNamShape(item)));

        //        int i = 1;
        //        foreach (IFormFile file in files)
        //        {
        //            using var stream = file.OpenReadStream();
        //            using var reader = new StreamReader(stream);

        //            NetTopologySuite.Features.FeatureCollection featureCollection;
        //            var jsonData = await reader.ReadToEndAsync();

        //            var vnCheck = JsonConvert.DeserializeObject<GeoJsonVietNam>(jsonData);

        //            featureCollection = new GeoJsonReader().Read<NetTopologySuite.Features.FeatureCollection>(jsonData);

        //            if (featureCollection == null) Ok("Fail");

        //            // Lặp qua từng đối tượng Feature trong FeatureCollection và trích xuất thông tin về đối tượng địa lý
        //            foreach (var feature in featureCollection)
        //            {
        //                //VietNamShape item = GetNamShape(feature, type);
        //                vnShape.Add(GetNamShape(feature, vnCheck.typedata, i));
        //                i++;
        //            }

        //        }
        //        i = 0;
        //        return Ok(await _vietNamShapeService.BulkAsync(vnShape));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.ToString());
        //    }

        //}

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetDataSuggestion")]
        public async Task<IActionResult> GetDataSuggestion(double lat = 0, double lng = 0, string distance = "100km", int size = 5, string keyword = null, int type = -1)
        {
            // type: -1: tìm kiếm bao gồm phần mở rộng
            //double lat = 21.006423010707078, double lng = 105.83878960584113, string distance = "30000m", int pageSize = 10, string keyWord = null
            return Ok(await _roadNameService.GetDataSuggestion(lat, lng, distance, size, keyword, type));
        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Add2Geo")]
        public async Task<IActionResult> Add2Geo([FromBody] GeoByAddressVm? body)
        {
            return Ok(await _roadElasticService.Add2Geo(body?.address, "vn"));
        }

        //[HttpGet]
        //[MapToApiVersion("1.0")]
        //[Route("Search")]
        //public async Task<IActionResult> Search(double lat = 0, double lng = 0, string distance = "300km", int size = 5, string keyword = null, int shapeid = 0)
        //{
        //    //var owner = _mapper.Map<List<RoadNameMergeDto>>(
        //    //    _roadNameService.GetDataSuggestionByProvince(lat, lng, distance, size, keyword));
        //    //return Ok(await _roadNameService.GetDataSuggestionByProvince(lat, lng, distance, size, keyword));
        //    //return Ok(await _roadNameService.Search(lat, lng, distance, size, keyword, shapeid));
        //    return Ok(await _roadElasticService.Search3(lat, lng, distance, size, keyword, shapeid));
        //}

        //[HttpGet]
        //[MapToApiVersion("1.0")]
        //[Route("Search2")]
        //public async Task<IActionResult> Search2(double lat = 0, double lng = 0, string distance = "300km", int size = 5, string keyword = null, int shapeid = 0)
        //{
        //    //var owner = _mapper.Map<List<RoadNameMergeDto>>(
        //    //    _roadNameService.GetDataSuggestionByProvince(lat, lng, distance, size, keyword));
        //    //return Ok(await _roadNameService.GetDataSuggestionByProvince(lat, lng, distance, size, keyword));
        //    return Ok(await _roadElasticService.Search(lat, lng, distance, size, keyword, shapeid));
        //}

        //[HttpGet]
        //[MapToApiVersion("1.0")]
        //[Route("Search3")]
        //public async Task<IActionResult> Search3(double lat = 0, double lng = 0, string distance = "300km", int size = 5, string keyword = null, int shapeid = 0)
        //{
        //    //var owner = _mapper.Map<List<RoadNameMergeDto>>(
        //    //    _roadNameService.GetDataSuggestionByProvince(lat, lng, distance, size, keyword));
        //    //return Ok(await _roadNameService.GetDataSuggestionByProvince(lat, lng, distance, size, keyword));
        //    return Ok(await _roadElasticService.Search3(lat, lng, distance, size, keyword, shapeid));
        //}

        private VietNamShape GetNamShape(IFeature feature, string type = "P", int i = 1)
        {
            // Đối tượng địa lý được lưu trữ trong biến "feature"
            // Truy cập đối tượng địa lý trong thuộc tính "Geometry" của Feature
            var writer = new WKTWriter();
            var geometry = feature.Geometry;

            VietNamShape item = new VietNamShape();
            item.location = writer.Write(geometry);

            if (type == "P")//Province
            {
                //item.id = Convert.ToInt32((feature.Attributes["gid"].ToString()) ?? "0");
                item.id = i;
                item.typename = "Tỉnh";
                item.provinceid = Convert.ToInt32((feature.Attributes["provinceid"].ToString()) ?? "0"); ;
                item.name = feature.Attributes["ten_tinh"].ToString() ?? "";
                item.keywords = feature.Attributes["ten_tinh"].ToString() ?? "";
            }
            else if (type == "D")//District
            {
                item.id = i;
                item.province = feature.Attributes["Province"].ToString();
                item.typename = @"Huyện";
                item.name = feature.Attributes["District"].ToString();
                item.keywords = feature.Attributes["District"].ToString() ?? "" + ", " + feature.Attributes["Province"].ToString() ?? "";
            }
            else if (type == "T")//Traffic
            {
                item.id = i;
                item.typename = @"Đường giao thông";
                item.name = feature.Attributes["ten"].ToString() ?? "";
                item.keywords = feature.Attributes["ten"].ToString() ?? "";
            }
            else if (type == "H")//harbor
            {
                item.id = i;
                item.typename = @"Bến cảng";

                var na = feature.Attributes["Name"];
                string nastrin = string.Empty;
                if (na != null)
                {
                    nastrin = na.ToString() ?? "";
                }

                item.name = nastrin;
                item.keywords = nastrin;
            }
            else if (type == "A")//airport
            {
                item.id = i;
                item.typename = @"Cảng hàng không";
                item.name = feature.Attributes["Name"].ToString() ?? "";
                item.keywords = feature.Attributes["Name"].ToString() ?? "" + ", " + feature.Attributes["City"].ToString() ?? "";
            }
            else if (type == "TS")//train station
            {
                item.id = i;
                item.typename = @"Ga đường sắt";
                item.name = feature.Attributes["Ten_Ga"].ToString() ?? "";
                item.keywords = feature.Attributes["Ten_Ga"].ToString() ?? "";
            }
            else if (type == "NP")//National Parks
            {
                item.id = i;
                item.typename = @"Vườn Quốc gia";
                item.name = feature.Attributes["Ten"].ToString() ?? ""; ;
                item.keywords = feature.Attributes["Ten"].ToString() ?? "";
            }
            //hydropower_2020
            else if (type == "HD")//hydropower
            {
                item.id = i;
                item.typename = @"Thủy điện";
                item.name = feature.Attributes["Vietnamese"].ToString() ?? ""; ;
                item.keywords = feature.Attributes["Vietnamese"].ToString() ?? "";
            }
            else if (type == "AG")//atm_rice_covid-19
            {
                item.id = i;
                item.typename = @"ATM Gạo";
                item.name = feature.Attributes["Name_VI"].ToString() ?? ""; ;
                item.keywords = feature.Attributes["Name_VI"].ToString() ?? "";
            }

            return item;

        }
    }
}
