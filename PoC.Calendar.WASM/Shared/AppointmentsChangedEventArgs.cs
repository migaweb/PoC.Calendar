using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Shared
{
  public class AppointmentsChangedEventArgs : EventArgs
  {
    public Appointment Data { get; set; }
  }
}
