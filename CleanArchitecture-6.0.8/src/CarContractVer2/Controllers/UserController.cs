
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Domain.Entities_SubModel.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Endpoints;
using CleanArchitecture.Application.Repository;
using System.Web.Http.ModelBinding;
using CleanArchitecture.Domain.Entities_SubModel.User.SubModel;
using CleanArchitecture.Application.Constant;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;

namespace CarContractVer2.Controllers
{
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private static readonly IList<User> _users = new List<User>();


        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet/*, Authorize(Roles = UserRoleConstant.Admin + "," + UserRoleConstant.Expertise)*/]
        [Route(UserEndpoints.GetAll)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers([FromQuery] UserFilter filter, int page = 1, int pageSize = 10)
        {
            var listUser = _userRepository.GetUsers(page, pageSize, filter);
            int toalCount = _userRepository.GetNumberOfUsers(filter);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(new { users = listUser, total = toalCount });
        }

        [HttpGet, Authorize(Roles = UserRoleConstant.Admin)]
        [Route(UserEndpoints.GetSingle)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUserById(int id)
        {
            if (!_userRepository.UserExit(id))
                return NotFound();
            var user = _userRepository.GetUserById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }

        [HttpGet]
        [Route(UserEndpoints.GetUserIdByEmail)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUserIdByEmail(string email)
        {
            if (!_userRepository.EmailExit(email))
                return NotFound();
            var userId = _userRepository.GetUserIdByEmail(email);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(userId);
        }

        [HttpPost]
        [Route(UserEndpoints.Create)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] CreateUserModel userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.CreateUser(userCreate, out string errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                return StatusCode(422, ModelState);
            }

            return Ok("Successfully Added");
        }
        [HttpPut]
        [Route(UserEndpoints.UpdateInfo)]
        public IActionResult Update(int userId, [FromBody] UserUpdateModel request)
        {
            if (request == null || userId != request.Id)
                return BadRequest();

            // Check if the car with the specified id exists
            if (!_userRepository.UserExit(userId))
                return NotFound();

            // Update the car and its related data
            _userRepository.UpdateUser(userId, request);

            return Ok();
        }

        [HttpPut]
        [Route(UserEndpoints.UpdateRole)]
        public async Task<IActionResult> UpdateUserRole([FromRoute] int id, [FromBody] UpdateUserRoleModel model)
        {
            if (model == null)
                return BadRequest(ModelState);

            if (!_userRepository.UserExit(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_userRepository.UpdateUserRoleAsync(id, model))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPut("password-change")]
        public async Task<ActionResult<string>> ChangePassword(UpdateUserPasswordModel request)
        {
            if (request == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.ChangePassword(request, out string errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                return StatusCode(422, ModelState);
            }
            return Ok();
        }

        [HttpPut]
        [Route(UserEndpoints.Delete)]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            if (!_userRepository.UserExit(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_userRepository.DeleteUser(id))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}



