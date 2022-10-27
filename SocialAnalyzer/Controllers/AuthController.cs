using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialAnalyzer.Models;
using SocialAnalyzer.Services.Interfaces;
using System.Threading.Tasks;

namespace SocialAnalyzer.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
     //   private readonly IClaimsHelper _claimsHelper;

        public AuthController(IAuthService authService) //, IClaimsHelper claimsHelper
        {
            _authService = authService;
           // _claimsHelper = claimsHelper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCredentialsModel credentials)
        {

            var loginResponse = await _authService.LoginAsync(credentials.UserName, credentials.Password);


            return Ok(loginResponse);
        }

        //[HttpGet("refresh")]
        //[Authorize]
        //public async Task<IActionResult> RefreshTokenAsync()
        //{
        //    var usuarioId = _claimsHelper.Id;

        //    var token = await _authService.RefreshTokenAsync(usuarioId);

        //    if (string.IsNullOrWhiteSpace(token))
        //        return NotFound();

        //    return Ok(token);
        //}




    
}
}
