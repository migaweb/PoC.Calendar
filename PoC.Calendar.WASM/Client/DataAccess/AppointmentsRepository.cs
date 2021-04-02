using PoC.Calendar.WASM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Client.DataAccess
{
  public class AppointmentsRepository : IAppointmentsRepository
  {
    private readonly HttpClient _httpClient;

    public AppointmentsRepository(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<List<Appointment>> GetAppointmentsAsync(DateTime startDate, DateTime endDate)
    {
      return await _httpClient.GetFromJsonAsync<List<Appointment>>("api/appointments");
    }
  }
}
