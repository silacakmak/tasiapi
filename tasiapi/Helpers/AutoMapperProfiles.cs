using AutoMapper;
using tasiapi.Dtos;
using tasiapi.Models;

namespace tasiapi.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public  AutoMapperProfiles()
        {

            CreateMap<Tasinmaz, TasinmazForListDto>().ReverseMap();
            CreateMap<Tasinmaz,TasinmazForDetailDto>().ReverseMap();
        }

    }


}

