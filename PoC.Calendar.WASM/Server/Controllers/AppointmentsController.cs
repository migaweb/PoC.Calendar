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
  }
}
