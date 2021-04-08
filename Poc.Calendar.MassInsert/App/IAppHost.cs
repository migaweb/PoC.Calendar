using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Calendar.MassInsert.App
{
  public interface IAppHost
  {
    Task RunAsync();
  }
}
