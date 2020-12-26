using LoggerLib.Appenders;
using LoggerLib.Enums;
using LoggerLib.Interpreters;
using LoggerLib.Layouts;
using LoggerLib.Loggers;
using System;
using System.Collections.Generic;

namespace LoggerLibrary.Core
{
    public class Engine
    {
        private List<IAppender> appenders;
        public void Run()
        {
            this.appenders = new List<IAppender>();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] inputArr = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries);

                LayoutInterpreter layoutInterpreter = new LayoutInterpreter();
                ILayout currentLayout = layoutInterpreter.GetType() as ILayout;

                if (currentLayout==null)
                {
                    continue;
                }

                ILogFile logFile = new LogFile();

                Type currentType = Type.GetType(inputArr[0]);
                IAppender instance = (IAppender)Activator.CreateInstance(currentType, new object[] { currentLayout,logFile});

                if (instance != null)
                {
                    if (inputArr.Length > 2)
                    {
                        ReportLevelInterpreter reportLevelInterpreter = new ReportLevelInterpreter(inputArr[2]?.ToLower());

                        (instance).ReportLevel = reportLevelInterpreter.ReturnReportLevel();
                    }
                }
            }

            string input = Console.ReadLine();
            CommandInterpreter commandInterpreter= new CommandInterpreter(appenders.ToArray());
            while (input!="END")
            {
                commandInterpreter.Evaluate(input);

                input = Console.ReadLine();
            }
        }
    }
}
