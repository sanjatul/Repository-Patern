using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatern.Models;
using RepositoryPatern.Services.IRepositories;
using Serilog;

namespace RepositoryPatern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IUnitOfWork unitOfWork, ILogger<ProductsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            Log.Information("Getting all products.");
            var products = await _unitOfWork.Product.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            Log.Information("Getting product by ID: {productId}", id);
            var product = await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null)
            {
                Log.Warning("Product with ID {productId} not found.", id);
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            Log.Information("Creating a new product.");
            await _unitOfWork.Product.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,Product product)
        {
            Log.Information("Updating product with ID: {productId}", id);
            var existingProduct = await _unitOfWork.Product.GetByIdAsync(id);
            if (existingProduct == null)
            {
                Log.Warning("Product with ID {productId} not found.", id);
                return NotFound();
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            await _unitOfWork.Product.UpdateAsync(existingProduct);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Log.Information("Deleting product with ID: {productId}", id);
            var product = await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null)
            {
               Log.Warning("Product with ID {productId} not found.", id);
                return NotFound();
            }
            await _unitOfWork.Product.RemoveAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("totalproduct")]
        public async Task<IActionResult> GetProductCountAsync()
        {
            Log.Information("Counting products in the database");
            var products = await _unitOfWork.Product.GetProductCountAsync();
            return Ok(products);
        }

    }
}
