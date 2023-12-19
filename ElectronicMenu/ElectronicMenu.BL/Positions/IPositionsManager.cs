using ElectronicMenu.BL.Positions.Entities;

namespace ElectronicMenu.BL.Positions
{
    public interface IPositionsManager
    {
        PositionModel CreatePosition(CreatePositionModel model);
        void DeletePosition(Guid positionId);
        PositionModel UpdatePosition(Guid positionId, UpdatePositionModel model);
    }
}
