﻿using AquaShop.Models.Decorations.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Decorations
{
    public abstract class Decoration : IDecoration
    {
        private int comfort;
        private decimal price;

        protected Decoration(int comfort, decimal price)
        {
            Comfort = comfort;
            Price = price;
        }

        public int Comfort 
        {
            get=>this.comfort;
            private set 
            {
                if (value>0)
                {
                    this.comfort = value;
                }
            }
        }

        public decimal Price
        {
            get => this.price;
            private set
            {
                if (value>0m)
                {
                    this.price = value;
                }
            }
        }
    }
}
