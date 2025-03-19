namespace Calculator.Core.Interfaces
{
    public interface ISystemCommand
    {
        string Name { get; }
        void Execute();
    }
} 