using Microsoft.EntityFrameworkCore;
using WebApi.Data.Entities;

namespace WebApi.Data
{
    public sealed class SqLiteDbContext : DbContext
    {
        public SqLiteDbContext(DbContextOptions<SqLiteDbContext> options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoicesLines { get; set; }
    }
}