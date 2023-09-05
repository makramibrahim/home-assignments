using AutoMapper;
using DatabaseEntities.Data;
using PersonDemo.API.Models;

namespace PersonDemo.API.CommonConfig
{
  public static class AutoMapper
  {
    private static IMapper _mapper = null!;

    public static IMapper Mapper => _mapper ??= RegisterMappings();

    public static IMapper RegisterMappings()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<DPerson, Person>().ReverseMap();
        cfg.CreateMap<DPersonVersion, PersonVersion>().ReverseMap();
      });

      return config.CreateMapper();
    }
  }
}
