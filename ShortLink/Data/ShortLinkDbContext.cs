using Microsoft.EntityFrameworkCore;
using ShortLink.Models;

namespace ShortLink.Data
{
    public class ShortLinkDbContext : DbContext
    {
        public ShortLinkDbContext(DbContextOptions<ShortLinkDbContext> options) : base(options) { }

        public DbSet<Link> Links { get; set; }

    }
}
