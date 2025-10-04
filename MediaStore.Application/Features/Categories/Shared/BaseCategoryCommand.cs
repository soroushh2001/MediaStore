namespace MediaStore.Application.Features.Categories.Shared
{
    public class BaseCategoryCommand 
    {
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
    }
}
