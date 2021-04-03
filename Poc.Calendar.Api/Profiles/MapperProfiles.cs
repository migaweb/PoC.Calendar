using AutoMapper;
using PoC.Calendar.Data;
using PoC.Calendar.WASM.Shared;

namespace PoC.Calendar.Api.Profiles
{
  public class MapperProfiles : Profile
  {
    public MapperProfiles()
    {
      CreateMap<Data.Model.Event, EventDto>();
      CreateMap<EventBaseDto, Data.Model.Event>();
    }
  }
}
