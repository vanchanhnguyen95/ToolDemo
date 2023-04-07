using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using SpeedWebAPI.Common.Constants;
using SpeedWebAPI.Common.Models;
using SpeedWebAPI.Infrastructure;
using SpeedWebAPI.Models;
using SpeedWebAPI.Models.SpeedLimitPQA;
using SpeedWebAPI.Services.Base;
using SpeedWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpeedWebAPI.Services
{
    #region interface
    public interface ISpeedLimitPQAService : IBaseService<SpeedLimitPQA>
    {
        /// <summary>
        /// Lấy thông tin SpeedLimit
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<object> GetSpeedProviders(int? limit);

        /// <summary>
        /// Update SpeedLimit từ api Push
        /// </summary>
        /// <param name="speedLimitParams"></param>
        /// <returns></returns>
        Task<IResult<object>> UpdateSpeedLimitPush(SpeedLimitParams speedLimitParams);

        /// <summary>
        /// Updload SpeedProvider từ file text
        /// </summary>
        /// <param name="speedProviderUpLoad"></param>
        /// <returns></returns>
        Task<IResult<object>> UpdloadSpeedProvider(List<SpeedProviderUpLoadVm> speedProviderUpLoad);

        Task<object> GetSpeedCurrent(int? limit);

        Task<ImportResponse<List<SpeedLimitPQA>>> ImportFromFileExcel(IFormFile formFile, string routeType, CancellationToken cancellationToken);

        Task<string> GetSpeedFromAPIPQA(List<SpeedLimitPQA> speedLimitPQAs, string url = @"http://103.47.194.15:11580/geocodebulk");
    }

    #endregion

    public class SpeedLimitPQAService : BaseService<SpeedLimitPQA, ApplicationDbContext>, ISpeedLimitPQAService
    {
        public SpeedLimitPQAService(ApplicationDbContext db) : base(db)
        {
        }

        public Task<object> GetSpeedCurrent(int? limit)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mỗi lần lấy dữ liệu chỉ lấy 1 tuyến(1 polyline có 1 segmendId)
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<object> GetSpeedProviders(int? limit)
        {
            try
            {
                if (limit == null || limit > 100)
                    limit = 100;

                var lstShow = new List<SpeedProvider>();

                var listQuery = await Db.SpeedLimitPQAs.Where(
                                x => x.DeleteFlag == 0
                                && x.IsUpdateSpeed == false )
                                .Take(limit ?? 100).OrderBy(x => x.STT).ToListAsync();
                if(!listQuery.Any())
                    return Result<object>.Success(lstShow, lstShow.Count(), Message.SUCCESS);

                //listQuery.ForEach(x => lstShow.Add(new SpeedProvider(x)));

                foreach (SpeedLimitPQA itemspeed in listQuery)
                {
                    var obj = await Db.SpeedLimitPQAs
                       .Where(x => x.Lat == (decimal)itemspeed.Lat
                       && x.Lng == itemspeed.Lng
                       && x.PointError == false).FirstOrDefaultAsync();

                    if (obj != null)
                    {
                        obj.IsUpdateSpeed = true;
                        Db.Entry(obj).State = EntityState.Modified;
                        lstShow.Add(new SpeedProvider(itemspeed));
                    }
                }

                await Db.SaveChangesAsync();

                return Result<object>.Success(lstShow, lstShow.Count(), Message.SUCCESS);
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.ToString());
            }
        }

        public async Task<ImportResponse<List<SpeedLimitPQA>>> ImportFromFileExcel(IFormFile formFile, string routeType, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return ImportResponse<List<SpeedLimitPQA>>.GetResult(-1, "formfile is empty");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return ImportResponse<List<SpeedLimitPQA>>.GetResult(-1, "Not Support file extension");
            }

            try
            {
                var dataImport = new List<SpeedLimitPQA>();

                using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream, cancellationToken);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 5; row <= rowCount; row++)
                        {
                            if ( worksheet.Cells[row, 1].Value == null
                                || worksheet.Cells[row, 2].Value == null
                                || worksheet.Cells[row, 3].Value == null
                                || worksheet.Cells[row, 4].Value == null
                                )
                                  continue;

                            if (Convert.ToInt32(worksheet.Cells[row, 4].Value.ToString().Trim()) == 0)
                                continue;

                            decimal lng = Convert.ToDecimal(worksheet.Cells[row, 2].Value.ToString().Trim());
                            decimal lat = Convert.ToDecimal(worksheet.Cells[row, 3].Value.ToString().Trim());
                            //LocationPQA locationPQA = new LocationPQA(lng, lat, "300");
                            //int speedPQA = await GetSpeedFromAPIPQA(locationPQA);

                            SpeedLimitPQA item = dataImport.Where(x => x.Lat == lat && x.Lng == lng).FirstOrDefault();
                            if(item != null)
                                dataImport.Remove(item);
                            item = null;

                            dataImport.Add(new SpeedLimitPQA
                            {
                                STT = Convert.ToInt32(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                Lng = lng,
                                Lat = lat,
                                SpeedGPS = Convert.ToInt32(worksheet.Cells[row, 4].Value.ToString().Trim()),
                                //SpeedPQA = 0,
                                Address = worksheet.Cells[row, 5].Value.ToString().Trim(),
                                RouteType = routeType,
                                FileName = formFile.FileName,
                                IsUpdSpeedPQA = false,

                                CreatedBy = "PQA",
                                CreatedDate = DateTime.Now
                            });
                        }

                        // add list to db ..
                        await AddDataFromFileExel(dataImport);
                        // here just read and return

                        return ImportResponse<List<SpeedLimitPQA>>.GetResult(0, "OK", dataImport);
                    }
                }

                //return ImportResponse<List<SpeedLimitPQA>>.GetResult(0, "OK", new List<SpeedLimitPQA>());
            }
            catch (Exception ex)
            {
                return ImportResponse<List<SpeedLimitPQA>>.GetResult(401, ex.ToString(), new List<SpeedLimitPQA>());
            }
        }

        private async Task<string> AddDataFromFileExel(List<SpeedLimitPQA> data)
        {
            try
            {
                if(!data.Any())
                    return "OK";

                // Xóa hết dữ liệu import cũ
                 Db.RemoveRange(data);
                 await Db.SaveChangesAsync();

                foreach (SpeedLimitPQA item in data)
                {
                    SpeedLimitPQA iCheck = Db.SpeedLimitPQAs.Where(x => x.Lat == item.Lat
                            && x.Lng == item.Lng).FirstOrDefault();
                    if (iCheck != null)
                        continue;

                    await Db.SpeedLimitPQAs.AddAsync(item);
                    await Db.SaveChangesAsync();
                }

                return "OK";
            }
            catch (Exception ex)
            {
                return $"Err AddDataFromFileExel: {ex}";
            }
        }

        public async Task<string> GetSpeedFromAPIPQA(List<SpeedLimitPQA> speedLimitPQAs, string url = "http://103.47.194.15:11580/geocodebulk")
        {
            try
            {
                if(!speedLimitPQAs.Any())
                    return "OK";

                foreach (SpeedLimitPQA item in speedLimitPQAs)
                {
                    LocationPQA loc = new LocationPQA(item.Lng, item.Lat);
                    int speed = await GetSingleSpeedFromAPIPQA(loc);

                    //item.MinSpeed = speedLimit.MinSpeed;
                    //item.MaxSpeed = speedLimit.MaxSpeed;
                    //item.IsUpdateSpeed = false;
                    //item.PointError = speedLimit.PointError ?? false;
                    item.IsUpdSpeedPQA = true;
                    item.SpeedPQA = speed;
                    item.UpdateCount++;
                    item.UpdatedDate = DateTime.Now;
                    item.UpdatedBy = $"Upd numbers {item.UpdateCount?.ToString()}";

                    Db.Entry(item).State = EntityState.Modified;

                }

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private async Task<int> GetSingleSpeedFromAPIPQA(LocationPQA locationPQA, string url = @"http://103.47.194.15:11580/geocodebulk")
        {
            // url gọi lấy tọa độ được cung cấp từ PQA
            //var url = @"http://103.47.194.15:11580/geocodebulk";

            GeocodeBulkPush geocodebulkpush = new GeocodeBulkPush();
            geocodebulkpush.locations = new List<LocationPQA>();
            geocodebulkpush.locations.Add(locationPQA);
            try
            {
                var json = JsonConvert.SerializeObject(geocodebulkpush);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                //var result = client.PostAsync(url, content).Result;
                using (var client = new HttpClient())
                {
                    var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, data);
                    var result = await response.Content.ReadAsStringAsync();

                    var geocodeBulkResponse = JsonConvert.DeserializeObject<GeocodeBulkResponse>(result);

                    if (geocodeBulkResponse.status != "OK")
                        return 0;

                    if (geocodeBulkResponse.info.Any())
                        return geocodeBulkResponse.info[0].speed_max;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<IResult<object>> UpdateSpeedLimitPush(SpeedLimitParams speedLimitParams)
        {

            foreach (SpeedLimitPush item in speedLimitParams.data)
            {
                await UpdateSpeedLimitPush(item);
            }

            return Result<object>.Success(speedLimitParams);
        }

        public Task<IResult<object>> UpdloadSpeedProvider(List<SpeedProviderUpLoadVm> speedProviderUpLoad)
        {
            throw new NotImplementedException();
        }

        private async Task<IResult<object>> UpdateSpeedLimitPush(SpeedLimitPush speedLimit)
        {
            try
            {
                var lstUpd = await Db.SpeedLimitPQAs
                .Where(x => x.Lat == speedLimit.Lat
                && x.Lng == speedLimit.Lng
                //&& x.ProviderType == speedLimit.ProviderType
                && x.DeleteFlag == 0
                && x.IsUpdateSpeed == true
                //&& x.SegmentID == speedLimit.SegmentID
                //&& x.Direction == speedLimit.Direction
                && x.PointError == false).ToListAsync();

                if (lstUpd != null)
                {
                    foreach (SpeedLimitPQA item in lstUpd)
                    {
                        item.MinSpeed = speedLimit.MinSpeed;
                        item.MaxSpeed = speedLimit.MaxSpeed;
                        item.IsUpdateSpeed = false;
                        item.PointError = speedLimit.PointError ?? false;
                        item.UpdateCount++;
                        item.UpdatedDate = DateTime.Now;
                        item.UpdatedBy = $"Upd numbers {item.UpdateCount?.ToString()}";

                        Db.Entry(item).State = EntityState.Modified;
                    }
                    await Db.SaveChangesAsync();
                }

                return Result<object>.Success(lstUpd);
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.ToString());
            }

        }

        
    }
}
