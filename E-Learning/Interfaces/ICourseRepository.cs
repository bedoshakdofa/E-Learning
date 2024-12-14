using E_Learning.Data.Model;
using E_Learning.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace E_Learning.Interfaces
{
    public interface ICourseRepository
    {
        Task AddCourse(CourseDTO CourseDto);

        void DeleteCourse(Course course);

        Task<Course> GetCourseById(int courseId);

        Task<CourseResponse> GetCourseByName(string courseName);

        Task< IEnumerable<GetAllCourses>> GetAllCourse();

        Task<bool> SaveAllAsync();
        void PatchCourse(JsonPatchDocument<CourseDTO> patchDoc, Course course);
    }
}
