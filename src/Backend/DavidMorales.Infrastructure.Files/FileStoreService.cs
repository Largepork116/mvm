using DavidMorales.Domain.Exceptions;
using DavidMorales.Domain.Interfaces.Services;

using System;
using System.IO;
using System.Threading.Tasks;

namespace DavidMorales.Infrastructure.Files
{
    public class FileStoreService : IFileStoreService
    {
        private readonly string _path;

        public FileStoreService()
        {
            _path = Environment.CurrentDirectory;
            _path = Path.Combine(_path, "wwwroot", "files");

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        public async Task<string> AddAsync(string uplodedFileName, Stream file)
        {
            var fileName = $"{Guid.NewGuid()}{GetExtension(uplodedFileName)}";
            var filePath = Path.Combine(_path, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task DeleteAsync(string fileName)
        {
            var filePath = Path.Combine(_path, fileName);


            if (!File.Exists(filePath))
            {
                throw new AppNotFoundException("Archivo no encontrado");
            }

            await Task.Delay(0);
            File.Delete(filePath);
        }

        public async Task<string> GetFullPath(string fileName)
        {
            var fullPath = await Task.FromResult(Path.Combine(_path, fileName));

            if (!File.Exists(fullPath))
            {
                throw new AppNotFoundException("No se encontró el archivo");
            }

            return fullPath;
        }

        public async Task<string> UpdateAsync(string uplodedFileName, string existingFileName, Stream file)
        {
            // Add the new file
            var name = await AddAsync(uplodedFileName, file);

            await DeleteAsync(existingFileName);

            return name;
        }

        private string GetExtension(string file)
        {
            if (!file.Contains("."))
                throw new AppException("Archivo no válido");

            var array = file.Split(".");
            return $".{array[array.Length - 1].Trim()}";
        }
    }
}
