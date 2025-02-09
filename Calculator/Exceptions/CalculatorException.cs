using System;

public class CalculatorException : Exception
{
    public CalculatorException(string message) : base(message)
    {
    }
}

public class InsufficientArgumentsException : CalculatorException
{
    public InsufficientArgumentsException(string operation) 
        : base($"Insufficient arguments for operation '{operation}'")
    {
    }
}

public class FunctionNotFoundException : CalculatorException 
{
    public FunctionNotFoundException(string function)
        : base($"Function '{function}' not found")
    {
    }
}