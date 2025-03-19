using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Core.Interfaces;
using Calculator.Core.Operations;
using Calculator.Core.Exceptions;

namespace Calculator.Core.Services
{
    public class OperationProvider
    {
        private readonly Dictionary<string, IOperation> _operations = new Dictionary<string, IOperation>();
        private readonly Dictionary<string, string> _aliases = new Dictionary<string, string>();

        public OperationProvider()
        {
            RegisterBasicOperations();
            RegisterMathOperations();
        }

        private void RegisterBasicOperations()
        {
            RegisterOperation(new AddOperation(), new[] { "+", "add", "plus" });
            RegisterOperation(new SubtractOperation(), new[] { "-", "sub", "subtract", "minus" });
            RegisterOperation(new MultiplyOperation(), new[] { "*", "mul", "multiply" });
            RegisterOperation(new DivideOperation(), new[] { "/", "div", "divide" });
            RegisterOperation(new PowerOperation(), new[] { "^", "pow", "power" });
            RegisterOperation(new ModuloOperation(), new[] { "%", "mod", "modulo" });
        }

        private void RegisterMathOperations()
        {
            RegisterOperation(new SinOperation(), new[] { "sin", "sine" });
            RegisterOperation(new CosOperation(), new[] { "cos", "cosine" });
            RegisterOperation(new TanOperation(), new[] { "tan", "tangent" });
            RegisterOperation(new LogOperation(), new[] { "log", "logarithm" });
            RegisterOperation(new SqrtOperation(), new[] { "sqrt", "root" });
            RegisterOperation(new AbsOperation(), new[] { "abs", "absolute" });
            RegisterOperation(new MaxOperation(), new[] { "max", "maximum" });
            RegisterOperation(new MinOperation(), new[] { "min", "minimum" });
        }

        private void RegisterOperation(IOperation operation, string[] aliases)
        {
            var name = operation.GetType().Name.ToLower().Replace("operation", "");
            _operations[name] = operation;
            
            foreach (var alias in aliases)
            {
                _aliases[alias.ToLower()] = name;
            }
        }

        public IOperation GetOperation(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new InvalidOperationException("Operation name cannot be empty");

            input = input.ToLower().Trim();
            
            if (_operations.TryGetValue(input, out var directOperation))
                return directOperation;
                
            if (_aliases.TryGetValue(input, out var operationName) && 
                _operations.TryGetValue(operationName, out var aliasedOperation))
                return aliasedOperation;

            throw new OperationNotFoundException(input);
        }
        
        public IEnumerable<IOperation> GetOperations()
        {
            return _operations.Values;
        }
        
        public Dictionary<string, string[]> GetOperationAliases()
        {
            var result = new Dictionary<string, string[]>();
            
            foreach (var operation in _operations.Keys)
            {
                var aliases = _aliases
                    .Where(a => a.Value == operation)
                    .Select(a => a.Key)
                    .ToArray();
                    
                result[operation] = aliases;
            }
            
            return result;
        }
    }
} 