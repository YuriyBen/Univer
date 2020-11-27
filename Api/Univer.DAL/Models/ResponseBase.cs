using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models
{
    public class ResponseBase<T>
    {
        public ResponeStatusCodes Status { get; set; } = ResponeStatusCodes.Ok;
        public T Data { get; set; }
    }

    //public class SimpleResponse<T>
    //{
    //    public string Message { get; set; }

    //    public T Result { get; set; }
    //}

}
