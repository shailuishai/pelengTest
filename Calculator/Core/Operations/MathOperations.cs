using System;
using Calculator.Core.Interfaces;

namespace Calculator.Core.Operations
{
    public class SinOperation : IUnaryOperation
    {
        public string Description => "Синус угла в радианах";
        public string Usage => "sin 1.57 = 1";
        public string Parameters => "<угол в радианах>";

        public double Call(params double[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Требуется один аргумент для операции синуса");
            
            return Math.Sin(args[0]);
        }
    }

    public class CosOperation : IUnaryOperation
    {
        public string Description => "Косинус угла в радианах";
        public string Usage => "cos 0 = 1";
        public string Parameters => "<угол в радианах>";

        public double Call(params double[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Требуется один аргумент для операции косинуса");
            
            return Math.Cos(args[0]);
        }
    }

    public class TanOperation : IUnaryOperation
    {
        public string Description => "Тангенс угла в радианах";
        public string Usage => "tan 0.785 = 1";
        public string Parameters => "<угол в радианах>";

        public double Call(params double[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Требуется один аргумент для операции тангенса");
            
            return Math.Tan(args[0]);
        }
    }

    public class LogOperation : IOperation
    {
        public string Description => "Логарифм числа по указанному основанию";
        public string Usage => "log 100 10 = 2";
        public string Parameters => "<число> <основание>";

        public double Call(params double[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Требуется два аргумента для операции логарифма");
            
            if (args[0] <= 0 || args[1] <= 0 || args[1] == 1)
                throw new ArgumentException("Некорректные аргументы для логарифма");
            
            return Math.Log(args[0], args[1]);
        }
    }

    public class SqrtOperation : IUnaryOperation
    {
        public string Description => "Квадратный корень из числа";
        public string Usage => "sqrt 9 = 3";
        public string Parameters => "<число>";

        public double Call(params double[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Требуется один аргумент для операции квадратного корня");
            
            if (args[0] < 0)
                throw new ArgumentException("Невозможно извлечь квадратный корень из отрицательного числа");
            
            return Math.Sqrt(args[0]);
        }
    }

    public class AbsOperation : IUnaryOperation
    {
        public string Description => "Абсолютное значение числа";
        public string Usage => "abs -5 = 5";
        public string Parameters => "<число>";

        public double Call(params double[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Требуется один аргумент для операции абсолютного значения");
            
            return Math.Abs(args[0]);
        }
    }

    public class MaxOperation : IOperation
    {
        public string Description => "Максимальное из двух чисел";
        public string Usage => "max 5 10 = 10";
        public string Parameters => "<число1> <число2>";

        public double Call(params double[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Требуется два аргумента для операции максимума");
            
            return Math.Max(args[0], args[1]);
        }
    }

    public class MinOperation : IOperation
    {
        public string Description => "Минимальное из двух чисел";
        public string Usage => "min 5 10 = 5";
        public string Parameters => "<число1> <число2>";

        public double Call(params double[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Требуется два аргумента для операции минимума");
            
            return Math.Min(args[0], args[1]);
        }
    }
} 