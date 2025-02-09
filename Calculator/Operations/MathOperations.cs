using System;

public class Sin : IUnaryOperation
{
    private readonly Func<double, double> _operation;
    
    public Sin()
    {
        _operation = Math.Sin;
    }
    
    public string Description => "Вычисляет синус угла (в радианах)";
    public string Usage => "sin 1.57";
    public string Parameters => "(args: angle double)";
    public double Call(params double[] args)
    {
        if (args.Length < 1)
            throw new InsufficientArgumentsException("sin");
        return _operation(args[0]);
    }
}

public class Cos : IUnaryOperation
{
    private readonly Func<double, double> _operation;
    
    public Cos()
    {
        _operation = Math.Cos;
    }
    
    public string Description => "Вычисляет косинус угла (в радианах)";
    public string Usage => "cos 3.14";
    public string Parameters => "(args: angle double)";
    public double Call(params double[] args)
    {
        if (args.Length < 1)
            throw new InsufficientArgumentsException("cos");
        return _operation(args[0]);
    }
}

public class Tan : IUnaryOperation
{
    private readonly Func<double, double> _operation;
    
    public Tan()
    {
        _operation = Math.Tan;
    }
    
    public string Description => "Вычисляет тангенс угла (в радианах)";
    public string Usage => "tan 0.785";
    public string Parameters => "(args: angle double)";
    public double Call(params double[] args)
    {
        if (args.Length < 1)
            throw new InsufficientArgumentsException("tan");
        return _operation(args[0]);
    }
}

public class Cotangent : IUnaryOperation
{
    private readonly Func<double, double> _operation;
    
    public Cotangent()
    {
        _operation = x => {
            double tanValue = Math.Tan(x);
            if (tanValue == 0)
                throw new CalculatorException("Котангенс не определен для углов, кратных π/2");
            return 1.0 / tanValue;
        };
    }
    
    public string Description => "Вычисляет котангенс угла (в радианах)";
    public string Usage => "cot 0.785";
    public string Parameters => "(args: angle double)";
    public double Call(params double[] args)
    {
        if (args.Length < 1)
            throw new InsufficientArgumentsException("cot");
        return _operation(args[0]);
    }
}

public class SquareRoot : IUnaryOperation
{
    private readonly Func<double, double> _operation;
    
    public SquareRoot()
    {
        _operation = x => {
            if (x < 0)
                throw new CalculatorException("Квадратный корень отрицательного числа не определен в действительных числах");
            return Math.Sqrt(x);
        };
    }
    
    public string Description => "Вычисляет квадратный корень числа";
    public string Usage => "sqrt 16";
    public string Parameters => "(args: number double)";
    public double Call(params double[] args)
    {
        if (args.Length < 1)
            throw new InsufficientArgumentsException("sqrt");
        return _operation(args[0]);
    }
}

public class Logarithm : IOperation
{
    private readonly Func<double, double, double> _operation;
    
    public Logarithm()
    {
        _operation = (x, b) => {
            if (x <= 0)
                throw new CalculatorException("Логарифм определен только для положительных чисел");
            if (b <= 0 || b == 1)
                throw new CalculatorException("Основание логарифма должно быть положительным числом, не равным 1");
            return Math.Log(x, b);
        };
    }
    
    public string Description => "Вычисляет логарифм числа по указанному основанию";
    public string Usage => "log 8 2";
    public string Parameters => "(args: number double, base double)";
    public double Call(params double[] args)
    {
        if (args.Length < 2)
            throw new InsufficientArgumentsException("log");
        return _operation(args[0], args[1]);
    }
}

public class NaturalLogarithm : IUnaryOperation
{
    private readonly Func<double, double> _operation;
    
    public NaturalLogarithm()
    {
        _operation = x => {
            if (x <= 0)
                throw new CalculatorException("Натуральный логарифм определен только для положительных чисел");
            return Math.Log(x);
        };
    }
    
    public string Description => "Вычисляет натуральный логарифм числа";
    public string Usage => "ln 2.718";
    public string Parameters => "(args: number double)";
    public double Call(params double[] args)
    {
        if (args.Length < 1)
            throw new InsufficientArgumentsException("ln");
        return _operation(args[0]);
    }
}

public class Absolute : IUnaryOperation
{
    private readonly Func<double, double> _operation;
    
    public Absolute()
    {
        _operation = Math.Abs;
    }
    
    public string Description => "Вычисляет модуль числа";
    public string Usage => "abs -5";
    public string Parameters => "(args: number double)";
    public double Call(params double[] args)
    {
        if (args.Length < 1)
            throw new InsufficientArgumentsException("abs");
        return _operation(args[0]);
    }
} 