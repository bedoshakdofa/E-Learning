namespace E_Learning.DTOs
{
    public class LectureDTO
    {
        public string Lec_Name { get; set; }

        public IFormFile Lec_PDF { get; set; }

        public int Course_ID { get; set; }
    }
}
