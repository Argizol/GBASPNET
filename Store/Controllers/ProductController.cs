using Microsoft.AspNetCore.Mvc;
using NetStore.Abstraction;
using NetStore.Models;
using NetStore.Models.DTO;


namespace Store.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("getProduct")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpPost("addProduct")]
        public IActionResult AddProduct([FromBody] DTOProduct product)
        {
            var result = _productRepository.AddProduct(product);
            return Ok(result);
        }

        [HttpDelete("deleteProduct")]
        public IActionResult DeleteProduct([FromQuery] int id)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    if (!context.Products.Any(x => x.Id == id))
                    {
                        return NotFound();
                    }

                    Product product = context.Products.FirstOrDefault(x => x.Id == id)!;
                    context.Products.Remove(product);
                    context.SaveChanges();

                    return Ok();

                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("addProductPrice")]
        public IActionResult AddProductPrice([FromQuery] int id, int price)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    if (!context.Products.Any(x => x.Id == id))
                    {
                        return NotFound();
                    }

                    Product product = context.Products.FirstOrDefault(x => x.Id == id)!;
                    product.Price = price;
                    context.SaveChanges();

                    return Ok();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
