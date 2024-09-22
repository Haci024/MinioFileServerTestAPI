namespace MinIO_Test_Project.Services
{
    public interface IMinioService
    {
         Task<string> UploadImage(IFormFile file);
    }

}
