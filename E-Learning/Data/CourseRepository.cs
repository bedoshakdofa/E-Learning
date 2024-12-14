using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_Learning.Data.Model;
using E_Learning.DTOs;
using E_Learning.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DbContextApp _context;
        private readonly IMapper _mapper;

        public CourseRepository(DbContextApp context,IMapper Mapper)
        {
            _context = context;
            _mapper = Mapper;
        }
        public async Task AddCourse(CourseDTO CourseDto)
        {
            CourseDto.Course_Name = CourseDto.Course_Name.ToLower();
            var course = _mapper.Map<Course>(CourseDto);
            await _context.Courses.AddAsync(course);
        }


        public void DeleteCourse(Course course)
        {
             _context.Courses.Remove(course);
        }

        public Task<Course>GetCourseById(int id)
        {
            return _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<GetAllCourses>> GetAllCourse()
        {
            var course=await _context.Courses.ProjectTo<GetAllCourses>(_mapper.ConfigurationProvider).ToListAsync();

            return course;
            
        }

        public async Task<CourseResponse> GetCourseByName(string courseName)
        {

            var course = await _context.Courses
                .ProjectTo<CourseResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Course_Name.Contains(courseName));
            return course;
        }
        public async Task<bool> SaveAllAsync()
        {
            if (await _context.SaveChangesAsync()>0) return true; 

            else return false;
        }

        public void PatchCourse(JsonPatchDocument<CourseDTO> patchDoc, Course course)
        {
            var CourseDtoToPatch = _mapper.Map<CourseDTO>(course);

            patchDoc.ApplyTo(CourseDtoToPatch);

            _mapper.Map(CourseDtoToPatch, course);

        }

    
    }
}
