﻿using Microsoft.EntityFrameworkCore;
using PoC.Calendar.Data.Configurations;
using PoC.Calendar.Data.Model;


namespace PoC.Calendar.Data
{
  public class CalendarDbContext : DbContext
  {
    public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options){}

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.ApplyConfiguration(new AppointmentConfiguration());
      builder.ApplyConfiguration(new EventConfiguration());
      base.OnModelCreating(builder);
    }
  }
}
