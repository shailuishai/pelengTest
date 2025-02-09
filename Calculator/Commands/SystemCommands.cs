using System;

public class HelpCommand : ISystemCommand
{
    private readonly OperationProvider _operationProvider;

    public HelpCommand(OperationProvider operationProvider)
    {
        _operationProvider = operationProvider;
    }

    public string Name => "help";
    public string Description => "Shows list of available commands and operations";

    public void Execute()
    {
        Console.WriteLine("Available commands: \n");
        Console.WriteLine("  h, help    Shows list of available commands and operations");
        Console.WriteLine("  e, exit    Exit program \n");
        Console.WriteLine("Available operations: \n");
        _operationProvider.ShowHelp();
    }
}

public class ExitCommand : ISystemCommand
{
    public string Name => "exit";
    public string Description => "Exit program";

    public void Execute()
    {
        Console.WriteLine("Program terminated.");
        Environment.Exit(0);
    }
} 