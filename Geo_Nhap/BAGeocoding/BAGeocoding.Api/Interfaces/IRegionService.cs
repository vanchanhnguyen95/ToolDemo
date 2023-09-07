using BAGeocoding.Api.Models;
using BAGeocoding.Entity.MapObj;
using BAGeocoding.Entity.Public;

namespace BAGeocoding.Api.Interfaces
{
    public interface IRegionService
    {
        Task<string> BulkAsyncProvince();
        Task<string> BulkAsyncDistrict();
        //Task<string> GetKeySearch(string keyword);
        //Task<Result<object>> GetBAGSearchKey(string keyword);
        //Task<Result<object>> GetProvince(string keyword);
        public Task<RPBLAddressResultV2> GeoByAddress(string keyStr, string lanStr);
        Task<Result<object>> GeoByAddressAsync(string? keyStr, string? lanStr);
    }
}
