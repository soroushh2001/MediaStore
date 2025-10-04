namespace MediaStore.Application.Specifications
{
    public class BaseQueryParameters
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int PageRange { get; set; } = 2;
        public string? OrderBy { get; set; } = "Desc";
        public string? SortBy { get; set; } = "ModifiedDate";
    }
}
