using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Decorations
{
    public class Ornament : Decoration
    {
        private const int comfortOrnamentInitial = 1;
        private const decimal priceOrnamentInitial = 5;
        public Ornament() : base(comfortOrnamentInitial, priceOrnamentInitial)
        {
        }

    }
}
