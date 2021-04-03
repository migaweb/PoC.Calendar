using Microsoft.EntityFrameworkCore;
using PoC.Calendar.Data.Configurations;
using PoC.Calendar.Data.Model;


namespace PoC.Calendar.Data
{
  public class EventsDbContext : DbContext
  {
    public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options){}

    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.ApplyConfiguration(new EventConfiguration());
      base.OnModelCreating(builder);
    }
  }
}
