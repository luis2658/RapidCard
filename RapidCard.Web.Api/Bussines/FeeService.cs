using Microsoft.Extensions.Caching.Memory;
using RapidCard.Web.Api.DB;
using RapidCard.Web.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidCard.Web.Api.Bussines
{
    public class FeeService
    {        
        private readonly IMemoryCache _cache;

        public FeeService( IMemoryCache cache)
        {            
            _cache = cache;
        }

        public PaymentFee GetFee()
        {
            var key = DateTimeKey();

            var feeEntry = _cache.Get<PaymentFee>(key);

            if (feeEntry == null)
            {
                feeEntry = _cache.Get<PaymentFee>(key);
                if (feeEntry != null)
                {
                    return feeEntry;
                }
            }
            return feeEntry;
        }

        public void SetFee(PaymentFee feeEntry)
        {
            var fee = _cache.Set(feeEntry.key, feeEntry);
        }

        public double SelectUFE()
        {
            Random r = new Random();
            var amount = r.Next(1, 20000)*1.0;
            return amount/10000.0;
        }
        public string DateTimeKey()
        {
            var date = DateTime.Now;
            return $"{date.Year}{date.Month.ToString("00")}{date.Day.ToString("00")}{date.Hour.ToString("00")}";
        }
    }
}
