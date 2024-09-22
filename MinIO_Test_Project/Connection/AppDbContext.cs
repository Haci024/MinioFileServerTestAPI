using Microsoft.EntityFrameworkCore;
using MinIO_Test_Project.Model;

namespace MinIO_Test_Project.Connection
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=MinioTestDatabase;integrated security=true;TrustServerCertificate=True;");//Database ilə əlaqə

        }
        public DbSet<ImageUpload> ImageUploads { get; set; }


       
    }
}
