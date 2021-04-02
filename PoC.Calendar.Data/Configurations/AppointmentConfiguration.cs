using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoC.Calendar.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Calendar.Data.Configurations
{
  public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
  {
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
      builder.HasData(
        new Appointment { Id=1, Start = DateTime.Today.AddDays(-2), End = DateTime.Today.AddDays(-2), Text = "Birthday" },
        new Appointment {Id=2, Start = DateTime.Today.AddDays(-11), End = DateTime.Today.AddDays(-10), Text = "Day off" },
        new Appointment {Id=3, Start = DateTime.Today.AddDays(-10), End = DateTime.Today.AddDays(-8), Text = "Work from home" },
        new Appointment {Id=4, Start = DateTime.Today.AddHours(10), End = DateTime.Today.AddHours(12), Text = "Online meeting" },
        new Appointment {Id=5, Start = DateTime.Today.AddHours(10), End = DateTime.Today.AddHours(13), Text = "Skype call" },
        new Appointment { Id = 6, Start = DateTime.Today.AddHours(14), End = DateTime.Today.AddHours(14).AddMinutes(30), Text = "Dentist appointment" }
      );
    }
  }
}
