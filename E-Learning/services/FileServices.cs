using E_Learning.Interfaces;

namespace E_Learning.services
{
    public class FileServices : IFileServices
    {
        private readonly IWebHostEnvironment _env;
        public FileServices(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> AddLectureFile(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            var contentPath=_env.WebRootPath;

            var path = Path.Combine(contentPath, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ext=Path.GetExtension(file.FileName);

            if (ext != ".pdf")  throw new ArgumentException($"only PDFs is allowed");

            var FullNameWithPath = Path.Combine(path, file.FileName);
            using var stream=new FileStream(FullNameWithPath,FileMode.Create);

            await file.CopyToAsync(stream);

            return $"/Uploads/{file.FileName}";
        }
    }
}
