using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.helper
{
    public class UploadFile
    {
        private readonly PathFile _pathFile;

        public UploadFile(PathFile pathFile)
        {
            _pathFile = pathFile;
        }

        public class IGetPathFile
        {
            public string DirectoryFile { get; set; } = string.Empty;
            public string FilePath { get; set; } = string.Empty;
            public string OriginalFileName { get; set; } = string.Empty;
            public string FileName { get; set; } = string.Empty;
            public string TypeFile { get; set; } = string.Empty;
            public long SizeFile { get; set; }

        }

        public async Task<IGetPathFile> GetPathFile(int keyMain, IFormFile document, string addPathName , string? removeImage = "")
        {
            string pathname = _pathFile.GetPathname();
            string baseDirectory = Directory.GetCurrentDirectory();
            string uploadsPath = Path.Combine(baseDirectory, pathname, addPathName);

            // Ensure the directory exists
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            // Generate a unique filename
            string fileExtension = Path.GetExtension(document.FileName);
            string newFileName = _pathFile.GenerateFilename(keyMain, uploadsPath, fileExtension);
            string filePath = Path.Combine(uploadsPath, newFileName);

            // Remove ld Image
            if (!string.IsNullOrEmpty(removeImage))
            {
                if (Directory.Exists(Path.Combine(baseDirectory, pathname, removeImage)))
                {
                    File.Delete(Path.Combine(baseDirectory, pathname, removeImage));
                }
            }

            // Save the file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await document.CopyToAsync(fileStream);
            }

            string relativePath = Path.GetRelativePath(Path.Combine(baseDirectory, pathname), filePath);

            // Return details
            return new IGetPathFile
            {
                DirectoryFile = filePath,
                OriginalFileName = document.FileName,
                FileName = newFileName,
                FilePath = relativePath,
                TypeFile = System.IO.Path.GetExtension(document.FileName),
                SizeFile = document.Length,
            };
        }

        public string CombineUrlPath(string url, string? pathImage)
        {
            if (string.IsNullOrWhiteSpace(pathImage))
            {
                return string.Empty;
            }

            return $"{url.TrimEnd('/')}/{pathImage.Replace("\\", "/").TrimStart('/')}";
        }
    }
}
