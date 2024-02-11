using ElectronicMenu.BL.Positions.Entities;

namespace ElectronicMenu.Services.Controllers.Positions.Entities
{
    public class PositionsPageResponse
    {
        public int Page { get; set; }
        public PositionModel[]? Positions { get; set; }
    }
}
