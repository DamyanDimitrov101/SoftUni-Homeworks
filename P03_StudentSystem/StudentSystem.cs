using System;
using System.Collections.Generic;
using System.Text;

namespace P03_StudentSystem
{
    public class StudentSystem
    {
        public StudentSystem()
        {
            Repo = new Dictionary<string, Student>();
        }

        public Dictionary<string, Student> Repo { get; private set; }

        public void ParseCommand()
        {
            string[] args = Console.ReadLine().Split();

            IfCreate(args);
            IfShow(args);
            IfExit(args);
        }

        private void IfExit(string[] args)
        {
            if (args[0] == "Exit")
            {
                Environment.Exit(0);
            }
        }

        private void IfShow(string[] args)
        {
            if (args[0] == "Show")
            {
                var name = args[1];
                if (Repo.ContainsKey(name))
                {
                    var student = Repo[name];
                    
                    Console.WriteLine(student);
                }

            }
        }

        private void IfCreate(string[] args)
        {
            if (args[0] == "Create")
            {

                var name = args[1];
                var age = int.Parse(args[2]);
                var grade = double.Parse(args[3]);
                if (!Repo.ContainsKey(name))
                {
                    var student = new Student(name, age, grade);
                    Repo[name] = student;
                }
            }
        }
    }

}
