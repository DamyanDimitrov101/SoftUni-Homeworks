using Chronometer.Contracts;
using System;

namespace Chronometer
{
    static class Program
    {
        static void Main(string[] args)
        {
            IChronometer chronometer = new Chronometer();
            var input = Console.ReadLine().ToLower();
            
            while (input != "exit")
            {
                switch (input)
                {
                    case "start":
                        chronometer.Start();
                        break;
                    case "stop":
                        chronometer.Stop();
                        break;
                    case "lap":
                        Console.WriteLine(chronometer.Lap());
                        break;
                    case "laps":
                        var counter = 0;
                        if (chronometer.Laps.Count>0)
                        {
                            Console.WriteLine("Laps:");
                            foreach (var lap in chronometer.Laps)
                            {
                                Console.WriteLine($"{counter++}. " + lap);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Laps: no laps");
                        }
                        break;
                    case "time":
                        Console.WriteLine(chronometer.GetTime);
                        break;
                    case "reset":
                        chronometer.Reset();
                        break;
                }

                input = Console.ReadLine().ToLower();
            }

            if (input=="exit")
            {
                chronometer.Stop();
            }
        }
    }
}
