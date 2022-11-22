using Microsoft.EntityFrameworkCore;
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
    public interface ISpeedLimitService : IBaseService<SpeedLimit>
    {
        Task<object> GetSpeedProviders(int? limit);
        //Task<IResult<object>> UpdateListSpeedProvider(List<SpeedProviderVm> speedLimits);

        Task<IResult<object>> UpdateSpeedLimitPush(SpeedLimitParams speedLimitParams);
        Task<IResult<object>> Save(SpeedLimitPush speedLimit);
    }

    public class SpeedLimitService : BaseService<SpeedLimit, ApplicationDbContext>, ISpeedLimitService // : ISpeedLimitService
    {
        public SpeedLimitService(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<object> GetSpeedProviders(int? limit)
        {
            try
            { 
                var query = (from s in Db.SpeedLimits where s.DeleteFlag == 0
                            select s).Select(x
                            => new SpeedLimitPush()
                            {
                                Lat = x.Lat,
                                Lng = x.Lng,
                                ProviderType = x.ProviderType
                            });
                var re = await query.Take(limit??100).ToListAsync();
                return Result<object>.Success(re, await query.CountAsync());
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.ToString());
            }
        }

        public async Task<IResult<object>> Save(SpeedLimitPush speedLimit)
        {
            try
            {
                await UpdateSpeedProvider(speedLimit);

                return Result<object>.Success(speedLimit);
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.ToString());
            }
        }

        private async Task<IResult<object>> UpdateSpeedProvider(SpeedLimitPush speedLimit)
        {
            var obj = await Db.SpeedLimits
                .Where(x => x.Lat == speedLimit.Lat &&  x.Lng == speedLimit.Lng).AsNoTracking().FirstOrDefaultAsync();
            if (obj != null)
            {
                obj.MinSpeed = speedLimit.MinSpeed;
                obj.MaxSpeed = speedLimit.MaxSpeed;
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
                Db.SpeedLimits.Add(obj);
            }

            await Db.SaveChangesAsync();
            return Result<object>.Success(obj);
        }

        private async Task<IResult<object>> UpdateSpeedLimitPush(SpeedLimitPush speedLimit)
        {
            var obj = await Db.SpeedLimits
                .Where(x => x.Lat == speedLimit.Lat && x.Lng == speedLimit.Lng).AsNoTracking().FirstOrDefaultAsync();
            if (obj != null)
            {
                obj.MinSpeed = speedLimit.MinSpeed;
                obj.MaxSpeed = speedLimit.MaxSpeed;
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
                Db.SpeedLimits.Add(obj);
            }

            await Db.SaveChangesAsync();
            return Result<object>.Success(obj);
        }
        public async Task<IResult<object>> UpdateSpeedLimitPush(SpeedLimitParams speedLimitParams)
        {
            try
            {
                if(speedLimitParams.data.Any())
                {
                    foreach (SpeedLimitPush item in speedLimitParams.data)
                    {
                        await UpdateSpeedLimitPush(item);
                    }

                    return Result<object>.Success(speedLimitParams);
                }
                return Result<object>.Success(speedLimitParams);
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.ToString());
            }
        }
    }

}
