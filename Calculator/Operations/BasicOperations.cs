using System;

public class Addition : IOperation
{
    private readonly Func<double, double, double> _operation;
    
    public Addition()
    {
        _operation = (a, b) => a + b;
    }
    
    public string Description => "Складывает два числа";
    public string Usage => "add 2 3";
    public string Parameters => "(args: a double, b double)";
    public double Call(params double[] args)
    {
        if (args.Length < 2)
            throw new InsufficientArgumentsException("+");
        return _operation(args[0], args[1]);
    }
}

public class Subtraction : IOperation
{
    private readonly Func<double, double, double> _operation;
    
    public Subtraction()
    {
        _operation = (a, b) => a - b;
    }
    
    public string Description => "Вычитает два числа";
    public string Usage => "sub 5 3";
    public string Parameters => "(args: a double, b double)";
    public double Call(params double[] args)
    {
        if (args.Length < 2)
            throw new InsufficientArgumentsException("-");
        return _operation(args[0], args[1]);
    }
}

public class Multiplication : IOperation
{
    private readonly Func<double, double, double> _operation;
    
    public Multiplication()
    {
        _operation = (a, b) => a * b;
    }
    
    public string Description => "Умножает два числа";
    public string Usage => "mul 2 3";
    public string Parameters => "(args: a double, b double)";
    public double Call(params double[] args)
    {
        if (args.Length < 2)
            throw new InsufficientArgumentsException("*");
        return _operation(args[0], args[1]);
    }
}

public class Division : IOperation
{
    private readonly Func<double, double, double> _operation;
    
    public Division()
    {
        _operation = (a, b) => {
            if (b == 0)
                throw new CalculatorException("Деление на ноль невозможно");
            return a / b;
        };
    }
    
    public string Description => "Делит два числа";
    public string Usage => "div 6 3";
    public string Parameters => "(args: a double, b double)";
    public double Call(params double[] args)
    {
        if (args.Length < 2)
            throw new InsufficientArgumentsException("/");
        return _operation(args[0], args[1]);
    }
}

public class Modulo : IOperation
{
    private readonly Func<double, double, double> _operation;
    
    public Modulo()
    {
        _operation = (a, b) => {
            if (b == 0)
                throw new CalculatorException("Деление на ноль невозможно");
            return a % b;
        };
    }
    
    public string Description => "Вычисляет остаток от деления двух чисел";
    public string Usage => "mod 7 3";
    public string Parameters => "(args: a double, b double)";
    public double Call(params double[] args)
    {
        if (args.Length < 2)
            throw new InsufficientArgumentsException("%");
        return _operation(args[0], args[1]);
    }
}

public class Power : IOperation
{
    private readonly Func<double, double, double> _operation;
    
    public Power()
    {
        _operation = Math.Pow;
    }
    
    public string Description => "Возводит число в указанную степень";
    public string Usage => "pow 2 3";
    public string Parameters => "(args: base double, exponent double)";
    public double Call(params double[] args)
    {
        if (args.Length < 2)
            throw new InsufficientArgumentsException("pow");
        return _operation(args[0], args[1]);
    }
}

public class Square : IUnaryOperation
{
    private readonly Func<double, double> _operation;
    
    public Square()
    {
        _operation = x => Math.Pow(x, 2);
    }
    
    public string Description => "Возводит число в квадрат";
    public string Usage => "sqr 5";
    public string Parameters => "(args: number double)";
    public double Call(params double[] args)
    {
        if (args.Length < 1)
            throw new InsufficientArgumentsException("sqr");
        return _operation(args[0]);
    }
}

public class Factorial : IUnaryOperation
{
    private readonly Func<double, double> _operation;
    
    public Factorial()
    {
        _operation = x => {
            if (x < 0)
                throw new CalculatorException("Факториал отрицательного числа не определен");
            if (x != Math.Floor(x))
                throw new CalculatorException("Факториал может быть вычислен только для целых чисел");
            if (x > 170)
                throw new CalculatorException("Слишком большое число для вычисления факториала");

            double result = 1;
            for (int i = 2; i <= x; i++)
                result *= i;
            return result;
        };
    }
    
    public string Description => "Вычисляет факториал числа";
    public string Usage => "fact 5";
    public string Parameters => "(args: number double)";
    public double Call(params double[] args)
    {
        if (args.Length < 1)
            throw new InsufficientArgumentsException("fact");
        return _operation(args[0]);
    }
}
