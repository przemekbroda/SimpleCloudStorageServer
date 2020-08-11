using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCloudStorageServer.Security;

namespace SimpleCloudStorageServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = ApiKeyAuthOptions.DefaultScheme)]
    public class StorageController : ControllerBase
    {

        [AllowAnonymous]
        [HttpGet("{appid}/{fileName}")]
        public async Task<IActionResult> Getfile(string appid, string filename)
        {
            return Ok("ok");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            return Ok("ok");
        }

        [HttpDelete("{fileName}")]
        public async Task<IActionResult> RemoveFile(string fileName)
        {
            return Ok("ok");
        }

    }
}
