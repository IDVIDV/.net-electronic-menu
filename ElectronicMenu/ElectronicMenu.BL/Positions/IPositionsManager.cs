using ElectronicMenu.BL.Positions.Entities;

namespace ElectronicMenu.BL.Positions
{
    public interface IPositionsManager
    {
        PositionModel CreatePosition(CreatePositionModel model);
        void DeletePosition(int positionId);
        PositionModel UpdatePosition(int positionId, UpdatePositionModel model);
    }
}
