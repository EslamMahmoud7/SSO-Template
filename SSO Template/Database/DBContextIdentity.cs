using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSO_Template.Models;
namespace SSO_Template.Database;

public class DBContextIdentity : IdentityDbContext<AppUser>
{
    public DBContextIdentity(DbContextOptions Dboptions) : base(Dboptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        List<IdentityRole> Roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name= "Admin",
                NormalizedName= "ADMIN"
            },
            new IdentityRole
            {
                Name= "User",
                NormalizedName= "USER"
            },
        };
        builder.Entity<IdentityRole>().HasData(Roles);
    }
}
