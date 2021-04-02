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
    private readonly string _apiBaseUrl = "api/appointments";
    private readonly HttpClient _httpClient;

    public AppointmentsRepository(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<List<Appointment>> GetAppointmentsAsync(DateTime startDate, DateTime endDate)
    {
      return await _httpClient.GetFromJsonAsync<List<Appointment>>(_apiBaseUrl);
    }

    public async Task<Appointment> AddAppointment(AppointmentBase appointment)
    {
      var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, appointment);

      if (response.IsSuccessStatusCode)
      {
        return await response.Content.ReadFromJsonAsync<Appointment>();
      }

      return null;
    }

    public async Task<bool> UpdateAppointment(Appointment appointment)
    {
      var response = await _httpClient.PutAsJsonAsync<AppointmentBase>($"{_apiBaseUrl}/{appointment.Id}", appointment);

      return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAppointment(Appointment appointment)
    {
      var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{appointment.Id}");

      return response.IsSuccessStatusCode;
    }
  }
}
