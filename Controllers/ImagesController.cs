using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdamyCourse.Model.Domain;
using UdamyCourse.Model.DTOs;
using UdamyCourse.Repositories;

namespace UdamyCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidationFileUpload(imageUploadRequestDto);
            if (ModelState.IsValid)
            {
                //Use your service to save the image

                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileName = imageUploadRequestDto.FileName,
                    FileDescription = imageUploadRequestDto.FileDescription,
                    FileExtention = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileSizeInBytes = imageUploadRequestDto.File.Length

                };

                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidationFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtention = new List<string> { ".jpg", ".jpeg", ".png" };    
            if(!allowedExtention.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("File", "Invalid file type. Only .jpg, .jpeg, and .png are allowed.");
            }

            if(imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size exceeds the limit of 10MB, please upload a smaller size file");
            }
        }

    }
}
