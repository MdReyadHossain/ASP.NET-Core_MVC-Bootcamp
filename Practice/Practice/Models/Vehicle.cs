using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
