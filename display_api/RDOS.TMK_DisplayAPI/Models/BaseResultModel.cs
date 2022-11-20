using System;

namespace RDOS.TMK_DisplayAPI.Models
{
    public interface IResponseModel
    {

    }
    public class BaseResultModel : IResponseModel
    {
        public int ObjectId { get; set; } = 0;
        public Guid ObjectGuidId { get; set; }
        public int Code { get; set; } = 0;
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = true;
        public bool Succeeded { get; set; }
        public object Data { get; set; }
        public BaseResultModel() { }

        public BaseResultModel(bool success, string message)
        {
            Message = message;
            Succeeded = success;
        }

        #region Success
        public static BaseResultModel Success(string message)
        {
            return new BaseResultModel(true, message);
        }

        public static BaseResultModel Success(object data)
        {
            return new BaseResultModel { Succeeded = true, Data = data };
        }
        #endregion

        #region Failed
        public static BaseResultModel Fail(string message)
        {
            return new BaseResultModel(false, message);
        }
        #endregion
    }
}