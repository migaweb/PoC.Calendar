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
      CreateMap<Data.Model.Event, Common.Contracts.EventItemCreated>();
      CreateMap<Data.Model.Event, Common.Contracts.EventItemUpdated>();
      CreateMap<Data.Model.Event, Common.Contracts.EventItemDeleted>();
    }
  }
}
