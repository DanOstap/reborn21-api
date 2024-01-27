using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using Reborn.Models;
using System.Linq;

namespace Reborn.Cookies
{
    public class ProductCookiesController : ControllerBase
    {

        [HttpPost("/api/AddCookies")]
        public IActionResult PostCookies([FromBody] Cart Cart_Model)
        {
            Dictionary<string, string[]> Cookies_Dictionary = new Dictionary<string, string[]> { };
          

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            options.HttpOnly = false;

           string[] Model_Array = new string[]{
           Cart_Model.size,
           Cart_Model.color,
           Cart_Model.price.ToString(),
           Cart_Model.quantity.ToString()
            };

            if (Request.Cookies["Product"] != null) {
                var cookies = JsonConvert.DeserializeObject<Dictionary<string, string[]>>
                    (Request.Cookies["Product"]);

                cookies.Add(Cart_Model.name, Model_Array);
                Response.Cookies.Append("Product", JsonConvert.SerializeObject(cookies), options);
                return Ok(Cart_Model);
            }
            Cookies_Dictionary.Add(Cart_Model.name, Model_Array);
            Response.Cookies.Append("Product",JsonConvert.SerializeObject(Cookies_Dictionary),
                options);

            return Ok(Cart_Model);
        }

        [HttpGet("api/GetCookies")]
        public IActionResult GetCookies()
        {
            return Ok(Request.Cookies["Product"]);
        }

        [HttpDelete("api/DeleteCookies")]
        public IActionResult DeleteCookies()
        {
            Response.Cookies.Delete("Product");
            return Ok("Delete Confirm");
        }
        [HttpDelete("api/DelteByKey")]
        public IActionResult DeleteCookieByKey(string key) {
            var cookies = JsonConvert.DeserializeObject<Dictionary<string, string[]>>
                    (Request.Cookies["Product"]);
            cookies.Remove(key);

            Response.Cookies.Append("Product", JsonConvert.SerializeObject(cookies));
            return Ok();
        }
        [HttpPatch("/api/EditCookies")]
        public IActionResult EditCookies(string  key, [FromBody] Cart Cart_Model) {
            var cookies = JsonConvert.DeserializeObject<Dictionary<string, string[]>>
                    (Request.Cookies["Product"]);

            var PatchCookiesByKey = cookies.FirstOrDefault(x => x.Key == key);
            if (PatchCookiesByKey.Key != null) {

                string[] Model_Array = new string[]{
                           Cart_Model.size,
                           Cart_Model.color,
                           Cart_Model.price.ToString(),
                           Cart_Model.quantity.ToString()
                 };
                //
                cookies[PatchCookiesByKey.Key] = Model_Array;
                Response.Cookies.Append("Product", JsonConvert.SerializeObject(cookies));
                return Ok($"Cookies by Key: {key} was edit ");
            }
            throw new Exception($"Cookies by Key {key} isn't in cooki");
        }
    }
}
