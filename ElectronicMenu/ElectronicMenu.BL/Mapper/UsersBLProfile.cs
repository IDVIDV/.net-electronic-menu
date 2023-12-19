using AutoMapper;
using ElectronicMenu.BL.Users.Entities;
using ElectronicMenu.DataAccess.Entities;

namespace ElectronicMenu.BL.Mapper
{
    public class UsersBLProfile : Profile
    {
        public UsersBLProfile() 
        {
            CreateMap<UserEntity, UserModel>()
                .ForMember(x => x.id, y => y.MapFrom(src => src.ExternalId));

            //??????
            CreateMap<CreateUserModel, UserEntity>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.ExternalId, y => y.Ignore())
                .ForMember(x => x.CreationTime, y => y.Ignore())
                .ForMember(x => x.ModificationTime, y => y.Ignore())
                .ForMember(x => x.Orders, y => y.Ignore());

            //??????????
            CreateMap<UpdateUserModel, UserEntity>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.ExternalId, y => y.Ignore())
                .ForMember(x => x.CreationTime, y => y.Ignore())
                .ForMember(x => x.ModificationTime, y => y.Ignore());
        }
    }
}
