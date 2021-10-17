using System;
using System.ComponentModel.DataAnnotations;

namespace ilmSalohiyat.Areas.Manager.ViewModels.Course
{
    public class CourseViewModel : EditImageViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
