using PoC.Calendar.WASM.Shared;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Server.CloudStore
{
  public interface IEventsApi
  {
    Task<Appointment> CreateEvent(AppointmentBase appointment);
    Task<bool> DeleteEvent(int id);
    Task<bool> UpdateEvent(int id, AppointmentBase appointment);
  }
}