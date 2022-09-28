using System.ComponentModel.DataAnnotations;

namespace Employee.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Email Id is a required field!")]
        public string Email { get; set; } = string.Empty;
        [MaxLength(200)]
        [Required(ErrorMessage = "Password is a required field!")]
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }//1-Employee or 2-Hr
        public Role? Role { get; set; }
    }
}
