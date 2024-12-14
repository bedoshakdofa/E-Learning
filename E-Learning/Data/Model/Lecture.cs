using System.ComponentModel.DataAnnotations;

namespace E_Learning.Data.Model
{
    public class Lecture
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Lec_Name { get; set; }
        [Required]
        public string Lec_source { get; set; }
        public int Course_ID { get; set; }
        public Course Course { get; set; }

    }
}
