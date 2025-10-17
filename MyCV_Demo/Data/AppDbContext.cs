using Microsoft.EntityFrameworkCore;
using MyCV_Demo.Models;

namespace MyCV_Demo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<GalleryResultModel> GalleryItems { get; set; }
        public DbSet<CVModel> CVs { get; set; }
    }
}
