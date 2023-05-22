using FunTranslator.Data.Models;
using Microsoft.EntityFrameworkCore.Design;

using Microsoft.EntityFrameworkCore;
using UtdPayApi.Data.Models;

namespace UtdPayApi.Data
{
    public class PaymentDbContext : DbContext
    {
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Payment>? Payments { get; set; }

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {

        }
    }
}