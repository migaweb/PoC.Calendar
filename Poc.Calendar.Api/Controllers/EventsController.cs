using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoC.Calendar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Calendar.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EventsController : ControllerBase
  {
    private readonly EventsDbContext _context;
    private readonly IMapper _mapper;

    public EventsController(EventsDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet("calendar/{id:int}")]
    public async Task<IActionResult> GetAllAppointments(int id)
    {
      var eventEntities = await _context.Events.Where(e => e.CalendarId == id).ToListAsync();
      var eventDtos = _mapper.Map<IList<Dtos.EventDto>>(eventEntities);

      return Ok(eventDtos);
    }

    [HttpGet("{id:int}", Name = "GetEventById")]
    public async Task<IActionResult> GetEventById(int id)
    {
      var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

      if (eventEntity == null) return NotFound();

      return Ok(_mapper.Map<Dtos.EventDto>(eventEntity));
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(Dtos.EventBaseDto eventBaseDto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);

      var eventEntity = _mapper.Map<Data.Model.Event>(eventBaseDto);
      await _context.Events.AddAsync(eventEntity);
      await _context.SaveChangesAsync();

      return CreatedAtRoute(nameof(GetEventById),
                            new { id = eventEntity.Id },
                            _mapper.Map<Dtos.EventDto>(eventEntity));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] Dtos.EventBaseDto eventBaseDto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);

      var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

      if (eventEntity == null) return BadRequest($"No event with id = {id} found.");

      _mapper.Map(eventBaseDto, eventEntity);

      _context.Events.Attach(eventEntity);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
      var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

      if (eventEntity == null) return NoContent();

      _context.Events.Remove(eventEntity);
      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}
