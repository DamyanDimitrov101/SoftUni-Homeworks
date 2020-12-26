using System;

namespace MXGP
{
    using Models.Motorcycles;
    using MXGP.Core;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            //TODO Add IEngine
            Engine engine = new Engine();
            engine.Run();

         Motorcycle varche = new PowerMotorcycle("12214235", 75);
         Console.WriteLine(varche.HorsePower);
        }
    }
}
