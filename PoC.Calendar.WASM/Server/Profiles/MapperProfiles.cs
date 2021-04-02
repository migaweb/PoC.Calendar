using AutoMapper;

namespace PoC.Calendar.WASM.Server.Profiles
{
  public class MapperProfiles : Profile
  {
    public MapperProfiles()
    {
      CreateMap<Data.Model.Appointment, Shared.Appointment>();
      CreateMap<Shared.AppointmentBase, Data.Model.Appointment>();
    }
  }
}
