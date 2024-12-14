using E_Learning.Data.Model;

namespace E_Learning.DTOs
{
    public class CourseResponse
    {
        public int Id {  get; set; }
        public string Course_Name { get; set; }

        public string Course_Description { get; set; }
        
        public DepartmentDTO Department { get; set; }

        public ICollection<LectureResponse> Lecture { get; set; }
    }
}
