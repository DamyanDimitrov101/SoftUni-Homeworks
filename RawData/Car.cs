using System;
using System.Collections.Generic;
using System.Text;

namespace RawData
{
    public class Car
    {
        string model;
        Engine engine;
        int weight;
        string color;

        public Car(string model, Engine engine)
        {
            Model = model;
            Engine = engine;
            Weight = -1;
            Color = null;
        }

        public Car(string model, Engine engine, int weight) : this(model, engine)
        {
            Weight = weight;
        }

        public Car(string model, Engine engine, string color) : this(model, engine)
        {
            Color = color;
        }

        public Car(string model, Engine engine, int weight, string color) : this(model, engine)
        {
            Color = color;
            Weight = weight;
        }

        public string Model { get => model; set => model = value; }
        public Engine Engine { get => engine; set => engine = value; }
        public int Weight { get => weight; set => weight = value; }
        public string Color { get => color; set => color = value; }

        public override string ToString()
        {
            string weigthHolder = this.Weight == -1 ? "n/a" : this.Weight.ToString();
            string colorHolder = this.Color == null ? "n/a" : this.Color;
            string powerHolder = this.Engine.Power == -1 ? "n/a" : this.Engine.Power.ToString();
            string efficiencyHolder = this.Engine.Efficiency == null ? "n/a" : this.Engine.Efficiency;
            string displacementHolder = this.Engine.Displacement == -1 ? "n/a" : this.Engine.Displacement.ToString();

            return $"{this.Model}:{System.Environment.NewLine}  {this.Engine.Model}:{System.Environment.NewLine}    Power: {powerHolder}{System.Environment.NewLine}    Displacement: {displacementHolder}{System.Environment.NewLine}    Efficiency: {efficiencyHolder}{System.Environment.NewLine}  Weight: {weigthHolder}{System.Environment.NewLine}  Color: {colorHolder}"; 
        }
    }

    
}
