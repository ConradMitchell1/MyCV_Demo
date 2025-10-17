namespace MyCV_Demo.Models
{
    public class GalleryInputModel
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public IFormFile File { get; set; } = null!;
    }
}
