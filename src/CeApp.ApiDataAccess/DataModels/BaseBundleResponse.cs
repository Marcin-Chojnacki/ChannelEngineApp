using System.Collections.Generic;

namespace ApiDataAccess.DataModels
{
    internal abstract class BaseBundleResponse<T> : BaseResponse
    {
        public IEnumerable<T> Content { get; set; }

        public int Count { get; set; }

        public int TotalCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
