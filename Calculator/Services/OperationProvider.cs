using System.Collections.Generic;
using System.Linq;

public class OperationProvider
{
    private readonly Dictionary<string[], IOperation> _operations;

    public OperationProvider()
    {
        _operations = new Dictionary<string[], IOperation>
        {
            { new[] { "+", "add", "plus" }, new Addition() },
            { new[] { "-", "sub", "subtract", "minus" }, new Subtraction() },
            { new[] { "*", "mul", "multiply" }, new Multiplication() },
            { new[] { "/", "div", "divide" }, new Division() },
            { new[] { "sin", "sine" }, new Sin() },
            { new[] { "cos", "cosine" }, new Cos() },
            { new[] { "tan", "tangent" }, new Tan() },
            { new[] { "%", "mod", "modulo" }, new Modulo() },
            { new[] { "pow", "power" }, new Power() },
            { new[] { "sqr", "square" }, new Square() },
            { new[] { "fact", "factorial" }, new Factorial() },
            { new[] { "cot", "cotangent" }, new Cotangent() },
            { new[] { "sqrt", "root" }, new SquareRoot() },
            { new[] { "log", "logarithm" }, new Logarithm() },
            { new[] { "ln" }, new NaturalLogarithm() },
            { new[] { "abs", "absolute" }, new Absolute() }
        };
    }


    public IOperation GetOperation(string input)
    {
        input = input.Trim().ToLower();
        
        foreach (var operation in _operations)
        {
            if (operation.Key.Contains(input))
            {
                return operation.Value;
            }
        }

        throw new CalculatorException("Неизвестная операция. Используйте 'help' для просмотра доступных команд.");
    }

    public void ShowHelp()
    {
        foreach (var operation in _operations)
        {
            var aliases = string.Join(", ", operation.Key);
            var opInstance = operation.Value;
            Console.WriteLine($"  {aliases} {opInstance.Parameters}");
            Console.WriteLine($"    Описание: {opInstance.Description}");
            Console.WriteLine($"    Пример: {opInstance.Usage}");
            Console.WriteLine();
        }
    }
} 