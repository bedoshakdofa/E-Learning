using System.ComponentModel.DataAnnotations;

namespace E_Learning.Data.Model
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Course_Name { get; set; }
        public string Course_Description { get; set; }

        [Required]
        public int Dept_Id_FK { get; set; }
        public Department department { get; set; }

        public ICollection<Enrollment> enrollment { get; set; }

        public ICollection<Lecture> Lecture { get; set; }

    }
}
