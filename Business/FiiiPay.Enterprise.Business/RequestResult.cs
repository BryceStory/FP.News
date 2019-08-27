using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Business
{
    public class RequestResult
    {
        public RequestResult(bool status)
        {
            Status = status;
        }
        public RequestResult(bool status, string filename, string key)
        {
            Status = status;
            MessageResFileName = filename;
            MessageResKey = key;
        }
        public bool Status { get; }
        public string MessageResFileName { get; }
        public string MessageResKey { get; }
    }
    public class RequestResult<T>
    {
        public RequestResult(bool status)
        {
            Status = status;
        }
        public RequestResult(bool status, T data)
        {
            Status = status;
            Data = data;
        }
        public RequestResult(bool status, string filename, string key)
        {
            Status = status;
            MessageResFileName = filename;
            MessageResKey = key;
        }
        public RequestResult(bool status, string filename, string key,T data)
        {
            Status = status;
            MessageResFileName = filename;
            MessageResKey = key;
            Data = data;
        }
        public bool Status { get; }
        public string MessageResFileName { get; }
        public string MessageResKey { get; }
        public T Data { get; set; }
    }
}
