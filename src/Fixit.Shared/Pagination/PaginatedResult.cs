using System;
using System.Collections.Generic;
using System.Linq;

namespace Fixit.Shared.Pagination
{
    public class PaginatedResult<T>
    {
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T> Result { get; set; }
        public int TotalPages { get; set; }

        public PaginatedResult(IQueryable<T> source, int? pageNumber, int? pageSize)
        {
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                PageNumber = pageNumber.Value;
                PageSize = pageSize.Value;
            }

            if (PageNumber <= 0 || PageSize <= 0)
            {
                AssignDefaultsParams();
            }

            if (source == null)
            {
                TotalItems = 0;
                Result = new List<T>();
            }
            else
            {
                Result = source.Skip(PageSize * (PageNumber - 1))
                    .Take(PageSize)
                    .ToList();
                TotalItems = source.Count();
            }

            TotalPages = (int)Math.Ceiling(TotalItems / (double)PageSize);
        }

        public PaginatedResult() { }

        public PagingHeader GetHeader()
        {
            return new PagingHeader(TotalItems, PageNumber, PageSize, TotalPages);
        }

        private void AssignDefaultsParams()
        {
            PageNumber = 1;
            PageSize = 100;
        }

    }
}