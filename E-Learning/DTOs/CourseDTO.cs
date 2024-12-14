namespace E_Learning.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Course_Name { get; set; }

        public string Course_Description { get; set; }
        public int Dept_Id_FK { get; set; }
    }
}
