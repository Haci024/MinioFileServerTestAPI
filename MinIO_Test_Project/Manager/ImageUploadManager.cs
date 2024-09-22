using CommunityToolkit.HighPerformance;
using Microsoft.EntityFrameworkCore;
using MinIO_Test_Project.Connection;
using MinIO_Test_Project.Model;
using MinIO_Test_Project.Services;

namespace MinIO_Test_Project.Manager
{
    public class ImageUploadManager : IImageUploadService
    {
        private readonly AppDbContext _db;
        private readonly IMinioService _minioService;

        public ImageUploadManager(AppDbContext db, IMinioService minioService)
        {
            _db = db;
            _minioService = minioService;
        }

        public async Task<ImageUpload> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Dosya bulunamadı.");
            var uniqueFileName = await _minioService.UploadImage(file);
            var imageUpload = new ImageUpload
            {
                FileName = uniqueFileName, 
                FilePath = $"goldlelectronicsbucket/{uniqueFileName}"
            };
            _db.ImageUploads.Add(imageUpload);
            await _db.SaveChangesAsync();
            return imageUpload;
        }
    }

}

