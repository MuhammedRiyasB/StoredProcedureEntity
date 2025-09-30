using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StoredProcedure.Model;
using StoredProcedure.Services;

namespace StoredProcedure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var get = await _productServices.GetAll();
            return Ok(get);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _productServices.Create(product);
                return Ok("Product added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _productServices.Update(product);
                return Ok("product updated successfully");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid id");

            try
            {
                await _productServices.Delete(id);
                return Ok("Product deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
