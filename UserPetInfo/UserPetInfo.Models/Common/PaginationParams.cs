using System;
using System.Collections.Generic;
using System.Text;

namespace UserPetInfo.Models.Common
{
    public class PaginationParams
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
