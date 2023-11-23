using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.API.Services;

namespace SignalR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;
        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task AddProduct()
        {
            _service.AddData();
            //return CreatedAtAction(nameof(SingleGet), new { id = response.Item.Id }, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAll();
           return Ok(response);

        }
    }
}
