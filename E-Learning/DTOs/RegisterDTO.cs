using E_Learning.Data.Model;

namespace E_Learning.DTOs
{
    public class RegisterDTO
    {

        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string phone { get; set; }

        public string Email { get; set; }

        public string password { get; set; }

        public string Role { get; set; }

        public int Dept_id { get; set; }
    }


}

