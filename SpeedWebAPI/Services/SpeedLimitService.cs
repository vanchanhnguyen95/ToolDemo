using Microsoft.EntityFrameworkCore;
using SpeedWebAPI.Common.Models;
using SpeedWebAPI.Infrastructure;
using SpeedWebAPI.Models;
using SpeedWebAPI.Services.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWebAPI.Services
{
    public interface ISpeedLimitService : IBaseService<SpeedLimit>
    {
        Task<object> GetSpeedProviders(int? limit);
        Task<object> Gets(string key, int offset, int limit, string sortField, bool isAsc);
        Task<IResult<object>> Save(SpeedLimit form);
        Task<IResult<object>> Update(SpeedLimit form);
    }

    public class SpeedLimitService : BaseService<SpeedLimit, ApplicationDbContext>, ISpeedLimitService // : ISpeedLimitService
    {
        public SpeedLimitService(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<object> GetSpeedProviders(int? limit)
        {
            var query = from s in Db.SpeedLimits
                        select s;

            var re = await query.Take(limit??100).ToListAsync();
            return Result<object>.Success(re, await query.CountAsync());
        }

        public async Task<object> Gets(string key, int offset, int limit, string sortField, bool isAsc)
        {
            var query = from s in Db.SpeedLimits
                        select s;

            //if (!string.IsNullOrEmpty(key))
            //{
            //    query = query.Where(x => x.Name.Contains(key) || x.Code.Contains(key));
            //}

            //switch (sortField ?? "")
            //{
            //    case "name":
            //        query = isAsc ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name);
            //        break;
            //    default:
            //        query = isAsc ? query.OrderBy(c => c.Id) : query.OrderByDescending(c => c.Id);
            //        break;
            //}

            var re = await query.Skip(offset).Take(limit).ToListAsync();
            return Result<object>.Success(re, await query.CountAsync());
        }

        public async Task<IResult<object>> Save(SpeedLimit form)
        {
            SpeedLimit obj;
            if (form.Id > 0)
            {
                obj = await Db.SpeedLimits.FindAsync(form.Id);
                obj.UpdateCount++;
                //obj.UpdatedBy = User.UserId;
                obj.UpdatedDate = DateTime.Now;
            }
            else
            {
                obj = new SpeedLimit();
                Db.SpeedLimits.Add(obj);
                //obj.CreatedBy = User.UserId;
                obj.CreatedDate = DateTime.Now;
            }

            obj.DeleteFlag = form.DeleteFlag;

            await Db.SaveChangesAsync();

            return Result<object>.Success(obj);
        }

        public async Task<IResult<object>> Update(SpeedLimit form)
        {
            var obj = await Db.SpeedLimits.Where(x => x.Id == form.Id).FirstOrDefaultAsync();
            if (obj != null)
            {
                //obj.UpdatedBy = User.UserId;
                obj.UpdatedDate = DateTime.Now;
                obj.UpdateCount++;

                await Db.SaveChangesAsync();
                return Result<object>.Success(obj);
            }
            return Result<object>.Error("Dữ liệu không chính xác!");
        }
    }

}
