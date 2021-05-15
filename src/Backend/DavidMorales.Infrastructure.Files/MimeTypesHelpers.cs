using DavidMorales.Domain.Exceptions;

using System.Collections.Generic;
using System.IO;

namespace DavidMorales.Infrastructure.Files
{
    public static class MimeTypesHelpers
    {
        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        public static string GetExtension(string filename)
        {
            if (filename.Contains("."))
                throw new AppException("El archivo no tiene una extensión válida");

            var arrayExtensions = filename.Split('.');
            return arrayExtensions[arrayExtensions.Length - 1];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
