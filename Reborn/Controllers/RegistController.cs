using Microsoft.AspNetCore.Mvc;
using Reborn.Models;
namespace Reborn.Controllers
{
    public class RegistController : ControllerBase
    {
        [HttpPost("api/Regist")]
        public IActionResult Regist([FromBody] LogReg _model)
        {
            
            return Ok();
        }
    }
}
