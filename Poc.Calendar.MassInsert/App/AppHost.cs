
using PoC.Calendar.WASM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Calendar.MassInsert.App
{
  public class AppHost : IAppHost
  {
    private readonly IHttpClientFactory _httpClientFactory;

    public AppHost(IHttpClientFactory httpClientFactory)
    {
      _httpClientFactory = httpClientFactory;
    }

    public async Task RunAsync()
    {
      var absoluteStartDate = new DateTime(2021, 4, 5, 8, 0, 0);
      var absoluteEndDate = new DateTime(2021, 4, 9, 17, 0, 0);

      while (absoluteStartDate < absoluteEndDate)
      {
        var endDate = absoluteStartDate.AddMinutes(30);
        var eventBaseDto = new EventBaseDto { 
          Start = absoluteStartDate, 
          End = endDate,
          Text = $"{randomText()}", 
          CalendarId = 1
        };

        absoluteStartDate = endDate;
        if (absoluteStartDate.Hour > 17)
          absoluteStartDate = new DateTime(absoluteStartDate.Year, absoluteStartDate.Month, absoluteStartDate.AddDays(1).Day, 8, 0, 0);

        await Send(eventBaseDto);

        await Task.Delay(200);
      }
    }

    private static string randomText()
    {
      var length = 7;
      const string chars = "abcdefghijklmnopqrstuvwxyzåäö";

      return new String(
          Enumerable.Repeat(chars, length)
               .Select(s => s[new Random().Next(s.Length)])
               .ToArray());
    }

    private async Task Send(EventBaseDto @event)
    {
      var httpClient = _httpClientFactory.CreateClient();
      var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44353/api/events");
      request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      var serJson = System.Text.Json.JsonSerializer.Serialize(@event);
      request.Content = new StringContent(serJson);
      request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

      try
      {
        using (var response = await httpClient.SendAsync(request))
        {
          Console.BackgroundColor = ConsoleColor.Green;
          Console.ForegroundColor = ConsoleColor.Black;
          Console.WriteLine("Send successful: {0}", serJson);
          Console.ResetColor();

          response.EnsureSuccessStatusCode();
        }
      }
      catch (HttpRequestException ex)
      {
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Send failed: {0}: {1}: {2}", ex.Message, ex.InnerException?.Message, ex.StackTrace);
        Console.ResetColor();
      }
    }
  }
}
