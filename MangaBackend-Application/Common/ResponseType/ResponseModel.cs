using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaBackend_Application.Common.ResponseType
{
    public partial class ResponseModel
    {
        public string? ResponseMessage { get; set; }
        public bool IsError { get; set; }
        public dynamic DataModel { get; set; }
        public int statusCode { get; set; }
        public bool Success { get; set; }
    }
}
