using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P05_GreedyTimes
{
    public class Bag
    {
        private Dictionary<string, Dictionary<string, long>> bagCollection;

        private long gold;
        private long stones;
        private long cash;

        public Bag()
        {
            BagCollection = new Dictionary<string, Dictionary<string, long>>();

            Gold = 0;
            Stones = 0;
            Cash = 0;
        }

        public Dictionary<string, Dictionary<string, long>> BagCollection { get => bagCollection; set => bagCollection = value; }
        public long Gold { get => gold; set => gold = value; }
        public long Stones { get => stones; set => stones = value; }
        public long Cash { get => cash; set => cash = value; }


        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var x in BagCollection)
            {
                sb.AppendLine($"<{x.Key}> ${x.Value.Values.Sum()}");
                foreach (var item2 in x.Value.OrderByDescending(y => y.Key).ThenBy(y => y.Value))
                {
                    sb.AppendLine($"##{item2.Key} - {item2.Value}");
                }
            }
            return sb.ToString();
        }

        public void IncreaseCurrentItemValue(long count, string item)
        {
            if (item == "Gold")
            {
                this.gold += count;
            }
            else if (item == "Gem")
            {
                this.stones += count;
            }
            else if (item == "Cash")
            {
                this.cash += count;
            }
        }
        public void CheckIfItemContainsValue(Dictionary<string, Dictionary<string, long>> bag, string name, string item)
        {
            if (!bag[item].ContainsKey(name))
            {
                bag[item][name] = 0;
            }
        }

        public void CheckIfBagContainsItemAndCreateIfNot(Dictionary<string, Dictionary<string, long>> bag, string item)
        {
            if (!bag.ContainsKey(item))
            {
                bag[item] = new Dictionary<string, long>();
            }
        }

        public bool CheckIfItemValuesSumIsBiggerThanTheElementValuesSum(Dictionary<string, Dictionary<string, long>> bag, long count, string item, string element)
        {
            return bag[item].Values.Sum() + count > bag[element].Values.Sum();
        }
    }
}
