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
            
            var helpCommand = new HelpCommand(operationProvider);
            var exitCommand = new ExitCommand();
            
            Console.WriteLine("Калькулятор запущен. Введите 'help' для просмотра доступных команд.");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Calculator > ");
                var input = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Ошибка: Пустой ввод");
                    continue;
                }

                try
                {
                    if (input == helpCommand.Name || input == "h")
                    {
                        helpCommand.Execute();
                        continue;
                    }

                    if (input == exitCommand.Name || input == "e")
                    {
                        exitCommand.Execute();
                    }

                    var operation = operationProvider.GetOperation(input);
                    Console.WriteLine($"Параметры: {operation.Parameters}");
                    Console.Write("args: ");
                    var arguments = Console.ReadLine();

                    if (string.IsNullOrEmpty(arguments))
                    {
                        Console.WriteLine("Ошибка: Пустой ввод");
                        continue;
                    }

                    var result = calculator.Calculate($"{input} {arguments}");
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
