using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_Learning.Data.Model;
using E_Learning.DTOs;
using E_Learning.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace E_Learning.Data
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly DbContextApp _context;
        private readonly IMapper _mapper;
        public EnrollmentRepository(DbContextApp context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateEnrollment(int courseId, string userId)
        {
            var enroll = new Enrollment
            {
                Course_ID = courseId,
                User_ID = userId,
                EnrollDate = DateTime.UtcNow,
            };

            await _context.Enrollments.AddAsync(enroll);
        }

        public void DeleteOneEnrollment(Enrollment enroll)
        {
            _context.Enrollments.Remove(enroll);
        }

        public async Task<IEnumerable<EnrollmentResponse>> GetAll(string Ssn)
        {
            var enrolls = await _context.Enrollments.Where(u => u.User_ID == Ssn).ProjectTo<EnrollmentResponse>(_mapper.ConfigurationProvider)
              .ToListAsync();
            return enrolls;
        }

        public async Task<bool> SaveAllAsync()
        {

            if (await _context.SaveChangesAsync()>0) return true;

            return false;
        }

        public async Task<Enrollment> GetOneEnrollment(int courseId, string Ssn)
        {
            var enroll = _context.Enrollments.AsQueryable();

            enroll = enroll.Where(u => u.User_ID ==Ssn);
            enroll = enroll.Where(c => c.Course_ID == courseId);

            return await enroll.FirstOrDefaultAsync();
        }
    }
} 
