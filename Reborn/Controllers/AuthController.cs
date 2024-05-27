using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reborn.Models;
using Reborn.Services;
using System.Security.Cryptography;

namespace Reborn.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly ITokenService tokenService;
        private readonly IMailService mailService;

        public AuthController(ITokenService tokenService, IUsersService usersService, IMailService mailService)
        {
            this.usersService = usersService;
            this.tokenService = tokenService;
            this.mailService = mailService;
        }

        [HttpPost("register")]
        async public Task<ActionResult<User>> Register([FromBody] User user) {
            var candidate = await usersService.FindOneByEmail(user.email);
            if (candidate != null) 
                return BadRequest(new { message = "User with current email already exists" });

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.password);
            user.password = hashedPassword;

            user.activationLink = this.GenerateRandomString(10);

            User newUser = await usersService.Create(user);
            mailService.SendEmail(user.email, "Account activation", "Please activate your account by the following link: http://localhost:5116/api/auth/activate/"+user.activationLink);

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

        [HttpGet("activate/{code}")]
        async public Task<ActionResult<String>> Activate(string code)
        {
            var existingUser = await usersService.FindOneByActivationLink(code);
            if (existingUser == null)
                return NotFound(new { message = "Invalid URL" });
            existingUser.isActivated = true;
            await usersService.Update(existingUser.id, existingUser);

            return Ok(new { message = "Activated Successfully" });
        }

        private string GenerateRandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var stringChars = new char[length];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new String(stringChars);
        }

    }
}
