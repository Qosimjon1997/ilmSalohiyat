using System.ComponentModel.DataAnnotations;

namespace ilmSalohiyat.Areas.Manager.ViewModels.Galary
{
    public class GalaryViewModel : EditImageViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
