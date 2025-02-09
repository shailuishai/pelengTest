using System;

public class Sin : IUnaryOperation
{
    private readonly Func<double, double> _operation;
    
    public Sin()
    {
        _operation = Math.Sin;
    }
    
    public string Description => "Calculates the sine of an angle (in radians)";
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
    
    public string Description => "Calculates the cosine of an angle (in radians)";
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
    
    public string Description => "Calculates the tangent of an angle (in radians)";
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
                throw new CalculatorException("Cotangent is not defined for angles that are multiples of π/2");
            return 1.0 / tanValue;
        };
    }
    
    public string Description => "Calculates the cotangent of an angle (in radians)";
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
                throw new CalculatorException("Square root of a negative number is not defined in real numbers");
            return Math.Sqrt(x);
        };
    }
    
    public string Description => "Calculates the square root of a number";
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
                throw new CalculatorException("Logarithm is only defined for positive numbers");
            if (b <= 0 || b == 1)
                throw new CalculatorException("Logarithm base must be a positive number not equal to 1");
            return Math.Log(x, b);
        };
    }
    
    public string Description => "Calculates the logarithm of a number with specified base";
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
                throw new CalculatorException("Natural logarithm is only defined for positive numbers");
            return Math.Log(x);
        };
    }
    
    public string Description => "Calculates the natural logarithm of a number";
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
    
    public string Description => "Calculates the absolute value of a number";
    public string Usage => "abs -5";
    public string Parameters => "(args: number double)";
    public double Call(params double[] args)
    {
        if (args.Length < 1)
            throw new InsufficientArgumentsException("abs");
        return _operation(args[0]);
    }
} 