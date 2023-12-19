using AutoMapper;
using ElectronicMenu.BL.Positions.Entities;
using ElectronicMenu.DataAccess;
using ElectronicMenu.DataAccess.Entities;

namespace ElectronicMenu.BL.Positions
{
    public class PositionsManager : IPositionsManager
    {
        private readonly IRepository<PositionEntity> _positionsRepository;
        private readonly IMapper _mapper;

        public PositionsManager(IRepository<PositionEntity> positionsRepository, IMapper mapper)
        {
            _positionsRepository = positionsRepository;
            _mapper = mapper;
        }

        public PositionModel CreatePosition(CreatePositionModel model)
        {
            //не могу придумать валидацию уровня BL

            PositionEntity entity = _mapper.Map<PositionEntity>(model);

            _positionsRepository.Save(entity);

            return _mapper.Map<PositionModel>(entity);
        }

        public void DeletePosition(Guid positionId)
        {
            PositionEntity? entity = _positionsRepository.GetById(positionId);

            if (entity == null)
            {
                throw new ArgumentException("Нет позиции по заданному id");
            }

            _positionsRepository.Delete(entity);
        }

        public PositionModel UpdatePosition(Guid positionId, UpdatePositionModel model)
        {
            //не могу придумать валидацию уровня BL для данных

            PositionEntity? entity = _positionsRepository.GetById(positionId);

            if (entity == null)
            {
                throw new ArgumentException("Нет позиции по заданному id");
            }

            PositionEntity newEntity = _mapper.Map<PositionEntity>(model);
            newEntity.Id = entity.Id;
            newEntity.ExternalId = entity.ExternalId;
            newEntity.CreationTime = entity.CreationTime;
            newEntity.ModificationTime = entity.ModificationTime;

            _positionsRepository.Save(newEntity);

            return _mapper.Map<PositionModel>(newEntity);
        }
    }
}
