using System.ComponentModel.DataAnnotations;

namespace ilmSalohiyat.Areas.Manager.ViewModels.Teacher
{
    public class TeacherViewModel : EditImageViewModel
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
