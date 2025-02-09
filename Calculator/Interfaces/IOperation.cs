public interface IOperation
{
    public string Description { get; }
    public string Usage { get; }
    public string Parameters { get; }
    double Call(params double[] args);
} 