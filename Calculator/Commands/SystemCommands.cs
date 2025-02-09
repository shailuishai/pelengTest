using System;

public class HelpCommand : ISystemCommand
{
    private readonly OperationProvider _operationProvider;

    public HelpCommand(OperationProvider operationProvider)
    {
        _operationProvider = operationProvider;
    }

    public string Name => "help";
    public string Description => "Показывает список доступных команд и операций";

    public void Execute()
    {
        Console.WriteLine("Доступные команды: \n");
        Console.WriteLine("  h, help    Показывает список доступных команд и операций");
        Console.WriteLine("  e, exit    Выход из программы \n");
        Console.WriteLine("Доступные операции: \n");
        _operationProvider.ShowHelp();
    }
}

public class ExitCommand : ISystemCommand
{
    public string Name => "exit";
    public string Description => "Выход из программы";

    public void Execute()
    {
        Console.WriteLine("Программа завершена.");
        Environment.Exit(0);
    }
} 