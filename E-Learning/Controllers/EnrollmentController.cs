using E_Learning.Data.Model;
using E_Learning.DTOs;
using E_Learning.Extenstions;
using E_Learning.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace E_Learning.Controllers
{
    [ApiController]
    [Route("/api/enroll")]
    [Authorize(Roles ="User")]
    public class EnrollmentController:ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        public EnrollmentController(IEnrollmentRepository repository)
        {
            _enrollmentRepository = repository;
        }

        [HttpPost("{id:int}")]
        public async Task<ActionResult>EnrollToCourse(int id)
        {
            var userId=User.GetUserId();

            var EnrollIsExist= await _enrollmentRepository.GetOneEnrollment(id,userId);

            if (EnrollIsExist != null) return BadRequest("you have enroll to this course before");

            await _enrollmentRepository.CreateEnrollment(id, userId);

            if (await _enrollmentRepository.SaveAllAsync()) return Ok("you have enroll to this course");

            return BadRequest("can't enroll to this course");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentResponse>>>GetAllEnroll()
        {
            var userId=User.GetUserId();
            var enrolls=await _enrollmentRepository.GetAll(userId);

            if (enrolls.Count()==0)
                return NotFound("you don't have any enrollment");

            return Ok(enrolls);
        }

        [HttpDelete("{courseId}")]

        public async Task<ActionResult> DeleteEnrollment(int courseId)
        {
            var userId = User.GetUserId();

            var enroll = await _enrollmentRepository.GetOneEnrollment(courseId, userId);

            if (enroll == null) return NotFound("you don't have any enrollment");

            _enrollmentRepository.DeleteOneEnrollment(enroll);

            if (await _enrollmentRepository.SaveAllAsync()) return Ok("deleted successfully");

            return BadRequest("can't delete");
        }
    }
}
