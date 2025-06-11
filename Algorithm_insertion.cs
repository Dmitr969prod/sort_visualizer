using System;
using System.Collections.Generic;

namespace Визуализатор_сортировки
{

    internal class Algorithm_insertion : ISort
    {
        public List<(int, int, double, double)> Sort(double[] numbers)
        {
            var actions = new List<(int, int, double, double)>();
            double[] arr = (double[])numbers.Clone();          // работаем с копией

            for (int i = 1; i < arr.Length; i++)
            {
                int j = i;
                while (j > 0)
                {
                    /* фиксируем сравнение соседних элементов */
                    if (arr[j - 1] > arr[j])
                    {
                        // обмен нужен
                        double tmp = arr[j - 1];
                        arr[j - 1] = arr[j];
                        arr[j] = tmp;

                        actions.Add((j - 1,           // левый индекс
                                     j,               // правый индекс
                                     arr[j - 1],      // новое значение слева
                                     arr[j]));        // новое значение справа
                    }
                    else
                    {
                        // обмен не нужен, но сравнение было
                        actions.Add((j - 1,
                                     j,
                                     arr[j - 1],      // остаются прежние
                                     arr[j]));
                        break;                         // текущий ключ встал на место
                    }

                    j--;                               // двигаемся левее
                }
            }

            return actions;
        }
    }
}
