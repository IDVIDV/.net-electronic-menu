using ElectronicMenu.BL.Positions.Entities;

namespace ElectronicMenu.BL.Positions
{
    public interface IPositionsProvider
    {
        IEnumerable<PositionModel> GetPositions(PositionModelFilter? filter = null);
        PositionModel GetPosition(int positionId);
        public IEnumerable<PositionModel> GetFilteredSortedPagePositions(GetFilteredSortedPagePositionModel? rules = null);
    }
}
