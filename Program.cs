// Задача 36: Задайте одномерный массив, заполненный случайными числами. Найдите сумму элементов, стоящих на нечётных позициях.
// [3, 7, 23, 12] -> 19
// [-4, -6, 89, 6] -> 0

using System;
using System.Text;

namespace GBArrayOddNumbers
{
    public class ConsoleApp
    {
        static void Main()
        {
            var array = InitializeRandomIntArray(4, 1, 2);
            var oddSumm = SummOdd(array);
            Console.WriteLine(array.ToArrayString() + " -> " + oddSumm);
        }

        static int SummOdd(IEnumerable<int> input)
        {
            var result = 0;
            foreach (var element in input ?? Enumerable.Empty<int>())
            {
                if (element % 2 != 0) result += element;
            }

            return result;
        }

        static int[] InitializeRandomIntArray (int arraySize, int minDigitsCount, int maxDigitsCount)
        {
            if (arraySize < 0 || maxDigitsCount < 0 || minDigitsCount <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var result = new int[arraySize];
            if (arraySize == 0 || maxDigitsCount == 0)
            {
                return result;
            }

            var rnd = new Random();
            var min = Pow(10, minDigitsCount - 1);
            var max = Pow(10, maxDigitsCount);
            for (int i = 0; i < arraySize; i++)
            {
                result[i] = rnd.Next(min, max) * (rnd.Next(1, 3) == 2 ? -1 : 1);
            }

            return result;
        }

        static int Pow(int @base, int pow)
        {
            var operation = default(Func<int, int, int>);
            switch (pow)
            {
                case > 0:
                operation = new Func<int, int, int>((int a, int b) => a * b);
                break;
                case < 0:
                operation = new Func<int, int, int>((int a, int b) => a / b);
                break;
                case 0:
                return 1;
            }
            
            var cycle = new Cycle(@base, Math.Abs(pow), operation);
            return cycle.Evaluate();
        }
    }

    internal static class ArrayExtension
    {
        internal static string ToArrayString(this int[] array)
        {
            if (array == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sb.Append('[');
            foreach (var element in array)
            {
                sb.Append(element);
                sb.Append(',');
                sb.Append(' ');
            }

            sb.Remove(sb.Length - 2, 2);
            sb.Append(']');
            return sb.ToString();
        }
    }

    internal class Cycle
    {
        private readonly Func<int, int, int> _operation;
        private readonly int _value;
        private readonly int _iterateMax;
        public Cycle(int value, int max, Func<int, int, int> operation)
        {
            _operation = operation;
            _iterateMax = max;
            _value = value;
        }

        public int Evaluate()
        {
            var result = 1;
            for (int i = 0; i < _iterateMax; i++)
            {
                result = _operation(result, _value);
            }

            return result;
        }
    }
}