using E_Learning.Data.Model;

namespace E_Learning.DTOs
{
    public class DepartmentResponse
    {
        public int Dept_Id { get; set; }

        public string Name { get; set; }

        public ICollection<CourseDTO> courses { get; set; }
    }
}
