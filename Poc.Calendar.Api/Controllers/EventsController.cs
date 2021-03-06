using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoC.Calendar.Data;
using PoC.Calendar.WASM.Shared;
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
    private readonly IPublishEndpoint _publishEndpoint;

    public EventsController(EventsDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
      _context = context;
      _mapper = mapper;
      _publishEndpoint = publishEndpoint;
    }

    [HttpGet("calendar/{id:int}")]
    public async Task<IActionResult> GetAllAppointments(int id)
    {
      var eventEntities = await _context.Events.Where(e => e.CalendarId == id).ToListAsync();
      var eventDtos = _mapper.Map<IList<EventDto>>(eventEntities);

      return Ok(eventDtos);
    }

    [HttpGet("{id:int}", Name = "GetEventById")]
    public async Task<IActionResult> GetEventById(int id)
    {
      var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

      if (eventEntity == null) return NotFound();

      return Ok(_mapper.Map<EventDto>(eventEntity));
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(EventBaseDto eventBaseDto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);

      var eventEntity = _mapper.Map<Data.Model.Event>(eventBaseDto);
      await _context.Events.AddAsync(eventEntity);
      await _context.SaveChangesAsync();

      await _publishEndpoint.Publish(_mapper.Map<Common.Contracts.EventItemCreated>(eventEntity));

      return CreatedAtRoute(nameof(GetEventById),
                            new { id = eventEntity.Id },
                            _mapper.Map<EventDto>(eventEntity));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventBaseDto eventBaseDto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);

      var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

      if (eventEntity == null) return BadRequest($"No event with id = {id} found.");

      _mapper.Map(eventBaseDto, eventEntity);

      _context.Events.Attach(eventEntity);
      await _context.SaveChangesAsync();

      await _publishEndpoint.Publish(_mapper.Map<Common.Contracts.EventItemUpdated>(eventEntity));

      return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
      var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

      if (eventEntity == null) return NoContent();

      _context.Events.Remove(eventEntity);
      await _context.SaveChangesAsync();

      await _publishEndpoint.Publish(_mapper.Map<Common.Contracts.EventItemDeleted>(eventEntity));

      return NoContent();
    }
  }
}
