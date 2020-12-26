using MortalEngines.Core.Contracts;
using MortalEngines.IO;
using MortalEngines.IO.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace MortalEngines.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            MachinesManager machinesManager = new MachinesManager();

            IReader reader = new ConsoleReader();
            ICommand[] commands = reader.ReadCommands().ToArray();
            IWriter writer = new ConsoleWriter();


            
            MethodInfo[] methodsInManager = machinesManager.GetType().GetMethods();

            foreach (var command in commands)
            {
                try
                {
                    string result = string.Empty;
                    

                    switch (command.Name)
                    {
                        case "ManufactureTank":
                            MethodInfo method1 = methodsInManager.FirstOrDefault(m => m.Name == command.Name);
                            if (method1 == null)
                            {
                                continue;
                            }
                            object[] arrTank = new object[] { command.Parameters[0], double.Parse(command.Parameters[1]), double.Parse(command.Parameters[2]) };
                            result = (string)method1.Invoke(machinesManager, arrTank);
                            break;

                        case "ManufactureFighter":
                            MethodInfo method2 = methodsInManager.FirstOrDefault(m => m.Name == command.Name);
                            if (method2 == null)
                            {
                                continue;
                            }
                            object[] arrFighter = new object[] { command.Parameters[0], double.Parse(command.Parameters[1]), double.Parse(command.Parameters[2]) };
                            result = (string)method2.Invoke(machinesManager, arrFighter);
                            break;

                        case "HirePilot":
                        case "PilotReport":
                        case "MachineReport":
                            MethodInfo method3 = methodsInManager.FirstOrDefault(m => m.Name == command.Name);
                            if (method3 == null)
                            {
                                continue;
                            }
                            result = (string)method3.Invoke(machinesManager, command.Parameters);
                            break;

                        case "AggressiveMode":
                            MethodInfo method4 = methodsInManager.FirstOrDefault(m => m.Name == "ToggleFighterAggressiveMode");
                            if (method4 == null)
                            {
                                continue;
                            }
                            result = (string)method4.Invoke(machinesManager, command.Parameters);
                            break;
                        case "DefenseMode":
                            MethodInfo method6 = methodsInManager.FirstOrDefault(m => m.Name == "ToggleTankDefenseMode");
                            if (method6 == null)
                            {
                                continue;
                            }
                            result = (string)method6.Invoke(machinesManager,command.Parameters);
                            break;

                        case "Engage":
                            string nameComm = command.Name + "Machine";
                            MethodInfo method7 = methodsInManager.FirstOrDefault(m => m.Name == nameComm);
                            if (method7 == null)
                            {
                                continue;
                            }
                            result = (string)method7.Invoke(machinesManager, command.Parameters);
                            break;

                        case "Attack":
                            string nameComm2 = command.Name + "Machines";
                            MethodInfo method5 = methodsInManager.FirstOrDefault(m => m.Name == nameComm2);
                            if (method5 == null)
                            {
                                continue;
                            }
                            result = (string)method5.Invoke(machinesManager, command.Parameters);
                            break;
                    }

                    writer.Write(result);

                }
                catch (Exception ex)
                {
                    writer.Write("Error:" + ex.Message);
                }
            }
        }
    }
}
