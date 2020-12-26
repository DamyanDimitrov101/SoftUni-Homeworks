using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Decorations
{
    public class Plant : Decoration
    {
        private const int comfortPlantInitial = 5;
        private const decimal pricePlantInitial = 10;

        public Plant() : base(comfortPlantInitial, pricePlantInitial)
        {
        }
    }
}
