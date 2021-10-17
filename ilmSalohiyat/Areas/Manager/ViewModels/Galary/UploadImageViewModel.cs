using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ilmSalohiyat.Areas.Manager.ViewModels.Galary
{
    public class UploadImageViewModel
    {
        [Required]
        [Display(Name = "Image")]
        public IFormFile GallaryPicture { get; set; }
    }
}
