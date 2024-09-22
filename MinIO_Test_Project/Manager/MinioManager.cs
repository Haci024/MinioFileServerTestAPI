using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using MinIO_Test_Project.Services;

namespace MinIO_Test_Project.Manager
{
    public class MinioManager : IMinioService
    {
        private readonly MinioClient _minioClient;
        private readonly string _bucketName;

        public MinioManager(MinioClient minioClient)
        {
            _minioClient = minioClient;
            _bucketName = "goldlelectronicsbucket";
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Dosya bulunamadı.");
            }

            try
            {
                var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
                if (!bucketExists)
                {
                    await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Bucket kontrolü sırasında hata oluştu: {ex.Message}");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

                await _minioClient.PutObjectAsync(
                    new PutObjectArgs()
                        .WithBucket(_bucketName)
                        .WithObject(uniqueFileName)
                        .WithStreamData(stream)
                        .WithObjectSize(stream.Length)
                        .WithContentType(file.ContentType)
                );

                return uniqueFileName; // Yüklenen dosyanın adını döndürüyoruz
            }
        }
    }
}
