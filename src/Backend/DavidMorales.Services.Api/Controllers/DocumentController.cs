using AutoMapper;

using DavidMorales.Domain.Authorization;
using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Enums;
using DavidMorales.Domain.Exceptions;
using DavidMorales.Domain.Interfaces.Services;
using DavidMorales.Domain.Security.Authentication;
using DavidMorales.Services.Api.Helpers;
using DavidMorales.Services.Api.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace DavidMorales.Services.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly IFileStoreService _fileStoreService;
        private readonly IMapper _mapper;
        private readonly AppIdentity _appIdentity;

        public DocumentController(
            IDocumentService documentService,
            IFileStoreService fileStoreService,
            AppIdentity appIdentity,
            IMapper mapper)
        {
            _documentService = documentService;
            _fileStoreService = fileStoreService;
            _mapper = mapper;
            _appIdentity = appIdentity;
        }

        [HttpPost("file")]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(209715200)]
        [Authorize(Policy = AppPermissions.Document.Add)]
        public async Task<IActionResult> InsertFile(FileViewModel fileViewModel)
        {
            var fileName = await _fileStoreService.AddAsync(fileViewModel.File.FileName, fileViewModel.File.OpenReadStream());

            var document = _mapper.Map<Document>(fileViewModel);
            var documentType = DocumentEnum.FindBy(document.Type);
            document.Type = documentType.Id;

            if(documentType.Id == DocumentEnum.EXTERNAL.Id)
            {
                document.ExternalFile = new ExternalFile
                {
                    Path = fileName
                };
            }

            if (documentType.Id == DocumentEnum.INTERNAL.Id)
            {
                document.InternalFile = new InternalFile
                {
                    Path = fileName
                };
            }

            await _documentService.CreateAsync(document);

            return ResponseHelper.Ok(document);
        }

        [HttpGet]
        [Authorize(Policy = AppPermissions.Document.Query)]
        public async Task<IActionResult> Get()
        {
            var documents = await _documentService.GetAsync();
            return ResponseHelper.Ok(documents);
        }

        [HttpGet("user/myself")]
        [Authorize(Policy = AppPermissions.Document.Query)]
        public async Task<IActionResult> GetMyDocuments()
        {
            var documents = await _documentService.GetByAddresseeAsync(_appIdentity.UserId);
            return ResponseHelper.Ok(documents);
        }

        [HttpPost("file/{fileName}/pdf")]
        [Authorize(Policy = AppPermissions.Document.Query)]
        public async Task<IActionResult> GetPdfFile(string fileName)
        {
            var path = await _fileStoreService.GetFullPath(fileName);
            return File(new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read), "application/pdf");
        }

    }
}
