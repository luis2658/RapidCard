using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidCard.Web.Api.Bussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidCard.Web.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentFeeController : ControllerBase
    {
        private readonly PaymentFeeService _paymentFeeService;
        private readonly CardService _cardService;

        public PaymentFeeController(PaymentFeeService paymentFeeService, CardService cardService)
        {
            _paymentFeeService = paymentFeeService;
            _cardService = cardService;
        }

        [HttpGet]
        public ActionResult GetCurrentFee()
        {
            return Ok(_paymentFeeService.GetFee());
        }
    }
}
