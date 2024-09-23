using Club.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = Club.Service.Services.IServices.IAuthorizationService;

namespace Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService authorizationService;

        // Constructor injection for IAuthorizationService
        public AuthorizationController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin(TokenRequest request)
        {
            // Ensure request is not null
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid request.");
            }
            //return Ok(authorizationService.GetToken(request.Email, request.Password));
            var result = await authorizationService.GetToken(request.Email, request.Password);

            if (result is IActionResult actionResult)
            {
                return actionResult; // Return the result from GetTokenSystem.NullReferenceException: 'Object 
            }

            return StatusCode(500, "Internal server error.");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(TokenRequest request)
        {
            // Ensure request is not null
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid request.");
            }
            var result = await authorizationService.CheckAccount(request.Email, request.Password);

            if (result is IActionResult actionResult)
            {
                return actionResult; // Return the result from GetTokenSystem.NullReferenceException: 'Object 
            }

            return StatusCode(500, "Internal server error.");
        }

    }
}
