using System.Collections.Generic;
using System.Linq;

public class InputParser
{
    private readonly OperationProvider _operationProvider;
    private readonly Dictionary<string, int> _operationPriorities;

    public InputParser(OperationProvider operationProvider)
    {
        _operationProvider = operationProvider;
        _operationPriorities = new Dictionary<string, int>
        {
            {"+", 1}, {"add", 1}, {"plus", 1},
            {"-", 1}, {"sub", 1}, {"subtract", 1}, {"minus", 1},
            {"*", 2}, {"mul", 2}, {"multiply", 2},
            {"/", 2}, {"div", 2}, {"divide", 2},
            {"sin", 3}, {"sine", 3},
            {"cos", 3}, {"cosine", 3},
            {"tan", 3}, {"tangent", 3},
            {"%", 2}, {"mod", 2}, {"modulo", 2},
            {"pow", 3}, {"power", 3},
            {"sqr", 3}, {"square", 3},
            {"fact", 3}, {"factorial", 3},
            {"cot", 3}, {"cotangent", 3},
            {"sqrt", 3}, {"root", 3},
            {"log", 3}, {"logarithm", 3},
            {"ln", 3},
            {"abs", 3}, {"absolute", 3}
        };
    }

    public Queue<string> ParseToRPN(string input)
    {
        var output = new Queue<string>();
        var operators = new Stack<string>();
        var tokens = input.Split(' ');

        var operation = tokens[0];
        operators.Push(operation);

        var numbers = tokens.Skip(1)
                           .Where(t => double.TryParse(t, out _))
                           .Take(_operationProvider.GetOperation(operation) is IUnaryOperation ? 1 : 2);

        foreach (var number in numbers)
        {
            output.Enqueue(number);
        }

        while (operators.Count > 0)
        {
            output.Enqueue(operators.Pop());
        }

        return output;
    }

    public (string operation, decimal[] arguments) Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new CalculatorException("Enter a command");

        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        if (parts.Length == 0)
            throw new CalculatorException("Invalid input format");

        var operation = parts[0].ToLower().Replace("op:", "").Trim();
        
        if (operation == "h")
        {
            return (operation, Array.Empty<decimal>());
        }

        if (parts.Length < 2)
            throw new CalculatorException("Insufficient arguments");

        var arguments = new List<decimal>();
        for (int i = 1; i < parts.Length; i++)
        {
            if (decimal.TryParse(parts[i], out decimal value))
            {
                arguments.Add(value);
            }
            else
            {
                throw new CalculatorException($"Invalid number format: {parts[i]}");
            }
        }

        return (operation, arguments.ToArray());
    }
} 
