namespace MediaStore.Application.Features.Brands.Shared
{
    public class BaseBrandCommand 
    {
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
    }
}
