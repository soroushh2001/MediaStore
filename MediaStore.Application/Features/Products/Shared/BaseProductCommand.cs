namespace MediaStore.Application.Features.Products.Shared
{
    public class BaseProductCommand
    {
        public string Title { get; set; } = null!;

        public int Price { get; set; }

        public string? Description { get; set; }
        public int BrandId { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
