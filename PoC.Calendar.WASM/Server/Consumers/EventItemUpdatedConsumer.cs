using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PoC.Calendar.Common.Contracts;
using PoC.Calendar.Data;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Server.Consumers
{
  public class EventItemUpdatedConsumer : IConsumer<EventItemUpdated>
  {
    private readonly CalendarDbContext _calendarDbContext;
    private readonly IMapper _mapper;

    public EventItemUpdatedConsumer(CalendarDbContext calendarDbContext, IMapper mapper)
    {
      _calendarDbContext = calendarDbContext;
      _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<EventItemUpdated> context)
    {
      var message = context.Message;

      var appointmentEntity = await _calendarDbContext.Appointments.FirstOrDefaultAsync(e => e.Id == message.Id);

      if (appointmentEntity == null) 
      {
        appointmentEntity = _mapper.Map<Data.Model.Appointment>(message);
        await _calendarDbContext.Appointments.AddAsync(appointmentEntity);
      }
      else
      {
        appointmentEntity.End = message.End;
        appointmentEntity.Start = message.Start;
        appointmentEntity.Text = message.Text;
      }

      await _calendarDbContext.SaveChangesAsync();
    }
  }
}
