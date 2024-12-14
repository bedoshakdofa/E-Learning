namespace E_Learning.Interfaces
{
    public interface IFileServices
    {
        Task<string>AddLectureFile(IFormFile file);
    }
}
