
using Microsoft.EntityFrameworkCore;
using tasiapi.Models;

namespace tasiapi.Data
{
    public class IssueDbContext : DbContext
    {
        public IssueDbContext(DbContextOptions<IssueDbContext>options)
            :base(options)
        {
        }
        public DbSet<Issue> Issues { get; set; }

    }
}

