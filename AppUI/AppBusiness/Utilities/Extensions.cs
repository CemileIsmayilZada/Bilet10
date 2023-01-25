using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Utilities
{
    public static class Extensions
    {
        public static bool CheckFileSize(this IFormFile file,int kByte)
        {
            return file.Length / 1024 < kByte;
        }
        public static bool CheckFileFormat(this IFormFile file, string format)
        {
            return file.ContentType.Contains(format);
        }
        public static string CopyToFile(this IFormFile file, params string[] folders)
        {
           string filePath=Guid.NewGuid()+file.FileName;
            string resultPath=string.Empty;
            foreach (var item in folders)
            {
                resultPath = Path.Combine(resultPath, item);
            }
            resultPath = Path.Combine(resultPath, filePath);
            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return filePath;
        }
    }
}
