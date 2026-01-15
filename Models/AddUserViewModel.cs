using System.ComponentModel.DataAnnotations;

namespace BasicCrudProject.Models
{
    public class AddUserViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public bool? Verified { get; set; }
    }
}
