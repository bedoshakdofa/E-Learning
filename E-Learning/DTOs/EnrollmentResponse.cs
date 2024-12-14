using E_Learning.Data.Model;

namespace E_Learning.DTOs
{
    public class EnrollmentResponse
    {
        public DateTime EnrollDate { get; set; }

        public CourseDTO Course { get; set; }
    }
}
