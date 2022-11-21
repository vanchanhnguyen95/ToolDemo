using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWebAPI.Services.Base
{
    public interface IBaseService<TModel>
    {
        TModel Get(object id);
        Task<TModel> GetAsync(object id);

        IQueryable<TModel> GetAll();

        void Add(TModel obj);

        void Delete(TModel obj);

        Task Save();
    }

    public abstract class BaseService<TModel, TDbContext> : IBaseService<TModel>
        where TModel : class
        where TDbContext : DbContext
    {
        public readonly TDbContext Db;
        //public readonly ICachingHelper CachingHelper;
        //public readonly IUserInfo User;

        public BaseService(TDbContext db)
        {
            Db = db;
        }

        public virtual TModel Get(object id)
        {
            return Db.Set<TModel>().Find(id);
        }

        public virtual async Task<TModel> GetAsync(object id)
        {
            return await Db.Set<TModel>().FindAsync(id);
        }

        public virtual IQueryable<TModel> GetAll()
        {
            return Db.Set<TModel>().Select(c => c);
        }

        public virtual void Add(TModel obj)
        {
            Db.Set<TModel>().AddAsync(obj);
        }

        public virtual async Task Save()
        {
            await Db.SaveChangesAsync();
        }

        public virtual void Delete(TModel obj)
        {
            Db.Set<TModel>().Remove(obj);
        }
    }
}
