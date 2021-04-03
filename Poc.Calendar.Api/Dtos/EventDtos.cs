using System;
using System.ComponentModel.DataAnnotations;

namespace PoC.Calendar.Api.Dtos
{
  public class EventDto : EventBaseDto 
  {
    [Required]
    public int Id { get; set; }
  }

  public class EventBaseDto
  {
    [Required]
    public int CalendarId { get; set; }
    [Required]
    public DateTime Start { get; set; }
    [Required]
    public DateTime End { get; set; }
    [Required]
    public string Text { get; set; }
  }

}
