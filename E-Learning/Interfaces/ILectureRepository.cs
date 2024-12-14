using E_Learning.Data.Model;
using E_Learning.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace E_Learning.Interfaces
{
    public interface ILectureRepository
    {
        Task CreateLectureAsync(LectureDTO lectureDTO);

        Task<Lecture>GetLectureByIdAsync(int id);

        void DeleteLectureAsync(Lecture lecture);

        Task<bool> SaveAllAsync();


    }
}
