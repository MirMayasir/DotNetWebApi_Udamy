using UdamyCourse.Data;
using UdamyCourse.Model.Domain;

namespace UdamyCourse.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DataBaseContext dataBaseContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, DataBaseContext dataBaseContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dataBaseContext = dataBaseContext;
        }

        

        public async Task<Image> Upload(Image image)
        {
            
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtention}");
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath  = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtention}";

            image.FilePath = urlFilePath;

            //Save the image to the database
            await dataBaseContext.Images.AddAsync(image);
            var result = await dataBaseContext.SaveChangesAsync();

            return image;

        }
    }
}
