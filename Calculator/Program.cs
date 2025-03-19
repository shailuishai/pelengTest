using System;
using Avalonia;
using Calculator.Core.Services;
using Calculator.Core.Commands;
using Calculator.Core.Interfaces;
using Calculator.Core.Exceptions;
using Calculator.UI;

namespace Calculator
{
    class Program
    {

        public static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].ToLower() == "--console")
            {
                RunConsoleMode();
            }
            else
            {
                RunGuiMode(args);
            }
        }

        private static void RunGuiMode(string[] args)
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }


        private static void RunConsoleMode()
        {
            var operationProvider = new OperationProvider();
            var inputParser = new InputParser(operationProvider);
            var calculator = new Core.Services.Calculator(operationProvider, inputParser);
            
            var helpCommand = new HelpCommand(operationProvider);
            var exitCommand = new ExitCommand();
            
            Console.WriteLine("Calculator started. Type 'help' to see available commands.");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Calculator > ");
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Error: Empty input");
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
                        continue;
                    }

                    var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 0)
                    {
                        Console.WriteLine("Error: Empty input");
                        continue;
                    }

                    var operationName = parts[0].ToLower();
                    
                    try
                    {
                        var operation = operationProvider.GetOperation(operationName);
                        Console.WriteLine($"Параметры: {operation.Parameters}");
                        
                        if (parts.Length > 1)
                        {
                            var arguments = new double[parts.Length - 1];
                            bool validArgs = true;
                            
                            for (int i = 1; i < parts.Length; i++)
                            {
                                if (!double.TryParse(parts[i], out arguments[i - 1]))
                                {
                                    Console.WriteLine($"Error: Invalid number format: {parts[i]}");
                                    validArgs = false;
                                    break;
                                }
                            }
                            
                            if (!validArgs)
                                continue;
                                
                            try
                            {
                                var result = operation.Call(arguments);
                                Console.WriteLine($"Result: {result}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.Write("args: ");
                            var argsInput = Console.ReadLine();
                            
                            if (string.IsNullOrEmpty(argsInput))
                            {
                                Console.WriteLine("Error: Empty input");
                                continue;
                            }
                            
                            var argParts = argsInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            var arguments = new double[argParts.Length];
                            bool validArgs = true;
                            
                            for (int i = 0; i < argParts.Length; i++)
                            {
                                if (!double.TryParse(argParts[i], out arguments[i]))
                                {
                                    Console.WriteLine($"Error: Invalid number format: {argParts[i]}");
                                    validArgs = false;
                                    break;
                                }
                            }
                            
                            if (!validArgs)
                                continue;
                                
                            try
                            {
                                var result = operation.Call(arguments);
                                Console.WriteLine($"Result: {result}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                    }
                    catch (OperationNotFoundException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                catch (CalculatorException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
    }
}
