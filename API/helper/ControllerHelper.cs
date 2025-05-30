using Application.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.helper
{
    public class ControllerHelper
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly JsonResponse _encrypt;

        public ControllerHelper(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _encrypt = new JsonResponse();
        }

        public async Task<IActionResult> HandleRequest<T>(Func<Task<T>> action) where T : ResponseBase
        {
            try
            {
                var response = await action();
                return CreateResponse(response);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new ResponseErrorMessages(400, false, e.Message));
            }
        }

        public IActionResult CreateResponse<T>(T response) where T : ResponseBase
        {
            if (_hostEnvironment.IsDevelopment() || true)
            {
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }
            return new ObjectResult(_encrypt.EncryptJson(response, "Vm14U1ExWXhVblJXYkZwT1ZsWmFWVlpyVmtaUFVUMDk=")) { StatusCode = response.StatusCode };
        }
    }
}
