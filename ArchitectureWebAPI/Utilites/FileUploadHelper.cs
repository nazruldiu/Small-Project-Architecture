namespace ArchitectureWebAPI.Utilites
{
    public static class FileUploadHelper
    {
        private static HttpContext _httpContext => new HttpContextAccessor().HttpContext; // first need to register HttpContextAccessor() in Program.cs file
        private static IWebHostEnvironment _webHostEnvironment => (IWebHostEnvironment)_httpContext.RequestServices.GetService(typeof(IWebHostEnvironment));
        
        public static string UploadedFile(IFormFile formFile)
        {
            string uniqueFileName = String.Empty;

            if (formFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                formFile.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        public static void DeleteFile(string? fileUrl)
        {
            if (fileUrl != null)
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileUrl);
                System.IO.File.Delete(filePath);
            }
        }

    }
}
