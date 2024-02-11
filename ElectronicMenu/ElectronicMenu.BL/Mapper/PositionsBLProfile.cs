using AutoMapper;
using ElectronicMenu.BL.Positions.Entities;
using ElectronicMenu.DataAccess.Entities;

namespace ElectronicMenu.BL.Mapper
{
    public class PositionsBLProfile : Profile
    {
        public PositionsBLProfile()
        {
            CreateMap<PositionEntity, PositionModel>()
                .ForMember(x => x.Id, y => y.MapFrom(src => src.Id));

            CreateMap<CreatePositionModel, PositionEntity>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.ExternalId, y => y.Ignore())
                .ForMember(x => x.ModificationTime, y => y.Ignore())
                .ForMember(x => x.CreationTime, y => y.Ignore());

            CreateMap<UpdatePositionModel, PositionEntity>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.ExternalId, y => y.Ignore())
                .ForMember(x => x.ModificationTime, y => y.Ignore())
                .ForMember(x => x.CreationTime, y => y.Ignore());
        }
    }
}
