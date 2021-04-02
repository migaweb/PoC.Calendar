using Microsoft.AspNetCore.Components;
using PoC.Calendar.WASM.Client.DataAccess;
using PoC.Calendar.WASM.Client.Pages;
using PoC.Calendar.WASM.Shared;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Client.Shared.Components
{
  public partial class Scheduler : ComponentBase
  {
    #region Injections
    
    [Inject]
    public DialogService DialogService { get; set; }

    [Inject]
    public NotificationService NotificationService { get; set; }

    [Inject]
    public IAppointmentsRepository AppointmentsRepository { get; set; }

    #endregion
    public IList<Appointment> Appointments { get; set; } = new List<Appointment>();

    private RadzenScheduler<Appointment> scheduler;
    Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();

    async Task LoadData(SchedulerLoadDataEventArgs args)
    {
      var startDate = args.Start;
      var endDate = args.End;
      Appointments = await AppointmentsRepository.GetAppointmentsAsync(startDate, endDate);
    }

    async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
    {
      Console.WriteLine($"SlotSelect: Start={args.Start} End={args.End}");

      Appointment data = await DialogService.OpenAsync<AddAppointmentPage>("Add Appointment",
          new Dictionary<string, object> { { "Start", args.Start }, { "End", args.End } });

      if (data != null)
      {
        var appointment = await AppointmentsRepository.AddAppointment(data);

        if (appointment != null)
        {
          Appointments.Add(appointment);
          // Either call the Reload method or reassign the Data property of the Scheduler
          await scheduler.Reload();
        }
        else
        {
          NotificationService.Notify(new NotificationMessage 
          { 
            Severity = NotificationSeverity.Warning, 
            Detail = "An error occured on the server. The appointment was not added.",
            Duration = 30000,
            Summary = "Error"
          });
        }
      }
    }

    async Task OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<Appointment> args)
    {
      Console.WriteLine($"AppointmentSelect: Appointment={args.Data.Text}");

      var result = await DialogService.OpenAsync<EditAppointmentPage>("Edit Appointment", new Dictionary<string, object> { { "Appointment", args.Data } });

      if (result == null)
      {
        // Deleted
        await scheduler.Reload();
        return;
      }
      else if ( !(await AppointmentsRepository.UpdateAppointment(args.Data)) )
      {
        NotificationService.Notify(new NotificationMessage
        {
          Severity = NotificationSeverity.Warning,
          Detail = "An error occured on the server. The appointment was not updated.",
          Duration = 30000,
          Summary = "Error"
        });

        return;
      }
    }

    void OnAppointmentRender(SchedulerAppointmentRenderEventArgs<Appointment> args)
    {
      // Never call StateHasChanged in AppointmentRender - would lead to infinite loop

      if (args.Data.Text == "Birthday")
      {
        args.Attributes["style"] = "background: red";
      }
    }
  }
}
