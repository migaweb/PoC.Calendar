using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Calendar.Common.Contracts
{
  public record EventItemCreated(int Id, int CalendarId, string Text, DateTime Start, DateTime End);
  public record EventItemUpdated(int Id, int CalendarId, string Text, DateTime Start, DateTime End);
  public record EventItemDeleted(int Id, int CalendarId);
}
