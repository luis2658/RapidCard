using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidCard.Web.Api.Bussines;
using RapidCard.Web.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidCard.Web.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly CardService _cardService;
        private readonly PaymentFeeService _paymentFeeService;
        public CardController(CardService cardService, PaymentFeeService paymentFeeService)
        {
            _cardService = cardService;
            _paymentFeeService = paymentFeeService;
        }

        [HttpGet]
        [Route("{code}")]
        public ActionResult GetBalance(string code)
        {
            var card = _cardService.GetCard(code);

            if (card == null)
            {
                return BadRequest(new { message = "Card Not Found" });
            }

            return Ok(card);
        }

        [HttpPost]
        [Route("")]
        public ActionResult CreateCard()
        {
            var card = _cardService.CreateCard();

            if (card == null)
            {
                return BadRequest(new { message = "Card Not Created" });
            }
            return Ok(card);
        }

        [HttpPut]
        [Route("{code}")]
        //[Route("")]
        public ActionResult Pay(string code, [FromQuery] int amount)
        {
            var fee = _paymentFeeService.GetFee();
            var result = _cardService.Pay(code, amount+fee);

            if (!result)
            {
                return BadRequest(new { message = "Transaction Not Processed" });
            }

            return Ok(new { message = "Transaction Processed" });
        }
    }
}
