using AutoMapper;
using E_Learning.Data.Model;
using E_Learning.DTOs;

namespace E_Learning.Helpers
{
    public class AutoMapperProfilies:Profile
    {
        public AutoMapperProfilies()
        {
            CreateMap<CourseDTO,Course>();
            CreateMap<Course, CourseDTO>();
            CreateMap<Course,CourseResponse>();
            CreateMap<Course, GetAllCourses>();
            CreateMap<DepartmentDTO, Department>();
            CreateMap<Department, DepartmentDTO>();
            CreateMap<Department, DepartmentResponse>();
            CreateMap<LectureDTO, Lecture>();
            CreateMap<Lecture, LectureResponse>();
            CreateMap<Enrollment, EnrollmentResponse>();
        }
    }
}
