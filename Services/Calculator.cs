using System.Collections.Generic;

public class Calculator
{
    private readonly OperationProvider _operationProvider;
    private readonly InputParser _inputParser;

    public Calculator(OperationProvider operationProvider, InputParser inputParser)
    {
        _operationProvider = operationProvider;
        _inputParser = inputParser;
    }

    public double Calculate(string input)
    {
        var rpn = _inputParser.ParseToRPN(input);
        var stack = new Stack<double>();

        foreach (var token in rpn)
        {
            if (double.TryParse(token, out double number))
            {
                stack.Push(number);
                continue;
            }

            var operation = _operationProvider.GetOperation(token);
            var right = stack.Pop();
            var left = stack.Count > 0 ? stack.Pop() : 0;
            
            stack.Push(operation.Execute(left, right));
        }

        return stack.Pop();
    }
} 