using CleanArchitecture.Domain.Entities_SubModel.User;
using System.Web.Http.ModelBinding;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginModel request)
        {
            if (request == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.Login(request, out string errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                return StatusCode(422, ModelState);
            }
            var token = _userRepository.CreateToken(request.UserName);
            return Ok (new { email = request.UserName, accessToken = token });
        }
    }
}
