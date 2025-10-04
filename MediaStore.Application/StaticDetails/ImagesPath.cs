namespace MediaStore.Application.StaticDetails
{
    public static class ImagesPath
    {
        public static string ProductImageOrgServerPath =
            Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/contents/product/org/");

        public static string ProductImageThumbServerPath =
           Path.Combine(Directory.GetCurrentDirectory(),
               "wwwroot/contents/product/thumb/");
    }
}
