using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using ServiceLayer.Exceptions;
namespace ServiceLayer.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
   // [Authorize(Roles = "Admin")]
    public class ProductInfoController : ControllerBase
    {
        private readonly IProductService<Product> _productService;
        private readonly ILogger<ProductInfoController> _logger;
        public ProductInfoController(IProductService<Product> productService, ILogger<ProductInfoController> logger)
        {
            this._productService = productService;
            this._logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        [Route("/")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts()
        {
          var products=await _productService.GetAllProducts();
            if (products.Count > 0)
            {
                _logger.LogInformation($"Total Product:{products.Count}");
                return Ok(products);//Status Code:200
            }
            else
            {
                return NotFound();//Status Code: 404
            }
        }

        [HttpGet("GetById/{id:int:range(1,100)}")]
        //[Route("GetById/{id}")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById(int id)
        {
            var productById =await _productService.GetProductById(id);
            if(productById is not null)
            {
                return Ok(productById);//200
            }
            else
            {
                throw new NotFoundException("Product Id does not exist");
                return NotFound();//404
            }
        }

        [HttpGet("GetByName/{name:alpha:maxlength(50)}")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductByName(string name) 
        {
           var productByName=await _productService.GetProductByName(name);
            if (productByName is not null)
            {
                return Ok(productByName);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost("AddProduct")]
        [Produces("application/json")]
        [Consumes("application/json")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> PostNewProduct([FromBody] Product product)
        {
           var count=await _productService.AddProduct(product);
            if (count > 0) 
            {
                //return Ok();//200
                return Created(HttpContext.Request.Path, product);//201
            }
            else
            {
                throw new BadRequestException("Invalid Data");
                return BadRequest();//400
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        [Produces("application/json")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

          var count=await  _productService.DeleteProduct(id);
            if (count > 0) 
            {
                return Ok();//200 
            }
            else
            {
                return BadRequest();//400
            }
        }


        [HttpPut("EditProduct")]
        [Produces("application/json")]
        [Consumes("application/json")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
           var count=await _productService.UpdateProduct(product);
            if(count > 0)
            {
                return Accepted(product);//202
            }
            else
            {
                return BadRequest();//400
            }
        }
    }
}
