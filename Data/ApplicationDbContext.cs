using CS436CVC3PROJECT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CS436CVC3PROJECT.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet สำหรับ Entities เท่านั้น
        public DbSet<Inventory> Inventorys { get; set; }
        public DbSet<EmailMessage> EmailMessages { get; set; }

    }
}
