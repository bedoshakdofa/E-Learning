
using E_Learning.DTOs;
using E_Learning.Interfaces;
using System.IO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    //[Authorize(Roles = "Instructor")]
    public class LectureController:ControllerBase
    {
        private readonly ILectureRepository _Lecture_repository;
        public LectureController(ILectureRepository lectureRepository)
        {
            _Lecture_repository = lectureRepository;

        }

        [HttpPost]
        public async Task<ActionResult>AddLecture(LectureDTO lectureDTO)
        {
            await _Lecture_repository.CreateLectureAsync(lectureDTO);

            if (await _Lecture_repository.SaveAllAsync()) return Ok("Lecture add sucessfully");

            return BadRequest("can't create a Lecture");

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult>DeleteLecture(int id)
        {
            var lecture = await _Lecture_repository.GetLectureByIdAsync(id);

            _Lecture_repository.DeleteLectureAsync(lecture);

            if (await _Lecture_repository.SaveAllAsync()) return Ok("Lecture deleted successfully");

            return BadRequest("can't delete lecture");
        }
    }
}
