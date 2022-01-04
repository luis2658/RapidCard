using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidCard.Web.Api.Model
{
    public class Card
    {
        public int id{ get; set; }
        public string code { get; set; }
        public double balance { get; set; }
    }
}
