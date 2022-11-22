namespace SpeedWebAPI.Common.Models
{
    /// <summary>
    /// Dùng trong trường hợp trả về hàm void khi bình thường (không cần data trả về)
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Trạng thái thành công hay không
        /// </summary>
        bool Status { get; set; }

        /// <summary>
        /// Diễn giải cho lỗi (nếu có)
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Mã lỗi (nếu có)
        /// </summary>
        int? Code { get; set; }
    }

    /// <summary>
    /// Dùng trong trường hợp trả về một dữ liệu nào đó khác void khi bình thường
    /// </summary>
    public class Result : IResult
    {
        public bool Status { get; set; } = true;
        public string Message { get; set; }
        public int? Code { get; set; }
        public object Data { get; set; }
        public int? TotalRecord { get; set; }

        public static Result Error(string message = "") => new Result()
        {
            Message = message,
            Status = false
        };

        public static Result Success(object data, int totalRecord = 0, string message = "") => new Result()
        {
            Data = data,
            TotalRecord = totalRecord,
            Message = message
        };
    }

    /// <summary>
    /// Mọi kết quả trả về của Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResult<T>
    {
        /// <summary>
        /// Trạng thái thành công hay không
        /// </summary>
        bool Status { get; set; }

        /// <summary>
        /// Output trả về nếu thành công
        /// </summary>
        T Data { get; set; }

        /// <summary>
        /// Diễn giải cho lỗi (nếu có)
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Mã lỗi (nếu có)
        /// </summary>
        //int? TotalRecord { get; set; }
    }

    public class Result<T> : IResult<T>
    {
        public bool Status { get; set; } = true;
        public T Data { get; set; }

        //public string Error { get; set; }
        public string Message { get; set; }

        //public int? TotalRecord { get; set; }
        //public T DataTotal { get; set; }
        //public int? Code { get; set; }

        public static Result<T> Success(T data, int totalRecord = 0, string message = "", T dataTotal = default)
        {
            return new Result<T>()
            {
                Message = message,
                Data = data,
                //TotalRecord = totalRecord,
                //DataTotal = dataTotal
            };
        }

        //public static Result<T> Success(T data, int totalRecord = 0, string message = "", T dataTotal = default)
        //{
        //    return new Result<T>()
        //    {
        //        Data = data,
        //        Message = message,
        //        TotalRecord = totalRecord,
        //        DataTotal = dataTotal
        //    };
        //}

        public static Result<T> Error(string message = "") => new Result<T>()
        {
            Status = false,
            Message = message,
        };
    }
}
