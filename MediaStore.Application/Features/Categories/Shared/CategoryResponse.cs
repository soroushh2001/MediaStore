namespace MediaStore.Application.Features.Categories.Shared
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
