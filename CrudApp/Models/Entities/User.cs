using System.ComponentModel.DataAnnotations;

namespace CrudApp.Models.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public Guid Uid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
        public string Type { get; set; }
    }
}
