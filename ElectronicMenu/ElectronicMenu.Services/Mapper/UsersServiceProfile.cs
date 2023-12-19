using AutoMapper;
using ElectronicMenu.BL.Positions.Entities;
using ElectronicMenu.BL.Users.Entities;
using ElectronicMenu.Services.Controllers.Positions.Entities;
using ElectronicMenu.Services.Controllers.Users.Entities;

namespace ElectronicMenu.Services.Mapper
{
    public class UsersServiceProfile : Profile
    {
        public UsersServiceProfile()
        {
            CreateMap<UpdateUserRequest, UpdateUserModel>();
            CreateMap<RegisterUserRequest, CreateUserModel>();

        }
    }
}
