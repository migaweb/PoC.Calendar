using AutoMapper;
using PoC.Calendar.Data;

namespace PoC.Calendar.Api.Profiles
{
  public class MapperProfiles : Profile
  {
    public MapperProfiles()
    {
      CreateMap<Data.Model.Event, Dtos.EventDto>();
      CreateMap<Dtos.EventBaseDto, Data.Model.Event>();
    }
  }
}
