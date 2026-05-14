using Microsoft.EntityFrameworkCore;
using SponsorshipWorkflow.Api.Models;
using System.Collections.Generic;

namespace SponsorshipWorkflow.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}