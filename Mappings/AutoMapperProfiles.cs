using AutoMapper;
using UdamyCourse.Model.Domain;
using UdamyCourse.Model.DTOs;

namespace UdamyCourse.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, AddRegionDto>().ReverseMap();
        }
    }
}
