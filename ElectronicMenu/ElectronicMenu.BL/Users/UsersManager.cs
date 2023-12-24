using AutoMapper;
using ElectronicMenu.BL.Users.Entities;
using ElectronicMenu.DataAccess;
using ElectronicMenu.DataAccess.Entities;

namespace ElectronicMenu.BL.Users
{
    public class UsersManager : IUsersManager
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;

        public UsersManager(IRepository<UserEntity> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void DeleteUser(Guid id)
        {
            UserEntity? entity = _userRepository.GetById(id);

            if (entity == null)
            {
                throw new ArgumentException("Нет пользователя по заданному id");
            }

            _userRepository.Delete(entity);
        }

        public UserModel UpdateUser(Guid id, UpdateUserModel model)
        {
            UserEntity? entity = _userRepository.GetById(id);

            if (entity == null)
            {
                throw new ArgumentException("Нет пользователя по заданному id");
            }

            UserEntity newEntity = _mapper.Map<UserEntity>(model);
            newEntity.Id = entity.Id;
            newEntity.ExternalId = entity.ExternalId;
            newEntity.CreationTime = entity.CreationTime;
            newEntity.ModificationTime = entity.ModificationTime;

            _userRepository.Save(newEntity);

            return _mapper.Map<UserModel>(newEntity);
        }
    }
}
