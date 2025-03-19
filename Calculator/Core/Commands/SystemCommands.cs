using System;
using System.Linq;
using Calculator.Core.Interfaces;
using Calculator.Core.Services;

namespace Calculator.Core.Commands
{
    public class HelpCommand : ISystemCommand
    {
        private readonly OperationProvider _operationProvider;

        public HelpCommand(OperationProvider operationProvider)
        {
            _operationProvider = operationProvider;
        }

        public string Name => "help";

        public void Execute()
        {
            Console.WriteLine("Доступные операции:");
            Console.WriteLine();

            var aliases = _operationProvider.GetOperationAliases();
            
            foreach (var operation in _operationProvider.GetOperations().OrderBy(o => o.GetType().Name))
            {
                var name = operation.GetType().Name.ToLower().Replace("operation", "");
                var operationAliases = aliases.ContainsKey(name) ? aliases[name] : Array.Empty<string>();
                var aliasesStr = operationAliases.Length > 0 ? $" (алиасы: {string.Join(", ", operationAliases)})" : "";
                
                Console.WriteLine($"  {name}{aliasesStr}");
                Console.WriteLine($"    Описание: {operation.Description}");
                Console.WriteLine($"    Параметры: {operation.Parameters}");
                Console.WriteLine($"    Пример: {operation.Usage}");
                Console.WriteLine();
            }

            Console.WriteLine("Системные команды:");
            Console.WriteLine("  help (h) - показать справку");
            Console.WriteLine("  exit (e) - выйти из программы");
            Console.WriteLine();
        }
    }

    public class ExitCommand : ISystemCommand
    {
        public string Name => "exit";

        public void Execute()
        {
            Console.WriteLine("Выход из программы...");
            Environment.Exit(0);
        }
    }
} 