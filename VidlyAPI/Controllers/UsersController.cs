using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VidlyAPI.Models;
using VidlyAPI.Repository.IRepository;

namespace VidlyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("Authanication")]
        [ProducesResponseType(200, Type = typeof(Authanication))]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [AllowAnonymous]
        public IActionResult Authanication([FromBody] Authanication user)
        {
            var UserObj = _userRepository.Authanication(user.UserName, user.Password, user.Email);
            if (UserObj == null)
                return BadRequest(new { message = "UserName or Passward Incorrect" });
            return Ok(UserObj);
        }
        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [AllowAnonymous]
        public IActionResult Register([FromBody] Authanication user)
        {
            var IsUnique = _userRepository.IsUnique(user.UserName);
            if (IsUnique)
            {
                return BadRequest(new { message = "User Is Already Excist" });
            }
            var NewUser = _userRepository.Register(user.UserName, user.Password, user.Email);
            if (NewUser == null)
                return BadRequest(new { Message = "Something Went wrong with registration" });

            return Ok();
        }
    }
}
