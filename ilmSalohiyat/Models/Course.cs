using System.ComponentModel.DataAnnotations;

namespace ilmSalohiyat.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public decimal Price { get; set; }



    }
}
