﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWebAPI.Models.SpeedLimitPQA
{
    public class ImportResponse<T>
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }

        public static ImportResponse<T> GetResult(int code, string msg, T data = default(T))
        {
            return new ImportResponse<T>
            {
                Code = code,
                Msg = msg,
                Data = data
            };
        }
    }
}
