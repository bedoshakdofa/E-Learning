using E_Learning.Data.Model;
using E_Learning.DTOs;

namespace E_Learning.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task CreateEnrollment(int courseId, string userId);

        Task<IEnumerable<EnrollmentResponse>> GetAll(string Ssn);

        void DeleteOneEnrollment(Enrollment enroll);

        Task<Enrollment> GetOneEnrollment(int courseId,string Ssn);

        Task<bool> SaveAllAsync();
    }
}
