using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reborn.Models;
using Reborn.Services;

namespace Reborn.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly ITokenService tokenService;

        public AuthController(ITokenService tokenService, IUsersService usersService)
        {
            this.usersService = usersService;
            this.tokenService = tokenService;
        }

        [HttpPost("register")]
        async public Task<ActionResult<User>> Register([FromBody] User user) {
            var candidate = await usersService.FindOneByEmail(user.email);
            if (candidate != null) 
                return BadRequest(new { message = "User with current email already exists" });

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.password);
            user.password = hashedPassword;

            User newUser = await usersService.Create(user);
            return Ok(new { access_token = tokenService.CreateToken(newUser) });
        }

        [HttpPost("login")]
        async public Task<ActionResult<User>> Login([FromBody] User user) {
            var existingUser = await usersService.FindOneByEmail(user.email);
            if (existingUser == null) 
                return NotFound(new { message = "User not found" });
            if(!BCrypt.Net.BCrypt.Verify(user.password, existingUser.password))
                return BadRequest(new { message = "Invalid password" });

            return Ok(new {access_token = tokenService.CreateToken(existingUser) });
        }
    }
}
