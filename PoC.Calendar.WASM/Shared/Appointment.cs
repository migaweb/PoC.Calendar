using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Shared
{
  public class Appointment : AppointmentBase
  {
    public int Id { get; set; }

    public bool Deleted { get; set; }
  }
}
