using BAGeocoding.Api.Dto;
using BAGeocoding.Api.Models;
using BAGeocoding.Api.Models.PBD;
using static BAGeocoding.Api.Models.PBD.RoadName;

namespace BAGeocoding.Api.Interfaces
{
    public interface IRoadNameService
    {
        Task<string> BulkAsync(List<RoadNamePush> roadPushs);
        /* shapeid: 0: toàn bộ, 1: Điểm, 2: Đường
         * provinceID: 0: toàn bộ, 1->63: các tỉnh
         */
        Task<string> BulkAsyncByProvince(List<RoadNameMerge> roadPushs, int shapeid, int provinceID);

        // Tìm kiếm theo Tọa độ / Từ Khóa / Tọa độ và từ khóa
        Task<List<RoadNameOut>> GetDataSuggestion(double lat, double lng, string distance, int size, string keyword, int type);
        Task<List<RoadNameOut>> GetDataSuggestion(double lat, double lng, string distance, int size, string keyword);
        Task<List<RoadNameMerge>> GetDataSuggestionByProvince(double lat, double lng, string distance, int size, string keyword);
        Task<List<RoadNameOut>> GetRoadNameByProvince(string keyword, short provinceId);

        Task<ResultMerge<object>> Search(double lat, double lng, string distance, int size, string keyword, int shapeid);
    }
}
