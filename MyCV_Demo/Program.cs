using Microsoft.EntityFrameworkCore;
using MyCV_Demo.Data;

namespace MyCV_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Resolve a writable home folder (works locally & on Azure App Service).
            var home = Environment.GetEnvironmentVariable("HOME")
                       ?? Directory.GetCurrentDirectory();

            var dataDir = Path.Combine(home, "data");
            Directory.CreateDirectory(dataDir);
            var dbPath = Path.Combine(dataDir, "mycv_demo.sqlite");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"
            ));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            // Ensure DB is created/migrated on boot (important on Azure)
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate(); // requires you to have at least one migration
            }

            app.Run();
        }
    }
}
