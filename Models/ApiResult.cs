using System;
using System.Collections.Generic;
using System.Text;

namespace Test.CAP.Models
{
    public class ApiResult
    {
        public static ApiResult Error(string message)
        {
            return new ApiResult
            {
                IsSuccess = false,
                Message = message,
            };
        }

        public static ApiResult<T> Ok<T>(T data)
        {
            return new ApiResult<T>()
            {
                IsSuccess = true,
                Message = "",
                Data = data
            };
        }

        public static ApiResult<T> Ok<T>(T data, int count)
        {
            return new ApiResult<T>()
            {
                IsSuccess = true,
                Message = "",
                Data = data,
                Count = count
            };
        }

        public static ApiResult<T> Ok<T>(T data, bool hasNextPage)
        {
            return new ApiResult<T>()
            {
                IsSuccess = true,
                Message = "",
                Data = data,
                HasNextPage = hasNextPage
            };
        }

        public static ApiResult<T> Ok<T>(T data, int count, bool hasNextPage)
        {
            return new ApiResult<T>()
            {
                IsSuccess = true,
                Message = "",
                Data = data,
                Count = count,
                HasNextPage = hasNextPage
            };
        }

        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int Count { get; set; }
        public bool HasNextPage { get; set; }
    }

    public class ApiResult<T> : ApiResult
    {
        public new static ApiResult<T> Error(string message)
        {
            return new ApiResult<T>
            {
                IsSuccess = false,
                Message = message,
            };
        }

        public T Data { get; set; }
    }
}