using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using tasiapi.Data;
using Npgsql;

using tasiapi.Models;

namespace tasiapi.Data
{
    
}
public class TasinmazDbContext : DbContext
{
   
    public TasinmazDbContext(DbContextOptions<TasinmazDbContext> options) : base(options) { }

    public DbSet<Tasinmaz> tasinmazlar { get; set; }
    public DbSet<User> User { get; set; }
}


