namespace MediaStore.Application.Specifications
{
    public class FilterProductSpecification : BaseQueryParameters
    {
        public string? CategorySlug { get; set; }
        public List<string>? BrandSlugs { get; set; }
        public string? Search { get; set; }
    }
}
