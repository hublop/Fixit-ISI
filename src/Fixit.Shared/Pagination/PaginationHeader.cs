using Microsoft.AspNetCore.Http;

namespace Fixit.Shared.Pagination
{
    public static class PaginationHeader
    {
        public static void AddPagination(this HttpResponse response, PagingHeader pagingHeader)
        {
            response.Headers.Add("X-Pagination", pagingHeader.ToJson());
            response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
        }
    }
}