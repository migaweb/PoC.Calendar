using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using PoC.Calendar.WASM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Client.Hubs
{
  public class CalendarHubManager : IAsyncDisposable
  {
    private HubConnection _hubConnection;
    private readonly NavigationManager _navigationManager;

    public event EventHandler<AppointmentsChangedEventArgs> AppointmentsChanged;

    public CalendarHubManager(NavigationManager navigationManager)
    {
      _navigationManager = navigationManager;
      InitHubConnection();
    }

    public async Task Start()
    {
      await _hubConnection.StartAsync();
      Console.WriteLine("Hub is started");
    }

    private void InitHubConnection()
    {
      
      _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri("/hubs/calendar"))
            .Build();

      _hubConnection.Reconnected += hubConnectionReconnected;
      _hubConnection.Reconnecting += hubConnectionReconnecting;
      _hubConnection.Closed += hubConnectionClosed;

      _hubConnection.On<Appointment>("appointmentsChanged", appointmentsChanged);
    }

    private void appointmentsChanged(Appointment apoointment)
    {
      OnAppointmentsChanged(new AppointmentsChangedEventArgs { Data = apoointment });
    }

    private void OnAppointmentsChanged(AppointmentsChangedEventArgs e)
    {
      var handler = AppointmentsChanged;

      if (handler != null)
      {
        handler(this, e);
      }
    }

    private Task hubConnectionReconnected(string arg)
    {
      Console.WriteLine("Hub was reconnected");

      return Task.CompletedTask;
    }

    private Task hubConnectionReconnecting(Exception arg)
    {
      Console.WriteLine("Hub is reconnecting");
      return Task.CompletedTask;
    }

    private Task hubConnectionClosed(Exception arg)
    {
      Console.WriteLine("Hub is closed");
      return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
      await _hubConnection.DisposeAsync();
    }
  }
}
