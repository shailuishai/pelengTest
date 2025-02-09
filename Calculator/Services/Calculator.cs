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
            
            // Получаем минимальное необходимое количество аргументов
            int requiredArgs = operation is IUnaryOperation ? 1 : 2;
            var args = new double[requiredArgs];
            
            // Берем только первые необходимые аргументы из стека
            for (int i = requiredArgs - 1; i >= 0; i--)
            {
                if (stack.Count == 0)
                    throw new InsufficientArgumentsException(token);
                args[i] = stack.Pop();
            }
            
            stack.Push(operation.Call(args));
        }

        if (stack.Count == 0)
            throw new CalculatorException("Ошибка вычисления: пустой стек");

        var result = stack.Pop();
        
        // Очищаем оставшиеся значения в стеке
        stack.Clear();
        
        return result;
    }
} 