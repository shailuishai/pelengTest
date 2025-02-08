using System.Collections.Generic;
using System.Linq;

public class InputParser
{
    private readonly OperationProvider _operationProvider;

    public InputParser(OperationProvider operationProvider)
    {
        _operationProvider = operationProvider;
    }

    public Queue<string> ParseToRPN(string input)
    {
        var output = new Queue<string>();
        var operators = new Stack<string>();
        var tokens = input.Split(' ');

        foreach (var token in tokens)
        {
            if (double.TryParse(token, out _))
            {
                output.Enqueue(token);
                continue;
            }

            if (_operationProvider.IsOperation(token))
            {
                while (operators.Count > 0 && _operationProvider.IsOperation(operators.Peek()))
                {
                    var op1 = _operationProvider.GetOperation(token);
                    var op2 = _operationProvider.GetOperation(operators.Peek());
                    
                    if (op1.Priority <= op2.Priority)
                        output.Enqueue(operators.Pop());
                    else
                        break;
                }
                operators.Push(token);
            }
        }

        while (operators.Count > 0)
            output.Enqueue(operators.Pop());

        return output;
    }
} 