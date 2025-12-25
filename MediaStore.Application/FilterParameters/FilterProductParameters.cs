namespace MediaStore.Application.FilterParameters
{
    public class FilterProductParameters 
    {
        public string? CategorySlug { get; set; }
        public List<string>? BrandSlugs { get; set; }
        public string? Search { get; set; }
    }
}
