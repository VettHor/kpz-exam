using AutoMapper;
using back_end.DTOs;
using back_end.Models;

namespace back_end.Mapping
{
    public class Mapping
    {
        public static IMapper Create()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CalendarDto, Calendar>();
                cfg.CreateMap<RecordDto, Record>();
                cfg.CreateMap<TherapistDto, Therapist>();

            });
            return config.CreateMapper();
        }
    }
}
