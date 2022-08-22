using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrLead.Models;

namespace UrLead.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UrLead.Models.LeadCategory>? LeadCategory { get; set; }
        public DbSet<UrLead.Models.Lead>? Lead { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Lead>().Property(l => l.OrganizationId).IsRequired(); // To pass ModelState validation. I will be non-nullable
            base.OnModelCreating(builder);
        }
    }
}