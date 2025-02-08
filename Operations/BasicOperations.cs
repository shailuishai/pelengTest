using System;

public class Addition : IOperation
{
    public string Symbol => "+";
    public int Priority => 1;
    public double Execute(double left, double right) => left + right;
}

public class Subtraction : IOperation
{
    public string Symbol => "-";
    public int Priority => 1;
    public double Execute(double left, double right) => left - right;
}

public class Multiplication : IOperation
{
    public string Symbol => "*";
    public int Priority => 2;
    public double Execute(double left, double right) => left * right;
}

public class Division : IOperation
{
    public string Symbol => "/";
    public int Priority => 2;
    public double Execute(double left, double right)
    {
        if (right == 0)
            throw new CalculatorException("Деление на ноль невозможно");
        return left / right;
    }
} 