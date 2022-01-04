using Microsoft.EntityFrameworkCore;
using RapidCard.Web.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidCard.Web.Api.DB
{
    public class RapidPayContext: DbContext
    {
        public RapidPayContext(DbContextOptions<RapidPayContext> options): base (options)
        {

        }

        public DbSet<Card> CardTtems { get; set; }
        public DbSet<PaymentFee> PaymentFeeTtems { get; set; }

    }
}
