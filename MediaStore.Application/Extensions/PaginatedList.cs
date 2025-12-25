using MediaStore.Application.Common.Responses;
using Microsoft.EntityFrameworkCore;

namespace MediaStore.Application.Extensions
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int PageRange { get; private set; } 
        public int PageSize { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, int pageRange = 2)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageRange = pageRange;
            PageSize = pageSize;

            AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public IEnumerable<int> VisiblePages
        {
            get
            {
                int startPage = Math.Max(1, PageIndex - PageRange);
                int endPage = Math.Min(TotalPages, PageIndex + PageRange);
                return Enumerable.Range(startPage, endPage - startPage + 1);
            }
        }

        public static async Task<PaginatedResponse<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, int pageRange = 2)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var paginated = new PaginatedList<T>(items, count, pageIndex, pageSize, pageRange);
            return new PaginatedResponse<T>()
            {
                Items = paginated.ToList(),
                HasPreviousPage = paginated.HasPreviousPage,
                HasNextPage = paginated.HasNextPage,
                VisiblePages = paginated.VisiblePages,
                TotalPages = paginated.TotalPages,
                PageIndex = pageIndex,
                PageRange = pageRange,
                PageSize = pageSize
            };
        }
    }
}