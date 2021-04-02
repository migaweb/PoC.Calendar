using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Calendar.Data.Model
{
  public class Appointment
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
  }
}
