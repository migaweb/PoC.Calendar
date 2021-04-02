using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoC.Calendar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AppointmentsController : ControllerBase
  {
    private readonly CalendarDbContext _context;
    private readonly IMapper _mapper;

    public AppointmentsController(CalendarDbContext context, IMapper mapper)
    {
      _context = context;
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
      var appointmentEntity = _mapper.Map<Data.Model.Appointment>(appointment);
      await _context.Appointments.AddAsync(appointmentEntity);
      await _context.SaveChangesAsync();

      return CreatedAtRoute(nameof(GetAppointmentById), 
                            new { id = appointmentEntity.Id }, 
                            _mapper.Map<Shared.Appointment>(appointmentEntity));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Shared.AppointmentBase appointment)
    {
      var appointmentEntity = await _context.Appointments.FirstOrDefaultAsync(e => e.Id == id);

      if (appointmentEntity == null) return BadRequest($"No appointment with id = {id} found.");

      _mapper.Map(appointment, appointmentEntity);

      _context.Appointments.Attach(appointmentEntity);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
      Console.WriteLine($"Deleting item: {id}");
      var appointmentEntity = await _context.Appointments.FirstOrDefaultAsync(e => e.Id == id);

      if (appointmentEntity == null) return NoContent();

      _context.Appointments.Remove(appointmentEntity);
      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}
