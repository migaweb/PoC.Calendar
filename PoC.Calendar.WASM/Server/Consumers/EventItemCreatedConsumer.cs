using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PoC.Calendar.Common.Contracts;
using PoC.Calendar.Data;
using PoC.Calendar.WASM.Server.Hubs;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Server.Consumers
{
  public class EventItemCreatedConsumer : IConsumer<EventItemCreated>
  {
    private readonly CalendarDbContext _calendarDbContext;
    private readonly IHubContext<CalendarHub> _calendarHub;
    private readonly IMapper _mapper;

    public EventItemCreatedConsumer(CalendarDbContext calendarDbContext, IHubContext<CalendarHub> calendarHub, IMapper mapper)
    {
      _calendarDbContext = calendarDbContext;
      _calendarHub = calendarHub;
      _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<EventItemCreated> context)
    {
      var message = context.Message;
      var appointmentEntity = _mapper.Map<Data.Model.Appointment>(message);
      
      var exists = await _calendarDbContext.Appointments.AnyAsync(e => e.Id == appointmentEntity.Id);
      if (exists) return;

      await _calendarDbContext.Appointments.AddAsync(appointmentEntity);
      await _calendarDbContext.SaveChangesAsync();

      await _calendarHub.Clients.All.SendAsync("appointmentsChanged", _mapper.Map<Shared.Appointment>(appointmentEntity));
    }
  }
}
