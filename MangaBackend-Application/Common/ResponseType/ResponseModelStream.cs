using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaBackend_Application.Common.ResponseType
{
    public class ResponseModelStream
    {
        public string? ResponseMessage { get; set; }
        public bool IsError { get; set; }
        public MemoryStream DataModel { get; set; }
        public int StatusCode { get; set; }
    }
}
