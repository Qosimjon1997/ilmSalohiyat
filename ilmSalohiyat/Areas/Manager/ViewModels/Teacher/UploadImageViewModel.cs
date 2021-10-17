using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ilmSalohiyat.Areas.Manager.ViewModels.Teacher
{
    public class UploadImageViewModel
    {
        [Required]
        [Display(Name = "Image")]
        public IFormFile TeacherPicture { get; set; }
    }
}
