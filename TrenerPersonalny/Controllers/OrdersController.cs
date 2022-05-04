using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Data;
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
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            return await _context.Orders
               .Include(o => o.OrderTrainer)
               .Where(x => x.BuyerId == User.Identity.Name)
               .ToListAsync();
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            return await _context.Orders
                 .Include(o => o.OrderTrainer)
                 .Where(x => x.BuyerId == User.Identity.Name && x.Id == id)
                 .FirstOrDefaultAsync();
        }

        [HttpPost("{trainerId}")]
        public async Task<ActionResult<Order>> CreateOrder(int trainerId)
        {

            var trainerP = await _context.Trainers.Where(o => o.Id.Equals(trainerId)).Include(p => p.Person).Include(c => c.Person.Client).FirstOrDefaultAsync();

            //var trainerP = await _context.Trainers.FindAsync();

            var trainerOrdered = new TrainerPersonOrdered
            {
                TrainerId = trainerP.Id,
                Name = trainerP.Person.Client.UserName,
                PictureUrl = trainerP.Person.ProfileImg
            };

            var orderTrainer = new OrderTrainer
            {
                TrainerOrdered = trainerOrdered,
                Price = trainerP.Price
            };

            var order = new Order
            {
                OrderTrainer = orderTrainer,
                BuyerId = User.Identity.Name
            };

            _context.Orders.Add(order);
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetOrder", new {id = order.Id}, order.Id);
            return BadRequest("Problem z utworzeniem zamówienia");
        }
    }

}

