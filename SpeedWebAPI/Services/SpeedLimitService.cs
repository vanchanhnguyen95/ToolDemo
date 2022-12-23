using Microsoft.EntityFrameworkCore;
using SpeedWebAPI.Common.Constants;
using SpeedWebAPI.Common.Models;
using SpeedWebAPI.Infrastructure;
using SpeedWebAPI.Models;
using SpeedWebAPI.Services.Base;
using SpeedWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWebAPI.Services
{
    #region interface
    public interface ISpeedLimitService : IBaseService<SpeedLimit>
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
    }

    #endregion

    public class SpeedLimitService : BaseService<SpeedLimit, ApplicationDbContext>, ISpeedLimitService
    {
        public SpeedLimitService(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<object> GetSpeedCurrent(int? limit)
        {
            try
            {
                if (limit == null || limit > 100)
                    limit = 100;

                var query = (from s in Db.SpeedLimits
                             where s.DeleteFlag == 0 && s.PointError == false
                             && s.UpdatedDate.Value.Date == DateTime.Now.Date
                             && s.UpdatedDate.Value.Month == DateTime.Now.Month
                             && s.UpdatedDate.Value.Year == DateTime.Now.Year
                             && s.DeleteFlag == 0
                             //&& s.IsUpdateSpeed == true
                             select s).OrderBy(x => x.UpdateCount).Select(x
                             => new SpeedLimit()
                             {
                                 Lat = x.Lat,
                                 Lng = x.Lng,
                                 ProviderType = x.ProviderType,
                                 UpdatedDate = x.UpdatedDate
                             });

                string messTotal = @$"Có {query.Count()} " + "điểm đã được cập nhật vận tốc giới hạn trong ngày " + $"{DateTime.Now.ToString("dd/MM/yyyy")}";

                var re = await query.Take(limit ?? 1000).ToListAsync();

                return Result<object>.Success(null, await query.CountAsync(), messTotal);
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.ToString());
            }
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

                // Nếu Update Date mà < 6 tháng thì không cho hiển thị ra
                DateTime UpdDateAllow = (DateTime)DateTime.Now.AddMonths(-6).Date;

                // Lấy ra 1 segmetId có 1 tuyến riêng
                SpeedLimit itemGet = await Db.SpeedLimits.Where(
                                x => x.DeleteFlag == 0
                                && x.IsUpdateSpeed == false)
                                .OrderBy(x => x.UpdateCount).ThenBy(x => x.SegmentID).ThenBy(x => x.Lat)
                                .FirstOrDefaultAsync();

                var listQuery = await Db.SpeedLimits.Where(
                                x => x.DeleteFlag == 0
                                && x.IsUpdateSpeed == false
                                && x.SegmentID == itemGet.SegmentID
                                && x.Direction == itemGet.Direction)
                                .OrderBy(x => x.UpdateCount).ThenBy(x => x.Lat).ToListAsync();

                var lstShow = new List<SpeedProvider>();
                var lstRe = new List<SpeedLimit>();

                foreach (SpeedLimit item in listQuery)
                {
                    if (item.UpdatedDate == null || (UpdDateAllow - item.UpdatedDate).Value.Days < 1)
                    {
                        SpeedLimit itemAdd = new SpeedLimit();
                        itemAdd.SegmentID = item.SegmentID;
                        itemAdd.Lat = item.Lat;
                        itemAdd.Lng = item.Lng;
                        itemAdd.ProviderType = item.ProviderType;
                        itemAdd.Position = item.Position;
                        itemAdd.Direction = item.Direction;

                        if (item.Position.Trim().IndexOf("S") > -1 && item.Direction == 0)
                        {
                            itemAdd.Sort = -1;
                        }
                        else if (item.Position.Trim().IndexOf("M") > -1 && item.Direction == 0)
                        {
                            itemAdd.Sort = Convert.ToInt32(item.Position.Substring((item.Position.LastIndexOf("-")) + 1, item.Position.Length - (item.Position.LastIndexOf("-")) - 1));
                        }
                        else if (item.Position.Trim().IndexOf("E") > -1 && item.Direction == 0)
                        {
                            itemAdd.Sort = 999;
                        }
                        
                        else if (item.Position.Trim().IndexOf("BS") > -1 && item.Direction == 1)
                        {
                            itemAdd.Sort = 9999;
                        }
                        else if (item.Position.Trim().IndexOf("BM") > -1 && item.Direction == 1)
                        {
                            itemAdd.Sort = 10000 + Convert.ToInt32(item.Position.Substring((item.Position.LastIndexOf("-")) + 1, item.Position.Length - (item.Position.LastIndexOf("-")) - 1));
                        }
                        else if (item.Position.Trim().IndexOf("BE") > -1 && item.Direction == 1)
                        {
                            itemAdd.Sort = 99999;
                        }
                        
                        lstRe.Add(itemAdd);
                    }
                }

                var re = lstRe.Take(limit ?? 100).OrderBy(x => x.Sort);
                foreach (SpeedLimit itemspeed in re)
                {
                    var obj = await Db.SpeedLimits
                       .Where(x => x.Lat == (decimal)itemspeed.Lat
                       && x.Lng == itemspeed.Lng
                       && x.SegmentID == (decimal)itemspeed.SegmentID
                       && x.Direction == itemspeed.Direction
                       && x.ProviderType == itemspeed.ProviderType
                       && x.Position.Trim() == itemspeed.Position.Trim()
                       && x.PointError == false).FirstOrDefaultAsync();

                    if(obj != null)
                    {
                        obj.IsUpdateSpeed = true;
                        Db.Entry(obj).State = EntityState.Modified;
                        lstShow.Add(new SpeedProvider() { Lat = itemspeed.Lat, Lng = itemspeed.Lng, ProviderType = itemspeed.ProviderType, SegmentID = itemspeed.SegmentID, Direction = itemspeed.Direction });
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

        public async Task<IResult<object>> UpdateSpeedLimitPush(SpeedLimitParams speedLimitParams)
        {

            foreach (SpeedLimitPush item in speedLimitParams.data)
            {
                await UpdateSpeedLimitPush(item);
            }

            return Result<object>.Success(speedLimitParams);

        }

        public async Task<IResult<object>> UpdloadSpeedProvider(List<SpeedProviderUpLoadVm> speedProviderUpLoad)
        {
            foreach (SpeedProviderUpLoadVm item in speedProviderUpLoad)
            {
                await UpdLoadSpeedProvider(item);
            }

            return Result<object>.Success(new SpeedProviderUpLoadVm(), 0, Message.SUCCESS);
        }

        #region private method

        private async Task<IResult<object>> UpdateSpeedLimitPush(SpeedLimitPush speedLimit)
        {
            try
            {
                var lstUpd = await Db.SpeedLimits
                .Where(x => x.Lat == speedLimit.Lat
                && x.Lng == speedLimit.Lng
                && x.ProviderType == speedLimit.ProviderType
                && x.DeleteFlag == 0
                && x.IsUpdateSpeed == true
                && x.SegmentID == speedLimit.SegmentID
                && x.Direction == speedLimit.Direction
                && x.PointError == false).ToListAsync();

                if (lstUpd != null)
                {
                    foreach (SpeedLimit item in lstUpd)
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

        /// <summary>
        /// Tạo dữ liệu từ file upload .txt
        /// </summary>
        /// <param name="speedProvider"></param>
        /// <returns></returns>
        private async Task<IResult<object>> UpdLoadSpeedProvider(SpeedProviderUpLoadVm speedProvider)
        {
            try
            {
                var obj = await Db.SpeedLimits
               .Where(x => x.Lat == speedProvider.Lat && x.Lng == speedProvider.Lng
               && x.ProviderType == speedProvider.ProviderType).AsNoTracking().FirstOrDefaultAsync();

                // Đã có dữ liệu trong database thì bỏ qua luôn
                if (obj != null)
                    return Result<object>.Success(obj);

                obj = new SpeedLimit();
                obj.Lat = speedProvider.Lat;
                obj.Lng = speedProvider.Lng;
                obj.SegmentID = speedProvider.SegmentID;
                obj.ProviderType = 1;// Đang chỉ có dữ liệu từ Navitel
                obj.DeleteFlag = 0;
                obj.CreatedDate = DateTime.Now;
                obj.CreatedBy = "UploadFile";
                obj.UpdateCount = 0;
                obj.PointError = false;
                Db.SpeedLimits.Add(obj);

                await Db.SaveChangesAsync();
                return Result<object>.Success(obj);
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.ToString());
            }

        }

        #endregion

    }

}
