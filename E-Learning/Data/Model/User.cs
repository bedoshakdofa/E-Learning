using System.ComponentModel.DataAnnotations;

namespace E_Learning.Data.Model
{
    public class User
    {
        [Key]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "The SSN must be exactly 14 characters.")]
        [Required]
        public string SSN { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public byte[] password { get; set; }

        public byte[] passwordSalt { get; set; }

        [RegularExpression("User|Instructor|Admin", ErrorMessage = "Invalid status value.")]
        public string Role { get; set; } = "User";
        public int? Dept_id { get; set; }
        public Department Department { get; set; }

        public ICollection<Enrollment> Enrollment { get; set; }
    }
}
