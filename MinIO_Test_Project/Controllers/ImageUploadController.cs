using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;

[Route("api/[controller]")]
[ApiController]
public  class ImageUploadController : ControllerBase
{
    private readonly MinioClient _minioClient;

    public ImageUploadController(MinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    [HttpPost("upload")]

    public IActionResult UploadImage(IFormFile file)
    {
        try
        {
            var bucketName = "testbucket";
            var objectName = file.FileName;
            var contentType = file.ContentType;

            using var stream = file.OpenReadStream();
            var fileSize = stream.Length;

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(fileSize)
                .WithContentType(contentType);

            _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);

            return Ok("Image uploaded successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public IActionResult GetList()
    {

        return Ok();
    }




}

