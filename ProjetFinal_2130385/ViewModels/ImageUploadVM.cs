using ProjetFinal_2130385.Models;

namespace ProjetFinal_2130385.ViewModels
{
    public class ImageUploadVM
    {
        public IFormFile? FormFile { get; set; }
        public Image Image { get; set; } = null!;
        public string NomModele { get; set; } = null!;
    }
}
