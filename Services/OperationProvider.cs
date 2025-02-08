using System.Collections.Generic;
using System.Linq;

public class OperationProvider
{
    private readonly Dictionary<string, IOperation> _operations;

    public OperationProvider()
    {
        _operations = new Dictionary<string, IOperation>
        {
            { "+", new Addition() },
            { "-", new Subtraction() },
            { "*", new Multiplication() },
            { "/", new Division() },
            { "sin", new Sin() },
            { "cos", new Cos() },
            { "tan", new Tan() }
        };
    }

    public IOperation GetOperation(string symbol)
    {
        if (!_operations.ContainsKey(symbol))
            throw new CalculatorException($"Неизвестная операция: {symbol}");
        
        return _operations[symbol];
    }

    public bool IsOperation(string symbol) => _operations.ContainsKey(symbol);
} 