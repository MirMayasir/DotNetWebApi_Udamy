using AutoMapper;
using UdamyCourse.Model.Domain;
using UdamyCourse.Model.DTOs;

namespace UdamyCourse.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionDto, Region>().ReverseMap();
            CreateMap<AddWalkDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
       
            CreateMap<DifficultyDto, Difficulty>().ReverseMap();
        }
    }
}
