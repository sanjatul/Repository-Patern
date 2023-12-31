﻿
using Microsoft.AspNetCore.Mvc;
using RepositoryPatern.Data.Access.Services.IRepositories;
using RepositoryPatern.Models;


namespace RepositoryPatern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _unitOfWork.Product.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _unitOfWork.Product.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return Ok(product);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var existingProduct = await _unitOfWork.Product.GetByIdAsync(product.ProductId);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name= product.Name;
            existingProduct.Description= product.Description;
            existingProduct.Price= product.Price;
            await _unitOfWork.Product.UpdateAsync(existingProduct);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null)
            {
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
            var products = await _unitOfWork.Product.GetProductCountAsync();
            return Ok(products);
        }

    }
}
