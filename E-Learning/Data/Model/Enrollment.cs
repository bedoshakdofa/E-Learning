namespace E_Learning.Data.Model
{
    public class Enrollment
    {
        
        public int Course_ID { get; set; }

        public string User_ID { get; set; }

        public DateTime EnrollDate { get; set; }

        public User user { get; set; }

        public Course Course { get; set; }
    }
}
