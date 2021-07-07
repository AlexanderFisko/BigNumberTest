using System;
using System.Globalization;
using System.Linq;

namespace Interview
{
    /* Задача:
     * Необходимо реализовать класс BigNumber для работы с длинными числами:
     * - конструктор
     * - преобразование в строку
     * - оператор сложения

     * !Нельзя использовать готовые реализации длинных чисел

     * Требования к длинному числу:
     * - целое
     * - положительное
     * - произвольное число разрядов (может быть больше, чем допускает long)
     * Ограничения на строку - параметр конструктора BigNumber:
     * - содержит только цифры
     * - отсутствуют ведущие нули
     * 
     * Пример использования:
     * var a = new BigNumber("175872");
     * var b = new BigNumber("1234567890123456789012345678901234567890");
     * var r = a + b;
     * 
     * Для проверки решения необходимо запустить тесты.
     */

    public class BigNumber
    {
        // лучше хранить в обратном порядке
        private readonly int[] _digits;

        public BigNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{nameof(value)} is empty");
            }

            if (value[0].Equals('0'))
            {
                throw new ArgumentException($"{nameof(value)} contains leading zeros");
            }

            _digits = new int[value.Length];

            for (var i = 0; i < value.Length; i++)
            {
                if (!int.TryParse(value[i].ToString(CultureInfo.InvariantCulture), NumberStyles.None,
                    CultureInfo.InvariantCulture, out var digit))
                {
                    throw new ArgumentException($"{nameof(value)} is invalid");
                }

                _digits[_digits.Length - 1 - i] = digit;
            }
        }

        public override string ToString() => string.Join(string.Empty, _digits.Reverse());

        public static BigNumber operator +(BigNumber a, BigNumber b)
        {
            var maxLen = Math.Max(a._digits.Length, b._digits.Length);

            var minDigits = new int [maxLen + 1];
            var maxDigits = new int [maxLen + 1];

            if (a._digits.Length == maxLen)
            {
                Array.Copy(a._digits, maxDigits, a._digits.Length);
                Array.Copy(b._digits, minDigits, b._digits.Length);
            }
            else
            {
                Array.Copy(a._digits, minDigits, a._digits.Length);
                Array.Copy(b._digits, maxDigits, b._digits.Length);
            }

            const int maxNumber = 9;

            for (var i = 0; i < maxDigits.Length; i++)
            {
                var sum = minDigits[i] + maxDigits[i];

                if (sum <= maxNumber)
                {
                    maxDigits[i] = sum;
                }
                else
                {
                    maxDigits[i] = sum - maxNumber - 1;
                    maxDigits[i + 1] += 1;
                }
            }

            var resultLen = maxDigits[maxDigits.Length - 1] == 0 ? maxDigits.Length - 1 : maxDigits.Length;

            return new BigNumber(string.Join(string.Empty, maxDigits.Take(resultLen).Reverse()));
        }
    }
}