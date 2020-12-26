using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rabbits
{
    public class Cage
    {
        List<Rabbit> data;
        string name;
        int capacity;
        
        public Cage(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            data = new List<Rabbit>();
        }

        public List<Rabbit> Data { get => data; set => data = value; }
        public string Name { get => name; set => name = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public int Count { get => data.Count; }

        public void Add(Rabbit rabbit)
        {
            if (this.Capacity<this.Count)
            {
                return;
            }

            foreach (var rab in data)
            {
                if (rab.Name==rabbit.Name)
                {
                    return;
                }
            }
            data.Add(rabbit);
        }

        public bool RemoveRabbit(string name)
        {
            bool exist = false;
            if (data.Count>0)
            {
                foreach (var rab in data)
                {
                    if (rab.Name == name)
                    {
                        data.Remove(rab);
                        exist = true;
                    }
                }
            }
            return exist;
        }

        public void RemoveSpecies(string species)
        {
            if (data.Count>0)
            {
                data.RemoveAll(r => r.Species == species);
            }
        }

        public Rabbit SellRabbit(string name)
        {
            Rabbit toRet = new Rabbit("", "");
            foreach (var rab in data)
            {
                if (rab.Name==name)
                {
                    rab.Available = false;
                    toRet = rab;
                    break;
                }
            }
            return toRet;
        }

        public Rabbit[] SellRabbitsBySpecies(string species)
        {
            foreach (var rab in data)
            {
                if (rab.Species==species)
                {
                    rab.Available = false;
                }
            }

            List<Rabbit> rabbitsArr = data.Where(r => r.Species == species).ToList();
            return rabbitsArr.ToArray();
        }

        public string Report()
        {
            Rabbit[] arr = data.Where(r => r.Available == true).ToArray();
            var sb = new StringBuilder();

            string output = $"Rabbits available at {this.Name}:";
            sb.AppendLine(output);

            if (arr.Length>0)
            {
                foreach (var rab in arr)
                {
                    sb.AppendLine(rab.ToString());
                }
            }
            return sb.ToString().TrimEnd();
        }
    }
}
