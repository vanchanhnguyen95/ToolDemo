namespace SpeedWebAPI.Common.Constants
{
    public class Message
    {
        public const string SUCCESS = @"Success";
        public const string FAIL = @"Fail";
        public const string GET_SPEED_SUCCESS = @"Lấy thông tin dữ liệu thành công";
    }

    public static class ErrMessage
    {
        public const string UPD_FILE_FORMAT_TXT = @"File upload là file có định dạng *.txt";
        public const string NOT_FIND_UPD = @"Không tìm thấy File upload";
        public const string GET_DATA_FILE_TXT = @"Có lỗi khi lấy dữ liệu từ file input";
        public const string NO_DATA_SPEED = @"Chưa có dữ liệu về tốc độ";

        public const string UPD_FILE_FORMAT_SHP = @"File upload là file có định dạng *.shp";
        public const string UPD_NUM_LINE_SHP = @"Chỉ được upload 500 line";
    }
}
