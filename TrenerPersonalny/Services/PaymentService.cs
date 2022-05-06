using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stripe;
using TrenerPersonalny.Models.Orders;
using TrenerPersonalny.Data;
using TrenerPersonalny.Models;

namespace TrenerPersonalny.Services
{
    public class PaymentService
    {
        private readonly IConfiguration _config;

        public PaymentService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(OrderPayment OrderPayment, Trainers trainer)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
            var service = new PaymentIntentService();


            var intent = new PaymentIntent();

            var price = trainer.Price * 100;
            if (string.IsNullOrEmpty(OrderPayment.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = price,
                    Currency = "pln",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = price
                };
                await service.UpdateAsync(OrderPayment.PaymentIntentId, options);
            }
            return intent;
        }
    }
}
