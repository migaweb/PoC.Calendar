using MassTransit;
using Microsoft.EntityFrameworkCore;
using PoC.Calendar.Common.Contracts;
using PoC.Calendar.Data;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Server.Consumers
{
  public class EventItemDeletedConsumer : IConsumer<EventItemDeleted>
  {
    private readonly CalendarDbContext _calendarDbContext;

    public EventItemDeletedConsumer(CalendarDbContext calendarDbContext)
    {
      _calendarDbContext = calendarDbContext;
    }
    public async Task Consume(ConsumeContext<EventItemDeleted> context)
    {
      var message = context.Message;

      var appointmentEntity = await _calendarDbContext.Appointments.FirstOrDefaultAsync(e => e.Id == message.Id);

      if (appointmentEntity == null) return;

      _calendarDbContext.Appointments.Remove(appointmentEntity);
      _calendarDbContext.SaveChanges();
    }
  }
}
