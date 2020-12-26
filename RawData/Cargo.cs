namespace RawData
{
    public class Cargo
    {
        string type;
        int weight;

        public Cargo(string type, int wieght)
        {
            this.type = type;
            this.weight = wieght;
        }

        public string Type { get => type; set => type = value; }
        public int Weight { get => weight; set => weight = value; }
    }
}