using App.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DataAccess.Contexts
{
    public class AppDbContex:IdentityDbContext<AppUser>
    {
        public AppDbContex(DbContextOptions<AppDbContex> options):base(options)
        {

        }
        public DbSet<TeamMember> TeamMembers { get; set; }
    }
}
