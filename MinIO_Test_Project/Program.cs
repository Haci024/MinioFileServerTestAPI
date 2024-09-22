using Minio;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var minioConfig = builder.Configuration.GetSection("Minio");
string endpoint = minioConfig["url"];
string accessKey = minioConfig["accessKey"];
string secretKey = minioConfig["secretKey"];


//var minioClient = new MinioClient()
//    .WithEndpoint("localhost", 9000) // 
//    .WithCredentials(accessKey,secretKey)
//    .Build();
builder.Services.AddSingleton<MinioClient>(sp =>
{
    var minioClient = new MinioClient()
        .WithEndpoint("localhost", 9000)
        .WithCredentials(accessKey, secretKey)
        .Build();
    return (MinioClient)minioClient;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
