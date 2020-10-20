using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCloudStorageServer.Helper.Exceptions;

namespace SimpleCloudStorageServer.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            int? code = null;
            string detail = null;

            if (exception is NotFoundException) { code = StatusCodes.Status404NotFound; detail = exception.Message; }
            if (exception is RegisterException) { code = StatusCodes.Status406NotAcceptable; detail = exception.Message; }
            if (exception is BusinessException) { code = StatusCodes.Status400BadRequest; detail = exception.Message; }

            return Problem(statusCode: code, detail: detail, title: null);
        }
    }
}
