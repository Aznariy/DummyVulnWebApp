using DummyVulnWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DummyVulnWebApp.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
