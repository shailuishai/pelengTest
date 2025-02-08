using System;

public class Sin : IOperation
{
    public string Symbol => "sin";
    public int Priority => 3;
    public double Execute(double left, double right) => Math.Sin(right);
}

public class Cos : IOperation
{
    public string Symbol => "cos";
    public int Priority => 3;
    public double Execute(double left, double right) => Math.Cos(right);
}

public class Tan : IOperation
{
    public string Symbol => "tan";
    public int Priority => 3;
    public double Execute(double left, double right) => Math.Tan(right);
} 