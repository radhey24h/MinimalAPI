using System.ComponentModel.DataAnnotations;

namespace Employee.Models.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        [MaxLength(30)]
        public string RoleName { get; set; } = string.Empty;
        [MaxLength(300)]
        public string RoleDescription { get; set; } = string.Empty;
        public ICollection<User>? Users { get; set; }
    }
}
