using Microsoft.AspNetCore.Mvc;
using Reborn.Services;

namespace Reborn.Cookies
{
    [Route("api/files")]
    [ApiController]
    public class StaticController : ControllerBase
    {
        IFileService fileService;

        public StaticController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet("{filepath}")]
        public IActionResult Get(string filepath)
        {
            try
            {
                var image = System.IO.File.OpenRead(Directory.GetCurrentDirectory() + "/Uploads/" + filepath);
                return File(image, "image/jpeg");
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(new { message = "Image not found" });
            }
        }
    }
}
