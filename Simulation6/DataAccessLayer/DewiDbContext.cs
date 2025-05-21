using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Simulation6.Models;
using Simulation6.Models.LoginRegister;

namespace Simulation6.DataAccessLayer;

public class DewiDbContext:IdentityDbContext<AppUser>
{
    public DewiDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Profession> Professions { get; set; }
    public DbSet<Person> People { get; set; }
}