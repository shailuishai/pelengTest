using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Calculator.Core.Exceptions;

namespace Calculator.Core.Services
{
    public class InputParser
    {
        private readonly OperationProvider _operationProvider;

        public InputParser(OperationProvider operationProvider)
        {
            _operationProvider = operationProvider;
        }

        public List<string> ParseToRPN(string input)
        {
            var tokens = Tokenize(input);
            var outputQueue = new List<string>();
            var operatorStack = new Stack<string>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out _))
                {
                    outputQueue.Add(token);
                }
                else if (IsOperator(token))
                {
                    while (operatorStack.Count > 0 && 
                           IsOperator(operatorStack.Peek()) && 
                           GetPrecedence(operatorStack.Peek()) >= GetPrecedence(token))
                    {
                        outputQueue.Add(operatorStack.Pop());
                    }
                    operatorStack.Push(token);
                }
                else if (token == "(")
                {
                    operatorStack.Push(token);
                }
                else if (token == ")")
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                    {
                        outputQueue.Add(operatorStack.Pop());
                    }

                    if (operatorStack.Count == 0 || operatorStack.Peek() != "(")
                    {
                        throw new ParsingException("Несоответствие скобок");
                    }

                    operatorStack.Pop();
                }
                else
                {
                    try
                    {
                        _operationProvider.GetOperation(token);
                        outputQueue.Add(token);
                    }
                    catch
                    {
                        throw new ParsingException($"Неизвестный токен: {token}");
                    }
                }
            }

            while (operatorStack.Count > 0)
            {
                if (operatorStack.Peek() == "(" || operatorStack.Peek() == ")")
                {
                    throw new ParsingException("Несоответствие скобок");
                }
                outputQueue.Add(operatorStack.Pop());
            }

            return outputQueue;
        }

        private List<string> Tokenize(string input)
        {
            var tokens = new List<string>();
            var pattern = @"(\d+\.\d+|\d+|[+\-*/()%^]|[a-zA-Z]+)";
            var matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                tokens.Add(match.Value);
            }

            return tokens;
        }

        private bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "^" || token == "%";
        }

        private int GetPrecedence(string op)
        {
            switch (op)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                case "%":
                    return 2;
                case "^":
                    return 3;
                default:
                    return 0;
            }
        }

        public (string operation, double[] arguments) Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new CalculatorException("Введите команду");

            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            if (parts.Length == 0)
                throw new CalculatorException("Неверный формат ввода");

            var operation = parts[0].ToLower().Trim();
            
            if (operation == "h" || operation == "help")
            {
                return (operation, Array.Empty<double>());
            }
            
            if (operation == "e" || operation == "exit")
            {
                return (operation, Array.Empty<double>());
            }

            var arguments = new List<double>();
            for (int i = 1; i < parts.Length; i++)
            {
                if (double.TryParse(parts[i], out double value))
                {
                    arguments.Add(value);
                }
                else
                {
                    throw new CalculatorException($"Неверный формат числа: {parts[i]}");
                }
            }

            return (operation, arguments.ToArray());
        }
    }
} 
