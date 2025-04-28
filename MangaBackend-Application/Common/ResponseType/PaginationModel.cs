using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaBackend_Application.Common.ResponseType
{
    public class PaginationModel
    {
        public int PerPage { get; set; }
        public int PageNo { get; set; }
        public int? TotalCount1 { get; set; } = 0;
        public int? TotalCount2 { get; set; } = 0;
        public int? TotalPages { get; set; }
        public int? TotalCount { get; set; }
        public dynamic Data { get; set; }


    }
}
