using RapidCard.Web.Api.DB;
using RapidCard.Web.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RapidCard.Web.Api.Bussines
{
    public class CardService
    {
        private readonly RapidPayContext _rapidPayContext;
        
        public CardService(RapidPayContext rapidPayContext, FeeService paymentFeeService) 
        {
            _rapidPayContext = rapidPayContext;            
        }

        public Card CreateCard()
        {            
            var newcard = new Card { 
                code = Create16DigitNumber(),
                balance = 1
            };
            
            _rapidPayContext.CardTtems.Add(newcard);
            var result = _rapidPayContext.SaveChanges();

            if(result == 0)
            {
                return null;
            }

            return newcard;
        }

        public bool Pay(string code, double amount)
        {
            var card = GetCard(code);
            card.balance = card.balance + amount;

            var result = _rapidPayContext.SaveChanges();

            if (result == 0)
            {
                return false;
            }

            return true;
        }
        public Card GetCard(string code)
        {
            return _rapidPayContext.CardTtems.FirstOrDefault(x => x.code == code);
        }

        private string Create16DigitNumber()
        {
            var builder = new StringBuilder();
            Random r = new Random();
            while (builder.Length < 16)
            {
                builder.Append(r.Next(10).ToString());
            }
            return builder.ToString();
        }
    }
}
