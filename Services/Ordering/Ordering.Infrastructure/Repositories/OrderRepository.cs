

using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Contracts;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<Order> AddOrder(Order order)
        {
           var newOrder = _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return await _context.Orders.FindAsync(order.Id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
           return await _context.Orders.Where(x=> x.UserName == userName).ToListAsync();
        }
    }
}
