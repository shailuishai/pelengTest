public interface ISystemCommand
{
    string Name { get; }
    string Description { get; }
    void Execute();
} 