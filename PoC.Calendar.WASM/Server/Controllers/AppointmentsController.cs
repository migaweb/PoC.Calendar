using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoC.Calendar.Data;
using PoC.Calendar.WASM.Server.CloudStore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AppointmentsController : ControllerBase
  {
    private readonly CalendarDbContext _context;
    private readonly IEventsApi _eventsApi;
    private readonly IMapper _mapper;

    public AppointmentsController(CalendarDbContext context, IEventsApi eventsApi, IMapper mapper)
    {
      _context = context;
      _eventsApi = eventsApi;
      _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAppointments()
    {
      var appointmentEntities = await _context.Appointments.ToListAsync();
      var appointmentDtos = _mapper.Map<IList<Shared.Appointment>>(appointmentEntities);

      return Ok(appointmentDtos);
    }

    [HttpGet("{id:int}", Name = "GetAppointmentById")]
    public async Task<IActionResult> GetAppointmentById(int id)
    {
      var appointmentEntity = await _context.Appointments.FirstOrDefaultAsync(e => e.Id == id);

      if (appointmentEntity == null) return NotFound();

      return Ok(_mapper.Map<Shared.Appointment>(appointmentEntity));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment(Shared.AppointmentBase appointment)
    {
      var appointmentDto = await _eventsApi.CreateEvent(appointment);

      return CreatedAtRoute(nameof(GetAppointmentById), 
                            new { id = appointmentDto.Id }, 
                            appointmentDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Shared.AppointmentBase appointment)
    {
      await _eventsApi.UpdateEvent(id, appointment);

      return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
      await _eventsApi.DeleteEvent(id);

      return NoContent();
    }
  }
}
