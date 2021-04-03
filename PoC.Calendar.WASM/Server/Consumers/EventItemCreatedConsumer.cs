using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PoC.Calendar.Common.Contracts;
using PoC.Calendar.Data;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Server.Consumers
{
  public class EventItemCreatedConsumer : IConsumer<EventItemCreated>
  {
    private readonly CalendarDbContext _calendarDbContext;
    private readonly IMapper _mapper;

    public EventItemCreatedConsumer(CalendarDbContext calendarDbContext, IMapper mapper)
    {
      _calendarDbContext = calendarDbContext;
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
    }
  }
}
