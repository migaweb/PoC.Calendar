using PoC.Calendar.WASM.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Client.DataAccess
{
  public interface IAppointmentsRepository
  {
    Task<List<Appointment>> GetAppointmentsAsync(DateTime startDate, DateTime endDate);
  }
}