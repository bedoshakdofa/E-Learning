using AutoMapper;
using E_Learning.Data.Model;
using E_Learning.DTOs;
using E_Learning.Interfaces;
using Microsoft.AspNetCore.JsonPatch;

namespace E_Learning.Data
{
    public class LectureRepository : ILectureRepository
    {
        private readonly DbContextApp _context;
        private readonly IFileServices _fileServices;
        private readonly IWebHostEnvironment _env;
        public LectureRepository(DbContextApp context,IFileServices fileServices,IWebHostEnvironment env) 
        {
            _context = context;
            _fileServices = fileServices;
            _env = env;
        }
        public async Task CreateLectureAsync(LectureDTO lectureDTO)
        {
            var fileName=await _fileServices.AddLectureFile(lectureDTO.Lec_PDF);

            if (fileName == null) throw new ArgumentNullException("file can't uploaded");

            var lecture = new Lecture
            {
                Lec_source = fileName,
                Lec_Name=lectureDTO.Lec_Name,
                Course_ID=lectureDTO.Course_ID,
            };
            await _context.AddAsync(lecture);
        }

        public void DeleteLectureAsync(Lecture lecture)
        {
            var wwwrootPath = _env.WebRootPath;

            var fullpath = Path.Combine(wwwrootPath, "Uploads", Path.GetFileName(lecture.Lec_source));

            var file = new FileInfo(fullpath);
            file.Delete();

            _context.lectures.Remove(lecture);
        }

        public async Task<Lecture> GetLectureByIdAsync(int id)
        {
            return await _context.lectures.FindAsync(id);
        }

        public async Task<bool> SaveAllAsync()
        {
            if (await _context.SaveChangesAsync() > 0) return true;

            return false;
        }
    }
}
