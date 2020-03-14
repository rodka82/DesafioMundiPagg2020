using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MundiPagg.OrderProcessor.Application.Interfaces;
using MundiPagg.OrderProcessor.Domain.Models;

namespace MundiPagg.OrderProcessor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProcessorController : ControllerBase
    {
        private readonly IOrderResponseService _orderResponseService;

        public OrderProcessorController(IOrderResponseService orderResponseService)
        {
            _orderResponseService = orderResponseService;
        }
        // GET api/orderProcessor
        [HttpGet]
        public ActionResult<IEnumerable<OrderResponse>> Get()
        {
            return Ok(_orderResponseService.GetOrderRequests());
        }
    }
}
