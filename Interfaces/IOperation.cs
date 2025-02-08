public interface IOperation
{
    string Symbol { get; }
    int Priority { get; }
    double Execute(double left, double right);
} 