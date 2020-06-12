using CeApp.DI;
using CommandDotNet;

namespace CeApp.CUI
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            OnStart();
            
            return new AppRunner<Service>().Run(args);
        }

        private static void OnStart()
        {
            DiConfig.RegisterTypesForCui();
        }
    }
}
