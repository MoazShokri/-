using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosBank.Dto;
using PosBank.Models;
using PosBank.Services;

namespace PosBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAlLOrder()
        {
            var order = await _orderService.GetAll();
            return Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewOrder(OrderDto orderDto)
        {
            var order = new Order
            {
                CustomerName = orderDto.CustomerName,
                CustomerPhone = orderDto.CustomerPhone,
                OrderAmount = orderDto.OrderAmount,
                OrderDate = orderDto.OrderDate,
                ProductId= orderDto.ProductId,
            };
            await _orderService.Add(order);
            return Ok(order);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDto orderDto)
        {
            var order = await _orderService.GetById(id);
            if (order == null)
                return NotFound("This order is not restricted");
            order.CustomerName = orderDto.CustomerName;
            order.CustomerPhone = orderDto.CustomerPhone;
            order.OrderAmount = orderDto.OrderAmount;
            order.OrderDate = orderDto.OrderDate;
            _orderService.Update(order);
            return Ok(order);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null)
                return NotFound("this order is not exit in Orders");
            _orderService.Delete(order);
            return Ok(order);
        }


    }
}
