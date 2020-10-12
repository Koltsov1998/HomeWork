using System;

namespace AreaCalculator.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputProcessor = new InputProcessor(Console.In, Console.Out);

            inputProcessor.ProcessInpit();
        }
    }
}
