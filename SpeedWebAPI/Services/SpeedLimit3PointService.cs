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
    public interface ISpeedLimit3PointService : IBaseService<SpeedLimit3Point>
    {
        /// <summary>
        /// Lấy thông tin SpeedLimit
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<object> GetSpeedProviders3Point(int? limit);

        /// <summary>
        /// Update SpeedLimit từ api Push
        /// </summary>
        /// <param name="speedLimitParams"></param>
        /// <returns></returns>
        Task<IResult<object>> UpdateSpeedLimitPush3Point(SpeedLimitParams speedLimitParams);

        /// <summary>
        /// Updload SpeedProvider từ file text
        /// </summary>
        /// <param name="speedProviderUpLoad"></param>
        /// <returns></returns>
        Task<IResult<object>> UpdloadSpeedProvider3Point(List<SpeedProviderUpLoadVm> speedProviderUpLoad);

        Task<object> GetSpeedCurrent3Point(int? limit);
    }

    #endregion

    public class SpeedLimit3PointService : BaseService<SpeedLimit3Point, ApplicationDbContext>, ISpeedLimit3PointService
    {
        public SpeedLimit3PointService(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<object> GetSpeedCurrent3Point(int? limit)
        {
            try
            {
                if (limit == null || limit > 100)
                    limit = 100;

                var query = (from s in Db.SpeedLimit3Points
                             where s.DeleteFlag == 0 && s.PointError == false
                             && s.UpdatedDate.Value.Date == DateTime.Now.Date
                             && s.UpdatedDate.Value.Month == DateTime.Now.Month
                             && s.UpdatedDate.Value.Year == DateTime.Now.Year
                             select s).OrderBy(x => x.UpdateCount).Select(x
                             => new SpeedLimit()
                             {
                                 //Lat = x.Lat,
                                 //Lng = x.Lng,
                                 ProviderType = x.ProviderType,
                                 UpdatedDate = x.UpdatedDate
                             });


                string messTotal = @$"Có {query.Count()} " + "điểm đã được cập nhật vận tốc giới hạn trong ngày " + $"{DateTime.Now.ToString("dd/MM/yyyy")}";

                var re = query.AsQueryable();
                //var re = await query.Take(limit ?? 1000).ToListAsync();
                return Result<object>.Success(re, await query.CountAsync(), messTotal);
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.ToString());
            }
        }

        public async Task<object> GetSpeedProviders3Point(int? limit)
        {
            try
            {
                if (limit == null || limit > 100)
                    limit = 100;

                var query = (from s in Db.SpeedLimit3Points where s.DeleteFlag == 0 && s.PointError == false
                             select s).OrderBy(x => x.UpdateCount).Select(x
                             => new SpeedLimit()
                             {
                                 //Lat = x.Lat,
                                 //Lng = x.Lng,
                                 ProviderType = x.ProviderType,
                                 UpdatedDate = x.UpdatedDate
                             });

               
                // Nếu Update Date mà < 6 tháng thì không cho hiển thị ra
                DateTime UpdDateAllow = (DateTime)DateTime.Now.AddMonths(-6).Date;

                var lstRe = new List<SpeedProvider>();

                foreach (SpeedLimit item in query)
                {
                   if(item.UpdatedDate == null || (UpdDateAllow - item.UpdatedDate).Value.Days < 1)
                    {
                        lstRe.Add(new SpeedProvider() { Lat= item.Lat, Lng= item.Lng, ProviderType = item.ProviderType});
                    }
                }

                var re = lstRe.Take(limit??100).ToList();
                return Result<object>.Success(re, await query.CountAsync(), Message.SUCCESS);
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.ToString());
            }
        }

        public async Task<IResult<object>> UpdateSpeedLimitPush3Point(SpeedLimitParams speedLimitParams)
        {
            foreach (SpeedLimitPush item in speedLimitParams.data)
            {
                await UpdateSpeedLimitPush(item);
            }

            return Result<object>.Success(speedLimitParams);
        }


        public async Task<IResult<object>> UpdloadSpeedProvider3Point(List<SpeedProviderUpLoadVm> speedProviderUpLoad)
        {
            foreach (SpeedProviderUpLoadVm item in speedProviderUpLoad)
            {
                await UpdloadSpeedProvider3Point(item);
            }

            return Result<object>.Success(new SpeedProviderUpLoadVm(), 0, Message.SUCCESS);

            
        }

        #region private method

        /// <summary>
        /// Cập nhật dữ liệu từ phía service push vận tốc diowis hạn
        /// </summary>
        /// <param name="speedLimit"></param>
        /// <returns></returns>
        private async Task<IResult<object>> UpdateSpeedLimitPush(SpeedLimitPush speedLimit)
        {
            try
            {
                var obj = await Db.SpeedLimit3Points
                .Where(
                x =>
                //    => x.Lat == speedLimit.Lat
                //&& x.Lng == speedLimit.Lng
                x.ProviderType == speedLimit.ProviderType
                && x.PointError == false).AsNoTracking().FirstOrDefaultAsync();

                if (obj != null)
                {
                    obj.MinSpeed = speedLimit.MinSpeed;
                    obj.MaxSpeed = speedLimit.MaxSpeed;
                    obj.PointError = false;
                    obj.UpdateCount++;
                    obj.UpdatedDate = DateTime.Now;
                    obj.UpdatedBy = $"Upd numbers {obj.UpdateCount?.ToString()}";

                    Db.Entry(obj).State = EntityState.Modified;
                }
                else
                {
                    obj = new SpeedLimit3Point();
                    //obj.Lat = speedLimit.Lat;
                    //obj.Lng = speedLimit.Lng;
                    obj.ProviderType = 1;
                    obj.DeleteFlag = 0;
                    obj.CreatedDate = DateTime.Now;
                    obj.UpdateCount = 0;
                    obj.PointError = speedLimit.PointError;
                    Db.SpeedLimit3Points.Add(obj);
                }

                await Db.SaveChangesAsync();
                return Result<object>.Success(obj);
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
        private async Task<IResult<object>> UpdloadSpeedProvider3Point(SpeedProviderUpLoadVm speedProvider)
        {
            try
            {
                var obj = new SpeedLimit3Point();
                //     = await Db.SpeedLimit3Points
                //.Where(x => x.Lat == speedProvider.Lat && x.Lng == speedProvider.Lng).AsNoTracking().FirstOrDefaultAsync();

                // Đã có dữ liệu trong database thì bỏ qua luôn
                if (obj != null)
                    return Result<object>.Success(obj);

                obj = new SpeedLimit3Point();
                //obj.Lat = speedProvider.Lat;
                //obj.Lng = speedProvider.Lng;
                obj.SegmentID = speedProvider.SegmentID;
                //obj.Note = speedProvider.Note;
                obj.ProviderType = 1;
                obj.Position = speedProvider.Position;
                obj.DeleteFlag = 0;
                obj.CreatedDate = DateTime.Now;
                obj.CreatedBy = "UploadFile";
                obj.UpdateCount = 0;
                obj.PointError = false;
                Db.SpeedLimit3Points.Add(obj);

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
