
using Microsoft.AspNetCore.Mvc;
using RepositoryPatern.Data.Access.Services.IRepositories;
using RepositoryPatern.Models;


namespace RepositoryPatern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderDetailsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrderDetails()
        {
            var orderDetails = await _unitOfWork.OrderDetail.GetAllAsync();
            return Ok(orderDetails);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetail(int orderId)
        {
            var orderDetail = await _unitOfWork.OrderDetail.GetByIdAsync(orderId);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(OrderDetail orderDetail)
        {
            await _unitOfWork.OrderDetail.AddAsync(orderDetail);
            await _unitOfWork.SaveChangesAsync();
            return Ok(orderDetail);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(OrderDetail orderDetail)
        {
            var existingOrder = await _unitOfWork.OrderDetail.GetByIdAsync(orderDetail.OrderId);
            if (existingOrder == null)
            {
                return NotFound();
            }
            existingOrder.ProductId= orderDetail.ProductId;
            existingOrder.Quantity= orderDetail.Quantity;
            existingOrder.Discount= orderDetail.Discount;
            await _unitOfWork.OrderDetail.UpdateAsync(existingOrder);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var order = await _unitOfWork.OrderDetail.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            await _unitOfWork.OrderDetail.RemoveAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        [Route("/applyDiscount/{orderId}/{discount}")]
        public async Task<IActionResult> ApplyDiscount(int orderId, decimal discount)
        {
            if (await _unitOfWork.OrderDetail.ApplyDiscountAsync(orderId, discount))
            {
                await _unitOfWork.SaveChangesAsync();
                return Ok("discount added");
            }
            return NotFound();
        }

    }
}
