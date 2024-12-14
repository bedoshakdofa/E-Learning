using E_Learning.DTOs;
using E_Learning.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CourseController:ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository, IWebHostEnvironment env)
        {
            _courseRepository = courseRepository;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateCourse(CourseDTO courseDTO)
        {
            
            var course=await _courseRepository.GetCourseByName(courseDTO.Course_Name.ToLower());

            if (course != null) return BadRequest("this course is already found");

            await _courseRepository.AddCourse(courseDTO);
            
            if (await _courseRepository.SaveAllAsync()) return Ok("course add sucessfully");

            return BadRequest("can't create a course");
        }

        [Authorize]
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<GetAllCourses>>> GetAllCourses()
        {
            var courses = await _courseRepository.GetAllCourse();

            if (courses.Count() == 0) return NotFound("there is not content");

            return Ok(courses);
        }
        [Authorize]
        [HttpGet("getOne/{courseName}")]
        public async Task<ActionResult<CourseResponse>> GetCourseByName(string courseName)
        {
            var course = await _courseRepository.GetCourseByName(courseName);
            if (course == null) return NotFound("course is not found");

            foreach (var lecture in course.Lecture)
            {
                lecture.Lec_source = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Uploads/{Path.GetFileName(lecture.Lec_source)}";
            }
            return Ok(course);  
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("{courseId:int}")]

        public async Task<ActionResult> DeleteCourseByID(int courseId)
        {
           var coures=await _courseRepository.GetCourseById(courseId);

            if (coures == null) return NotFound("this course is not found");

            _courseRepository.DeleteCourse(coures);

            if (await _courseRepository.SaveAllAsync()) return Ok("course is deleted");

            return BadRequest("samething went wwrong");
        }
        [Authorize(Roles ="Admin")]
        [HttpPatch("{id:int}")]

        public async Task<ActionResult>UpdateCourse([FromRoute]int id,
            [FromBody] JsonPatchDocument<CourseDTO> courseDTO)
        {
            var course = await _courseRepository.GetCourseById(id);

            if (course == null) return NotFound("this course is not found");

            _courseRepository.PatchCourse(courseDTO, course);
            if (await _courseRepository.SaveAllAsync()) return Ok("course is updated");

            return BadRequest("some this went wrong");
        }
    }
}
