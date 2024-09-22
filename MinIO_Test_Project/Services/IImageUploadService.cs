using MinIO_Test_Project.Model;

namespace MinIO_Test_Project.Services
{
    public interface IImageUploadService
    {
        Task<ImageUpload> UploadImage(IFormFile file);
    }
}
