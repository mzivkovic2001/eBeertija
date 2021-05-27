using AutoMapper;
using ebeertijaBackend.DTOs;
using ebeertijaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebeertijaBackend.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Cjenik, CjenikDto>()
            .ForMember(dest => dest.IsEditable, opt =>
                {
                    opt.MapFrom(src => src.DatumPocetkaPrimjene.Date > DateTime.Now.Date);
                })
                .ForMember(dest => dest.StavkeCjenika, opt =>
                {
                    opt.MapFrom(src => src.StavkeCjenika.Where(s => s.IsActive).ToList());
                });
            CreateMap<CjenikDto, Cjenik>();
            CreateMap<StavkaCjenika, StavkaCjenikaDto>();
            CreateMap<StavkaCjenikaDto, StavkaCjenika>();

            CreateMap<Stol, StolDto>();
            CreateMap<StolDto, Stol>();
        }
    }
}
