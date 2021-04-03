using AutoMapper;

namespace PoC.Calendar.WASM.Server.Profiles
{
  public class MapperProfiles : Profile
  {
    public MapperProfiles()
    {
      CreateMap<Data.Model.Appointment, Shared.Appointment>().ReverseMap();
      CreateMap<Shared.AppointmentBase, Data.Model.Appointment>();
      CreateMap<Shared.AppointmentBase, Shared.EventBaseDto>();
      CreateMap<Shared.EventDto, Shared.Appointment>();
    }
  }
}
