using System;

namespace AreaCalculator.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputProcessor = new InputTextProcessor(Console.In, Console.Out);

            inputProcessor.ProcessInput();
        }
    }
}
