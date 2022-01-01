using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SampleAPI.Services
{
    public static class FileService
    {
        public static string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string format = "yyyy_MM_dd";
                DateTime now = DateTime.Now;
                string time = now.ToString(format);
                string fileName = time + "_"
                    + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images", fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return fileName;
            }
            else return null;
        }
    }
}
