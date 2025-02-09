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
        : base($"Недостаточно аргументов для операции '{operation}'")
    {
    }
}

public class FunctionNotFoundException : CalculatorException 
{
    public FunctionNotFoundException(string function)
        : base($"Функция '{function}' не найдена")
    {
    }
}