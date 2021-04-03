using Microsoft.EntityFrameworkCore;
using PoC.Calendar.Data.Configurations;
using PoC.Calendar.Data.Model;
using System;

namespace PoC.Calendar.Data
{
  public class CalendarDbContext : DbContext
  {
    public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options){}

    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.ApplyConfiguration(new AppointmentConfiguration());
      base.OnModelCreating(builder);
    }
  }
}
