using System.ComponentModel.DataAnnotations;

namespace ilmSalohiyat.Models
{
    public class Gallery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageURL { get; set; }

    }
}
