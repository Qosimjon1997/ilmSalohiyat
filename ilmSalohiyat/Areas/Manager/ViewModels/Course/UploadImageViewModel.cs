using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ilmSalohiyat.Areas.Manager.ViewModels.Course
{
    public class UploadImageViewModel
    {
        [Required]
        [Display(Name = "Image")]
        public IFormFile CoursePicture { get; set; }
    }
}
