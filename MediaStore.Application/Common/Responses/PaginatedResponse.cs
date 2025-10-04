namespace MediaStore.Application.Common.Responses
{
    public class PaginatedResponse<T>
    {
        public int TotalPage { get; set; }
        public List<T>? Items { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public IEnumerable<int>? VisiblePages { get; set; }
    }
}
