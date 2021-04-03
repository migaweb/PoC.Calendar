using System;
using System.ComponentModel.DataAnnotations;

namespace PoC.Calendar.Data.Model
{
  public class Event
  {
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public DateTime Start { get; set; }
    [Required]
    public DateTime End { get; set; }
    [Required]
    public string Text { get; set; }
    [Required]
    public int CalendarId { get; set; }
  }
}
