using System;

namespace MyProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var operationProvider = new OperationProvider();
            var inputParser = new InputParser(operationProvider);
            var calculator = new Calculator(operationProvider, inputParser);

            while (true)
            {
                Console.WriteLine("Введите выражение (или 'exit' для выхода):");
                var input = Console.ReadLine();

                if (input?.ToLower() == "exit")
                    break;

                try
                {
                    var result = calculator.Calculate(input);
                    Console.WriteLine($"Результат: {result}");
                }
                catch (CalculatorException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
                }
            }
        }
    }
}
