using Core.IUtils;
using System.Reflection.Metadata;
using static Infrastructure.Enums.Enums;

namespace API.Files
{
    public class LocalFormFileProxy : IFileProxy
    {
        const string allowedExt = ".png.jpg.jpeg";
        const int MaxFileSize = 5242880;
        const string _baseURL = "https://localhost:44358/Images";

        private IFormFile _file;
        private string _ext;
        private string basePath = "./Images";

        public LocalFormFileProxy(IFormFile file)
        {
            _file = file;
            _ext = Path.GetExtension(_file.FileName);
            Validate();
        }

        private void Validate()
        {
            string ext = Path.GetExtension(_file.FileName);
            long fileSize = _file.Length;

            if (fileSize > MaxFileSize || !allowedExt.Contains(ext))
                throw new Exception("Invalid File");
        }

        public async Task<string> SaveFile(string fileName, Folder folder = Folder.Default)
        {
            string folderName = folder.ToString();
            //AppContext.BaseDirectory
            //Directory.CreateDirectory(folderName);

            var filePath = Path.Combine($"{basePath}/", fileName + _ext);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await _file.CopyToAsync(fileStream);

            return $"{_baseURL}/{fileName}{_ext}";
        }
    }
}
