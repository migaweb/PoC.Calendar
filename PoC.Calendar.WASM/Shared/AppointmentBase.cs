using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Shared
{
  public class AppointmentBase
  {
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Text { get; set; }
  }
}
