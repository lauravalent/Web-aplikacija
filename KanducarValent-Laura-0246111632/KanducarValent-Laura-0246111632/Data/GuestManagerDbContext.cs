using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace KanducarValent_Laura_0246111632.Migrations
{
    public class GuestManagerDbContext(DbContextOptions<GuestManagerDbContext> options) : IdentityDbContext<KanducarValent_Laura_0246111632.Data.ApplicationUser>(options)
    {
    }
}

