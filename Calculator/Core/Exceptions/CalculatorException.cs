using System;

namespace Calculator.Core.Exceptions
{
    public class CalculatorException : Exception
    {
        public CalculatorException(string message) : base(message)
        {
        }
    }

    public class OperationNotFoundException : CalculatorException
    {
        public OperationNotFoundException(string operationName)
            : base($"Операция '{operationName}' не найдена")
        {
        }
    }

    public class InsufficientArgumentsException : CalculatorException
    {
        public InsufficientArgumentsException(string operationName)
            : base($"Недостаточно аргументов для операции '{operationName}'")
        {
        }
    }

    public class ParsingException : CalculatorException
    {
        public ParsingException(string message) : base(message)
        {
        }
    }
}