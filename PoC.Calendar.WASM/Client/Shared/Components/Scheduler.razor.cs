using Microsoft.AspNetCore.Components;
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
    [Inject]
    public DialogService DialogService { get; set; }
    
    private RadzenScheduler<Appointment> scheduler;
    Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();

    IList<Appointment> appointments = new List<Appointment>
    {
    new Appointment { Start = DateTime.Today.AddDays(-2), End = DateTime.Today.AddDays(-2), Text = "Birthday" },
    new Appointment { Start = DateTime.Today.AddDays(-11), End = DateTime.Today.AddDays(-10), Text = "Day off" },
    new Appointment { Start = DateTime.Today.AddDays(-10), End = DateTime.Today.AddDays(-8), Text = "Work from home" },
    new Appointment { Start = DateTime.Today.AddHours(10), End = DateTime.Today.AddHours(12), Text = "Online meeting" },
    new Appointment { Start = DateTime.Today.AddHours(10), End = DateTime.Today.AddHours(13), Text = "Skype call" },
    new Appointment { Start = DateTime.Today.AddHours(14), End = DateTime.Today.AddHours(14).AddMinutes(30), Text = "Dentist appointment" },
   
    };

    async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
    {
      Console.WriteLine($"SlotSelect: Start={args.Start} End={args.End}");

      Appointment data = await DialogService.OpenAsync<AddAppointmentPage>("Add Appointment",
          new Dictionary<string, object> { { "Start", args.Start }, { "End", args.End } });

      if (data != null)
      {
        appointments.Add(data);
        // Either call the Reload method or reassign the Data property of the Scheduler
        await scheduler.Reload();
      }
    }

    async Task OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<Appointment> args)
    {
      Console.WriteLine($"AppointmentSelect: Appointment={args.Data.Text}");

      await DialogService.OpenAsync<EditAppointmentPage>("Edit Appointment", new Dictionary<string, object> { { "Appointment", args.Data } });

      await scheduler.Reload();
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
