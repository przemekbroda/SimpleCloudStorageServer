using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SimpleCloudStorageServer.Dto;
using SimpleCloudStorageServer.Security;
using SimpleCloudStorageServer.Service;

namespace SimpleCloudStorageServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = ApiKeyAuthOptions.DefaultScheme, Roles = "user")]
    public class StorageController : ControllerBase
    {

        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService) 
        {
            _storageService = storageService;
        }

        [AllowAnonymous]
        [HttpGet("{appid}/{fileName}")]
        public async Task<IActionResult> GetFile(string appid, string filename)
        {
            var file = await _storageService.GetFile(appid, filename);

            new FileExtensionContentTypeProvider().TryGetContentType(file.Item2, out var contentType);

            return File(file.Item1, contentType);
        }

        [HttpPost]
        [RequestSizeLimit(15000000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 15000000)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(await _storageService.UploadFile(file, userId));
        }

        [HttpPut("{fileName}")]
        public async Task<IActionResult> UpdateFile(string fileName, FileForUpdateDto updateDto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(await _storageService.UpdateFile(userId, fileName, updateDto));
        }

        [HttpDelete("{fileName}")]
        public async Task<IActionResult> RemoveFile(string fileName)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(await _storageService.RemoveFile(userId, fileName));
        }

    }
}
