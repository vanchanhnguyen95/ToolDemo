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
        Task<IResult<object>> UpdateListSpeedProvider(List<SpeedProviderVm> speedLimits);
        Task<IResult<object>> Save(SpeedProviderVm speedLimit);
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
                            => new SpeedProviderGetVm()
                            {
                                Lat = x.Lat,
                                Long = x.Long,
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

        public async Task<IResult<object>> UpdateListSpeedProvider(List<SpeedProviderVm> speedLimits)
        {
            try
            {
                foreach (SpeedProviderVm speedLimit in speedLimits)
                {
                    await UpdateSpeedProvider(speedLimit);
                }

                return Result<object>.Success(speedLimits);
            }
            catch (Exception ex)
            {
                return Result<object>.Error(ex.ToString());
            }
        }

        public async Task<IResult<object>> Save(SpeedProviderVm speedLimit)
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

        private async Task<IResult<object>> UpdateSpeedProvider(SpeedProviderVm speedLimit)
        {
            var obj = await Db.SpeedLimits
                .Where(x => x.Lat == speedLimit.Lat &&  x.Long == speedLimit.Long).AsNoTracking().FirstOrDefaultAsync();
            if (obj != null)
            {
                obj.MinSpeed = speedLimit.MinSpeed;
                obj.MaxSpeed = speedLimit.MaxSpeed;
                obj.UpdateCount++;
                obj.UpdatedDate = DateTime.Now;
                obj.UpdatedBy = $"Upd numbers {obj.UpdateCount?.ToString()}";

                Db.Entry(obj).State = EntityState.Modified;
                //await Db.SaveChangesAsync();
                //return Result<object>.Success(obj);
            }
            else
            {
                obj = new SpeedLimit();
                obj.Lat = speedLimit.Lat;
                obj.Long = speedLimit.Long;
                obj.ProviderType = 1;
                obj.DeleteFlag = 0;
                obj.CreatedDate = DateTime.Now;
                obj.UpdateCount = 0;
                Db.SpeedLimits.Add(obj);
            }

            await Db.SaveChangesAsync();
            return Result<object>.Success(obj);
        }

    }

}
