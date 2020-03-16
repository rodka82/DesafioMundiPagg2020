using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MundiPagg.Payment.Application.Interfaces;
using MundiPagg.Payment.Domain.Models;

namespace MundiPagg.Payment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IOrderRequestService _orderRequestService;

        public PaymentController(IOrderRequestService orderRequestService)
        {
            _orderRequestService = orderRequestService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderRequest>> Get()
        {
            return Ok(_orderRequestService.GetOrderRequests());
        }

        [HttpPost]
        public IActionResult Post(string postData)
        {
            _orderRequestService.CreateOrder(postData);
            return Ok(postData);
        }
    }
}
