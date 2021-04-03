using AutoMapper;
using Microsoft.Extensions.Options;
using PoC.Calendar.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Server.CloudStore
{
  public class EventsApi : IEventsApi
  {
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly int _calendarId;
    private readonly string _baseApiUrl;

    public EventsApi(IMapper mapper, IOptions<CalendarSettings> config)
    {
      _httpClient = new HttpClient();
      _mapper = mapper;
      _calendarId = config.Value.CalendarId;
      _baseApiUrl = config.Value.CalendarApiBaseUrl;
    }

    public async Task<Shared.Appointment> CreateEvent(Shared.AppointmentBase appointment)
    {
      var eventBaseDto = _mapper.Map<Shared.EventBaseDto>(appointment);
      eventBaseDto.CalendarId = _calendarId;

      var result = await _httpClient.PostAsJsonAsync(_baseApiUrl, eventBaseDto);

      if (!result.IsSuccessStatusCode)
      {
        return null;
      }

      var eventDto = await result.Content.ReadFromJsonAsync<Shared.EventDto>();
      var appointmentDto = _mapper.Map<Shared.Appointment>(eventDto);

      return appointmentDto;
    }

    public async Task<bool> UpdateEvent(int id, Shared.AppointmentBase appointment)
    {
      var eventBaseDto = _mapper.Map<Shared.EventBaseDto>(appointment);
      eventBaseDto.CalendarId = _calendarId;

      var result = await _httpClient.PutAsJsonAsync($"{_baseApiUrl}/{id}", eventBaseDto);

      return result.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteEvent(int id)
    {
      var result = await _httpClient.DeleteAsync($"{_baseApiUrl}/{id}");

      return result.IsSuccessStatusCode;
    }
  }
}
