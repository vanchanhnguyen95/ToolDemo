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
    }

    #endregion

    public class SpeedLimitService : BaseService<SpeedLimit, ApplicationDbContext>, ISpeedLimitService
    {
        public SpeedLimitService(ApplicationDbContext db) : base(db)
        {
        }
        public async Task<object> GetSpeedProviders(int? limit)
        {
            try
            {
                if (limit == null || limit > 100)
                    limit = 100;

                var query = (from s in Db.SpeedLimits where s.DeleteFlag == 0 && s.PointError == false
                             select s).OrderBy(x => x.UpdateCount).Select(x
                             => new SpeedLimit()
                             {
                                 Lat = x.Lat,
                                 Lng = x.Lng,
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

                //var queryResult = (from s in query
                //                   select s => {
                //                       sp => new SpeedProvider
                //                       (
                //                        Lat = sp.Lat,
                //                        Lng = x.Lng,
                //                        ProviderType = x.ProviderType,
                //                       )
                //                   });

                //var re = await query.Take(limit??100).ToListAsync();
                var re = lstRe.Take(limit??100).ToList();
                return Result<object>.Success(re, await query.CountAsync(), Message.SUCCESS);
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

            //try
            //{
            //    if(speedLimitParams.data.Any())
            //    {
            //        foreach (SpeedLimitPush item in speedLimitParams.data)
            //        {
            //            await UpdateSpeedLimitPush(item);
            //        }
            //    }
            //    return Result<object>.Success(speedLimitParams);
            //}
            //catch (Exception ex)
            //{
            //    return Result<object>.Error(ex.ToString());
            //}
        }

        //public async Task<IResult<object>> UpdloadSpeedProvider(List<SpeedProviderUpLoadVm> speedProviderUpLoad)
        //{
        //    try
        //    {
        //        foreach (SpeedProviderUpLoadVm item in speedProviderUpLoad)
        //        {
        //            await UpdLoadSpeedProvider(item);
        //        }

        //        return Result<object>.Success(new SpeedProviderUpLoadVm(), 0, Message.SUCCESS);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result<object>.Error(ex.ToString());
        //    }
        //}

        public async Task<IResult<object>> UpdloadSpeedProvider(List<SpeedProviderUpLoadVm> speedProviderUpLoad)
        {
            foreach (SpeedProviderUpLoadVm item in speedProviderUpLoad)
            {
                await UpdLoadSpeedProvider(item);
            }

            return Result<object>.Success(new SpeedProviderUpLoadVm(), 0, Message.SUCCESS);

            //try
            //{
            //    foreach (SpeedProviderUpLoadVm item in speedProviderUpLoad)
            //    {
            //        await UpdLoadSpeedProvider(item);
            //    }

            //    return Result<object>.Success(new SpeedProviderUpLoadVm(), 0, Message.SUCCESS);
            //}
            //catch (Exception ex)
            //{
            //    return Result<object>.Error(ex.ToString());
            //}
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
                var obj = await Db.SpeedLimits
                .Where(x => x.Lat == speedLimit.Lat
                && x.Lng == speedLimit.Lng
                && x.ProviderType == speedLimit.ProviderType
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
                    obj = new SpeedLimit();
                    obj.Lat = speedLimit.Lat;
                    obj.Lng = speedLimit.Lng;
                    obj.ProviderType = 1;
                    obj.DeleteFlag = 0;
                    obj.CreatedDate = DateTime.Now;
                    obj.UpdateCount = 0;
                    obj.PointError = speedLimit.PointError;
                    Db.SpeedLimits.Add(obj);
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
        private async Task<IResult<object>> UpdLoadSpeedProvider(SpeedProviderUpLoadVm speedProvider)
        {
            try
            {
                var obj = await Db.SpeedLimits
               .Where(x => x.Lat == speedProvider.Lat && x.Lng == speedProvider.Lng).AsNoTracking().FirstOrDefaultAsync();

                // Đã có dữ liệu trong database thì bỏ qua luôn
                if (obj != null)
                    return Result<object>.Success(obj);

                obj = new SpeedLimit();
                obj.Lat = speedProvider.Lat;
                obj.Lng = speedProvider.Lng;
                obj.SegmentID = speedProvider.SegmentID;
                obj.Note = speedProvider.Note;
                obj.ProviderType = 1;
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
