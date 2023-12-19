using AutoMapper;
using ElectronicMenu.BL.Positions.Entities;
using ElectronicMenu.Services.Controllers.Positions.Entities;

namespace ElectronicMenu.Services.Mapper
{
    public class PositionsServiceProfile : Profile
    {
        public PositionsServiceProfile()
        {
            CreateMap<PositionsFilter, PositionModelFilter>();
            CreateMap<CreatePositionRequest, CreatePositionModel>();
            CreateMap<UpdatePositionRequest, UpdatePositionModel>();
        }
    }
}
