using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using TrenerPersonalny.Data;
using TrenerPersonalny.Extensions;
using TrenerPersonalny.Models.DTOs.Orders;
using TrenerPersonalny.Models.Orders;
using TrenerPersonalny.Services;

namespace TrenerPersonalny.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly PaymentService _paymentService;
        private readonly ApiDbContext _context;
        private readonly IConfiguration _config;
        public PaymentsController(PaymentService paymentService, ApiDbContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("{trainerId}")]
        public async Task<ActionResult<OrderPaymentDto>> CreateOrUpdatePaymentIntent(int trainerId)
        {
            var orderPayment = await _context.OrderPayments.Where( o => o.BuyerId == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (orderPayment == null) {
                orderPayment = new OrderPayment();
            }

            var trainer = await _context.Trainers.Where(t => t.Id == trainerId).FirstOrDefaultAsync();
            var intent = await _paymentService.CreateOrUpdatePaymentIntent(orderPayment, trainer);

            if (intent == null) return BadRequest(new ProblemDetails { Title = "Problem creating payment intent" });

            orderPayment.PaymentIntentId = orderPayment.PaymentIntentId ?? intent.Id;
            orderPayment.ClientSecret = orderPayment.ClientSecret ?? intent.ClientSecret;
            orderPayment.BuyerId = orderPayment.BuyerId ?? User.Identity.Name;

            _context.Update(orderPayment);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return BadRequest(new ProblemDetails { Title = "Problem updating orderPayment with intent" });

            return orderPayment.MapOrderPaymentToDto();
        }

        [HttpGet]
        public async Task<ActionResult<OrderPaymentDto>> GetPayment()
        {

            var orderPayment = await _context.OrderPayments.Where(o => o.BuyerId == User.Identity.Name).FirstOrDefaultAsync();

            return Ok(orderPayment);

        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
                _config["StripeSettings:WhSecret"]);

            var charge = (Charge)stripeEvent.Data.Object;

            var order = await _context.Orders.FirstOrDefaultAsync(x =>
                x.PaymentIntentId == charge.PaymentIntentId);

            if (charge.Status == "Succeeded") order.OrderStatus = OrderStatus.PaymentReceived;

            await _context.SaveChangesAsync();

            return new EmptyResult();
        }
    }
}