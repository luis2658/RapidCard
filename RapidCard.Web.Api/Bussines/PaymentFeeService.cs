using RapidCard.Web.Api.DB;
using RapidCard.Web.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RapidCard.Web.Api.Bussines
{
    public class PaymentFeeService
    {
        private readonly RapidPayContext _rapidPayContext;
        private readonly FeeService _feeProvider;

        private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public PaymentFeeService(RapidPayContext rapidPayContext, FeeService paymentFeeProvider)
        {
            _rapidPayContext = rapidPayContext;
            _feeProvider = paymentFeeProvider;
        }

        public double GetFee()
        {
            semaphore.WaitAsync();
            try
            {
                var currentFee = _feeProvider.GetFee();

                if (currentFee == null)
                {
                    var newFee = CreateFeeEntry();
                    _feeProvider.SetFee(newFee);
                    currentFee = newFee;
                }

                return currentFee.fee;
            }
            finally
            {
                semaphore.Release();
            }
        }

        public PaymentFee CreateFeeEntry()
        {
            var fee = GetLastFee();
            var key = _feeProvider.DateTimeKey();

            var newfeeEntry = new PaymentFee
            {
                key = key,
                fee = fee
            };
            
            _rapidPayContext.Add(newfeeEntry);
            _rapidPayContext.SaveChanges();
            
            return newfeeEntry;
        }
        private double GetLastFee()
        {
            var feeEntry = _rapidPayContext.PaymentFeeTtems.LastOrDefault();

            if (feeEntry == null)
            {
                return _feeProvider.SelectUFE();
            }

            return feeEntry.fee * _feeProvider.SelectUFE();
        }
    }
}
