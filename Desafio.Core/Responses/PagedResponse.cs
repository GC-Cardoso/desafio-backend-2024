using System.Text.Json.Serialization;

namespace Desafio.Core.Responses
{
    public class PagedResponse<TData> : Response<TData>
    {
        [JsonConstructor]
        public PagedResponse(
            TData? data,
            int totalCount,
            int currentPage = 1,
            int pageSize = Configuration.DEFAULT_PAGE_SIZE
            ) : base(data) 
        {
            this.data = data;
            this.totalCount = totalCount;
            this.currentPage = currentPage;
            this.pageSize = pageSize;
        }

        public PagedResponse(
            TData data,
            int code = Configuration.DEFAULT_STATUS_CODE,
            string? message = null
            ) : base(data, code, message)
        { 
        }

        public int currentPage { get; set; }
        public int totalPages => (int)Math.Ceiling(totalCount / (double)pageSize);
        public int pageSize { get; set; } = Configuration.DEFAULT_PAGE_SIZE;
        public int totalCount { get; set; }
    }
}
