using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidCard.Web.Api.Model
{
    public class PaymentFee
    {
        public int id { get; set; }
        public string key { get; set; }
        public double fee { get; set; }
    }
}
