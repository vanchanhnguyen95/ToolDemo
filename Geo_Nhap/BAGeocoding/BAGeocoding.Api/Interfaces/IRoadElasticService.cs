using BAGeocoding.Api.Models;
using BAGeocoding.Api.Models.PBD;

namespace BAGeocoding.Api.Interfaces;

public interface IRoadElasticService
{
    /* shapeid: 0: toàn bộ, 1: Điểm, 2: Đường
        * provinceID: 0: toàn bộ, 1->63: các tỉnh
     */
    Task<string> BulkAsyncByProvince(List<BGCElasticRequestCreate> roadPushs, int shapeid, int provinceID);
    Task<string> BulkAsyncByProvinceNoShape(List<BGCElasticRequestCreate> roadPushs, int provinceID);

    //Task<ResultMerge<object>> Search(double lat, double lng, string distance, int size, string keyword, int shapeid);
    //Task<ResultMerge<object>> Search3(double lat, double lng, string distance, int size, string keyword, int shapeid);
    Task<ResultMerge<object>> SearchV4(double lat, double lng, double distance, int size, string keyword, int shapeid);
    Task<ResultMerge<object>> Add2Geo(string? keyStr, string? lanStr);
}
