using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrLead.ViewModels;
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
    }
}