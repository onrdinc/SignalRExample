namespace SignalR.API.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using SignalR.API.Models;
    using System;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=Test;trusted_connection=true; trustServerCertificate=true;");
        }
        public DbSet<Product> Products { get; set; }
    }
}
