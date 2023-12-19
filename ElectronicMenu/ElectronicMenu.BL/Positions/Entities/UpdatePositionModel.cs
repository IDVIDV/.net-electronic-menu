﻿namespace ElectronicMenu.BL.Positions.Entities
{
    public class UpdatePositionModel
    {
        public string PositionName { get; set; }
        public string ImgLink { get; set; }
        public float Price { get; set; }
        public float Weight { get; set; }
        public float Calories { get; set; }
        public int IsVegan { get; set; }
        public string Ingridients { get; set; }
    }
}
