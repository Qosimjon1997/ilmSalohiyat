using System;
using System.ComponentModel.DataAnnotations;

namespace ilmSalohiyat.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public DateTime PostTime { get; set; }

    }
}
