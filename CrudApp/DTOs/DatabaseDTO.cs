using System.ComponentModel.DataAnnotations;

namespace CrudApp.DTOs
{
    public class DatabaseDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Database name should not be empty!")]
        public string Name { get; set; }
    }
}
