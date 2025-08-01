using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UdamyCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            string[] products = new string[]
            {
                "Product 1",
                "Product 2",
                "Product 3"
            };

            return Ok(products);
        }

        [HttpGet("{name}")]

        public IActionResult GetByName(string name )
        {
            string[] products = new string[]
            {
                "Product 1",
                "Product 2",
                "Product 3"

            };

            var getName = products.FirstOrDefault(p => p.Equals(name, StringComparison.OrdinalIgnoreCase));
            if(getName == null)
            {
                return BadRequest("dosent exist");
            }

            return Ok(getName);
        }

    }
}
