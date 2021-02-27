using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        public static string Add(IFormFile file)
        {
            var path = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            var newPath = CreateUniquePath(file);
            File.Move(path, newPath);

            return newPath;
        }

        public static void Delete(string path)
        {
            File.Delete(path);
        }

        public static string Update(string path, IFormFile file)
        {
            var newPath = CreateUniquePath(file);

            if (path.Length > 0)
            {
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            File.Delete(path);

            return newPath;
        }

        public static string CreateUniquePath(IFormFile file)
        {
            FileInfo ff = new FileInfo(file.FileName);

            string fileExtension = ff.Extension;

            string folderPath = Environment.CurrentDirectory + @"\Images";
            string guid = Guid.NewGuid().ToString("D");
            string date = DateTime.Now.ToString("yyyy-mm-dd_hh-mm");

            var newPath = Guid.NewGuid().ToString() + "_" + date + fileExtension;

            return $@"{folderPath}\{newPath}";
        }
    }
}
