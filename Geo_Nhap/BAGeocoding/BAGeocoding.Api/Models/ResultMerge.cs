namespace BAGeocoding.Api.Models
{
    public interface IResultMerge<T>
    {
        // Mã trả về thành công hoặc lỗi.
        // Nếu trả về mã code "ok" nghĩa là thành công.
        // Ngược lại, nếu lỗi thì trả về mã lỗi tương ứng.
        string? errorcode { get; set; }

        // Nội dung của mã lỗi(nếu có).
        bool? state { get; set; }

        // Danh sách "place" được tìm thấy, nếu lỗi thì result là "null".
        T? data { get; set; }
    }

    public class ResultMerge<T> : IResultMerge<T>
    {
        public string? errorcode { get; set; } = null;
        public bool? state { get; set; } = null;

        public T? data { get; set; } = default(T?);
        
        //public string? ProcessState { get; set; } = null;

        public static ResultMerge<T> Success(T data,string errorcode = "", bool state = false)
        {
            return new ResultMerge<T>()
            {
                errorcode = errorcode,
                state = state,
                data = data,
                //ProcessState = processState,
            };
        }

        public static ResultMerge<T> Error(T data, string errorcode, bool state = false) => new ResultMerge<T>()
        {
            errorcode = errorcode,
            state = state,
            data = data,
        };
    }
}
