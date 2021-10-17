using System.ComponentModel.DataAnnotations;

namespace ilmSalohiyat.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

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
