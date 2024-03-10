using Microsoft.AspNetCore.Mvc;
using Reborn.Models;
using Reborn.Services;

namespace Reborn.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService, IUsersService usersService)
        {
            _usersService = usersService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        async public Task<ActionResult<User>> Register([FromBody] User user) {
            var candidate = await _usersService.FindOneByEmail(user.email);
            if (candidate != null) return BadRequest();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.password);
            user.password = hashedPassword;

            User newUser = await _usersService.Create(user);
            return Ok(_tokenService.CreateToken(newUser));
        }

        [HttpPost("login")]
        async public Task<ActionResult<User>> Login([FromBody] User user) {
            var existingUser = await _usersService.FindOneByEmail(user.email);
            if (existingUser == null) return NotFound("User not found");
            if(!BCrypt.Net.BCrypt.Verify(user.password, existingUser.password))
                return BadRequest("Invalid Password");

            return Ok(_tokenService.CreateToken(existingUser));
        }
    }
}
