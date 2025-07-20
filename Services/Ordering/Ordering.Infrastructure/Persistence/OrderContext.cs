

using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public OrderContext(DbContextOptions<DbContext> options):base(options) 
        {
            
        }

    }
}
