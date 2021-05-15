using Microsoft.AspNetCore.Http;

namespace DavidMorales.Services.Api.ViewModels
{
    public class FileViewModel
    {
        public string Type { get; set; }
        public int SenderId { get; set; }
        public int AddresseeId { get; set; }

        public IFormFile File { get; set; }
    }
}
