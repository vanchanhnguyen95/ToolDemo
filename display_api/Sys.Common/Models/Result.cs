using System;
using System.Collections.Generic;

namespace Sys.Common.Models
{
    public class Result<T>
    {
        public IList<string> Messages { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; }
        public string StrackTrace { get; set; }
        public int TotalCount { get; set; }

        public Result()
        {
            Messages = new List<string>();
        }
    }


    public class ResultPaging<T>
    {
        public IList<string> Messages { get; set; }
        public T Items { get; set; }
        public bool Success { get; set; }
        public string StrackTrace { get; set; }
        public MetaData MetaData { get; set; }

        public ResultPaging()
        {
            Messages = new List<string>();
        }
    }

    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasNext
        {

            get { return PageSize * (CurrentPage - 1) < TotalCount; }
        }

        public bool HasPrevious
        {
            get { return PageSize * (CurrentPage - 1) > 0; }
        }

    }
}