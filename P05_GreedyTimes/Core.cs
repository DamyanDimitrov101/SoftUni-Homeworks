using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P05_GreedyTimes
{
    public class Core
    {
        private string[] safe;
        private long input;

        public Core(string[] safe, long input)
        {
            this.safe = safe;
            this.input = input;
        }

        public void Run()
        {
            var bag = new Bag();

            for (int i = 0; i < safe.Length; i += 2)
            {
                string name = safe[i];

                long count = long.Parse(safe[i + 1]);

                string item = CheckItemType(name);

                if (item == "" || item == string.Empty)
                {
                    continue;
                }
                else if (input < bag.BagCollection.Values.Select(x => x.Values.Sum()).Sum() + count)
                {
                    continue;
                }

                switch (item)
                {
                    case "Gem":
                        if (!bag.BagCollection.ContainsKey(item))
                        {
                            if (bag.BagCollection.ContainsKey("Gold"))
                            {
                                if (count > bag.BagCollection["Gold"].Values.Sum())
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (bag.CheckIfItemValuesSumIsBiggerThanTheElementValuesSum(bag.BagCollection, count, item, "Gold"))
                        {
                            continue;
                        }
                        break;
                    case "Cash":
                        if (!bag.BagCollection.ContainsKey(item))
                        {
                            if (bag.BagCollection.ContainsKey("Gem"))
                            {
                                if (count > bag.BagCollection["Gem"].Values.Sum())
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (bag.CheckIfItemValuesSumIsBiggerThanTheElementValuesSum(bag.BagCollection, count, item, "Gem"))
                        {
                            continue;
                        }
                        break;
                }

                bag.CheckIfBagContainsItemAndCreateIfNot(bag.BagCollection, item);

                bag.CheckIfItemContainsValue(bag.BagCollection, name, item);

                bag.BagCollection[item][name] += count;

                bag.IncreaseCurrentItemValue(count, item);
            }

            Console.WriteLine(bag);
        }
        private static string CheckItemType(string name)
        {
            string item = string.Empty;

            if (name.Length == 3)
            {
                item = "Cash";
            }
            else if (name.ToLower().EndsWith("gem"))
            {
                item = "Gem";
            }
            else if (name.ToLower() == "gold")
            {
                item = "Gold";
            }

            return item;
        }
    }
}
