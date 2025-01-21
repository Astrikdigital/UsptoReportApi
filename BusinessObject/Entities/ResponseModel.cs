using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class ResponseModel
    {
        public bool Response { get; set; }
        public string Message { get; set; } 
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public bool IsSuccess { get; set; } = true;
    }
}
