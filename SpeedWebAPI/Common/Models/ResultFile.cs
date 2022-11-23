namespace SpeedWebAPI.Common.Models
{
    public interface IResultFile
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
    public class ResultFile : IResultFile
    {
        public bool Status { get; set; } = true;
        public string Message { get; set; }
        public string FilePath { get; set; }
        public int? Code { get; set; }
        public object Data { get; set; }

        public static ResultFile Error(string filePath, string message = "") => new ResultFile()
        {
            FilePath = filePath,
            Message = message,
            Status = false
        };

        public static ResultFile Success(object data, string filePath, string message = "") => new ResultFile()
        {
            Data = data,
            FilePath = filePath,
            Message = message
        };
    }

    /// <summary>
    /// Mọi kết quả trả về của Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResultFile<T>
    {
        public string FilePath { get; set; }

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
    }

    public class ResultFile<T> : IResultFile<T>
    {
        public bool Status { get; set; } = true;
        public T Data { get; set; }

        public string FilePath { get; set; }

        public string Message { get; set; }

        public static ResultFile<T> Success(T data, string filePath, string message = "", T dataTotal = default)
        {
            return new ResultFile<T>()
            {
                FilePath = filePath,
                Message = message,
                Data = data,
            };
        }

        public static ResultFile<T> Error(string filePath, string message = "") => new ResultFile<T>()
        {
            FilePath = filePath,
            Status = false,
            Message = message,
        };
    }
}
