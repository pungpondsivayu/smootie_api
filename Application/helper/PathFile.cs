using Microsoft.Extensions.Configuration;

namespace Application.helper
{
    public class PathFile
    {
        private readonly IConfiguration _configuration;

        public PathFile(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetPathname()
        {
            string pathname = _configuration.GetSection("UploadFilePath")?.Value ?? "wwwroot";
            return pathname;
        }

        public string GenerateFilename(int personId, string directoryPath, string extension)
        {
            string dateTimeString = DateTime.Now.ToString("yy.MM.dd_HH.mm");
            string baseFilename = $"{personId}_{dateTimeString}";
            string filename = $"{baseFilename}(1){extension}";
            int counter = 1;

            while (File.Exists(Path.Combine(directoryPath, filename)))
            {
                counter++;
                filename = $"{baseFilename}({counter}){extension}";
            }

            return filename;
        }
    }
}
