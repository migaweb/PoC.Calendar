using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PoC.Calendar.Common.Contracts;
using PoC.Calendar.Data;
using PoC.Calendar.WASM.Server.Hubs;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Server.Consumers
{
  public class EventItemDeletedConsumer : IConsumer<EventItemDeleted>
  {
    private readonly CalendarDbContext _calendarDbContext;
    private readonly IHubContext<CalendarHub> _calendarHub;

    public EventItemDeletedConsumer(CalendarDbContext calendarDbContext, IHubContext<CalendarHub> calendarHub)
    {
      _calendarDbContext = calendarDbContext;
      _calendarHub = calendarHub;
    }
    public async Task Consume(ConsumeContext<EventItemDeleted> context)
    {
      var message = context.Message;

      var appointmentEntity = await _calendarDbContext.Appointments.FirstOrDefaultAsync(e => e.Id == message.Id);

      if (appointmentEntity == null) return;

      _calendarDbContext.Appointments.Remove(appointmentEntity);
      _calendarDbContext.SaveChanges();

      await _calendarHub.Clients.All.SendAsync("appointmentsChanged", null);
    }
  }
}
