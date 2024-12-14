using System.ComponentModel.DataAnnotations;

namespace E_Learning.Data.Model
{
    public class Department
    {
        [Key]
        public int Dept_Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Course> courses { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
