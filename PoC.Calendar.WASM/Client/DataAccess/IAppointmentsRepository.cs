using PoC.Calendar.WASM.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Client.DataAccess
{
  public interface IAppointmentsRepository
  {
    Task<Appointment> AddAppointment(AppointmentBase appointment);
    Task<bool> DeleteAppointment(Appointment appointment);
    Task<List<Appointment>> GetAppointmentsAsync(DateTime startDate, DateTime endDate);
    Task<bool> UpdateAppointment(Appointment appointment);
  }
}