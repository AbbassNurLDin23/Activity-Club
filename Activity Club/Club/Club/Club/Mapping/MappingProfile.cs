using AutoMapper;
using Club.Core.DataModels;
using Club.Resources;

namespace Club.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<LookupResource, Lookup>();
            CreateMap<UserResource, User>();
            CreateMap<MemberResource, Member>();
            CreateMap<GuideResource, Guide>();
            CreateMap<EventResource, Event>();
        }
    }
}
