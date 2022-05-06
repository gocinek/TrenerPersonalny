using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Data;
using TrenerPersonalny.Extensions;
using TrenerPersonalny.Models;
using TrenerPersonalny.Models.DTOs.Orders;
using TrenerPersonalny.Models.Orders;

namespace TrenerPersonalny.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly ApiDbContext _context;
        public OrdersController(ApiDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            return await _context.Orders
               .ProjectOrderToOrderDto()
               .Where(x => x.BuyerId == User.Identity.Name)
               .ToListAsync();
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            return await _context.Orders
                 .ProjectOrderToOrderDto()
                 .Where(x => x.BuyerId == User.Identity.Name && x.Id == id)
                 .FirstOrDefaultAsync();
        }

        [HttpPost("{trainerId}")]
        public async Task<ActionResult<int>> CreateOrder(int trainerId, CreateOrderDto createOrderDto)
        {

            var trainerP = await _context.Trainers
                .Where(o => o.Id.Equals(trainerId))
                .Include(p => p.Person)
                .Include(c => c.Person.Client)
                .FirstOrDefaultAsync();
            //var trainerP = await _context.Trainers.FindAsync();
            var expDate = await _context.Orders.Where(o => o.BuyerId == User.Identity.Name).OrderBy(o => o.Id).LastOrDefaultAsync();

            var trainerOrdered = new TrainerPersonOrdered
            {
                TrainerId = trainerP.Id,
                Name = trainerP.Person.Client.UserName,
                PictureUrl = trainerP.Person.ProfileImg
            };

            var orderTrain = new List<OrderTrainer>();
            var orderTrainer = new OrderTrainer
            {
                TrainerOrdered = trainerOrdered,
                Price = trainerP.Price
            };
            orderTrain.Add(orderTrainer);

            DateTime eD;
            if (expDate == null || expDate.Expired < DateTime.Now.Date)
            {
                eD = DateTime.Now.Date.AddMonths(1);
               // Console.WriteLine("dodano do dzisiaj");
            }
            else
            {                
                eD = expDate.Expired.Date.AddMonths(1);
               // Console.WriteLine("dodano");               
            }
           
            var order = new Order
            {
                OrderTrainer = orderTrain,
                BuyerId = User.Identity.Name,
                Expired = eD,
                Summary = trainerP.Price,
                UsedreditCard = createOrderDto.UsedCreditCard
            };

            _context.Orders.Add(order);
            if (createOrderDto.SaveCreditCard)
            {
                var user = await _context.Users
                    .Include(a => a.UserCreditCard)
                    .FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);

                var creditCard = new UserCreditCard
                {
                    cardNumber = createOrderDto.UsedCreditCard.cardNumber,
                    expDate = createOrderDto.UsedCreditCard.expDate,
                    cvv = createOrderDto.UsedCreditCard.cvv,
                    nameOnCard = createOrderDto.UsedCreditCard.nameOnCard
                };
                user.UserCreditCard = creditCard;
            }
           
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetOrder", new {id = order.Id}, order.Id);
            return BadRequest("Problem z utworzeniem zamówienia");
        }
    }

}