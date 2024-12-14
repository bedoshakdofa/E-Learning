namespace E_Learning.DTOs
{
    public class GetAllCourses
    {
        public int Id { get; set; }
        public string Course_Name { get; set; }

        public string Course_Description { get; set; }

        public DepartmentDTO Department { get; set; }
    }
}
