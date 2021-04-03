using Microsoft.AspNetCore.Components;
using PoC.Calendar.WASM.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Calendar.WASM.Client.Shared.Components
{
  public partial class CalendarHub : ComponentBase
  {
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Inject]
    public CalendarHubManager CalendarHubManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
      await CalendarHubManager.Start();
    }
  }
}
