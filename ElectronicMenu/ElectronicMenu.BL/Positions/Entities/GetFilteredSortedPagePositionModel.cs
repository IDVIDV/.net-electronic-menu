namespace ElectronicMenu.BL.Positions.Entities
{
    public class GetFilteredSortedPagePositionModel
    {
        public int Page {  get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public bool SortDirection { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxWeight { get; set; }
        public float? MinWeight { get; set; }
        public float? MaxCalories { get; set; }
        public float? MinCalories { get; set; }
    }
}
