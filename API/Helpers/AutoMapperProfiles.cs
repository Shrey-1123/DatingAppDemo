using System.Linq;
using API.DTO;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    // This class MAPs one object to another
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // < map from, map to>
            CreateMap<AppUser, MemberDTO>()
             .ForMember(dest => dest.PhotoUrl, opt=>opt.MapFrom(src=>
             src.Photos.FirstOrDefault(x=>x.IsMain).Url))
             .ForMember(dest=>dest.Age, opt=>opt.MapFrom(src=>
             src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDTO>();

            CreateMap<MemberUpdateDTO,AppUser>();

            CreateMap<RegisterDTO,AppUser>();
        }
    }
}