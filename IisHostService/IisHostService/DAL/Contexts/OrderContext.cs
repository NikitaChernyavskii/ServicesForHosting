using IisHostService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IisHostService.DAL.Contexts
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
        :base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
