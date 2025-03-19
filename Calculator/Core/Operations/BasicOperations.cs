using System;
using Calculator.Core.Interfaces;

namespace Calculator.Core.Operations
{
    public class AddOperation : IOperation
    {
        public string Description => "Сложение двух чисел";
        public string Usage => "add 2 3 = 5";
        public string Parameters => "<число1> <число2>";

        public double Call(params double[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Требуется два аргумента для операции сложения");
            
            return args[0] + args[1];
        }
    }

    public class SubtractOperation : IOperation
    {
        public string Description => "Вычитание второго числа из первого";
        public string Usage => "subtract 5 3 = 2";
        public string Parameters => "<число1> <число2>";

        public double Call(params double[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Требуется два аргумента для операции вычитания");
            
            return args[0] - args[1];
        }
    }

    public class MultiplyOperation : IOperation
    {
        public string Description => "Умножение двух чисел";
        public string Usage => "multiply 2 3 = 6";
        public string Parameters => "<число1> <число2>";

        public double Call(params double[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Требуется два аргумента для операции умножения");
            
            return args[0] * args[1];
        }
    }

    public class DivideOperation : IOperation
    {
        public string Description => "Деление первого числа на второе";
        public string Usage => "divide 6 3 = 2";
        public string Parameters => "<число1> <число2>";

        public double Call(params double[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Требуется два аргумента для операции деления");
            
            if (args[1] == 0)
                throw new DivideByZeroException("Деление на ноль невозможно");
            
            return args[0] / args[1];
        }
    }

    public class PowerOperation : IOperation
    {
        public string Description => "Возведение первого числа в степень второго";
        public string Usage => "power 2 3 = 8";
        public string Parameters => "<основание> <показатель>";

        public double Call(params double[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Требуется два аргумента для операции возведения в степень");
            
            return Math.Pow(args[0], args[1]);
        }
    }

    public class ModuloOperation : IOperation
    {
        public string Description => "Остаток от деления первого числа на второе";
        public string Usage => "modulo 7 3 = 1";
        public string Parameters => "<число1> <число2>";

        public double Call(params double[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Требуется два аргумента для операции взятия остатка");
            
            if (args[1] == 0)
                throw new DivideByZeroException("Деление на ноль невозможно");
            
            return args[0] % args[1];
        }
    }
}
