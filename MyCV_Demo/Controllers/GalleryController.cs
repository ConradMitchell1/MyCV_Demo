using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCV_Demo.Data;
using MyCV_Demo.Models;
using System.Threading.Tasks;

namespace MyCV_Demo.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _db;
        public GalleryController(IWebHostEnvironment env, AppDbContext db)
        {
            _env = env;
            _db = db;
        }
        // GET: GalleryController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _db.GalleryItems.OrderByDescending(x => x.Id).ToListAsync();
            return View(items);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteImage(int id)
        {
            var item = await _db.GalleryItems.FindAsync(id);
            if (item == null) return NotFound();
 
            var physicalPath = Path.Combine(_env.WebRootPath, item.ImagePath.TrimStart('/'));
            if (System.IO.File.Exists(physicalPath))
            {
                System.IO.File.Delete(physicalPath);
            }
            _db.GalleryItems.Remove(item);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult UploadImage()
        {
            return View("Upload");
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(GalleryInputModel vm)
        {
            if(vm.File == null || vm.File.Length == 0)
            {
                ModelState.AddModelError("File", "Please select a valid image file.");
                return View(vm);
            }

            var allowedTypes = new[] {"image/jpeg", "image/png", "image/gif", "image/webp" };
            if (!allowedTypes.Contains(vm.File.ContentType))
            {
                ModelState.AddModelError("File", "Only JPEG, PNG, GIF, or WEBP images are allowed.");
                return View(vm);
            }
            var uploadsRoot = Path.Combine(_env.WebRootPath, "uploads");
            var ext = Path.GetExtension(vm.File.FileName);
            var baseName = Path.GetFileNameWithoutExtension(vm.Title);
            var safeName = $"{baseName}_{Guid.NewGuid():N}{ext}";
            var physicalPath = Path.Combine(uploadsRoot, safeName);

            using (var stream = System.IO.File.Create(physicalPath))
            {
                await vm.File.CopyToAsync(stream);
            }

            var newGalleryItem = new GalleryResultModel
            {
                Title = vm.Title,
                Description = vm.Description,
                ImagePath = $"/uploads/{safeName}"
            };
            _db.GalleryItems.Add(newGalleryItem);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
