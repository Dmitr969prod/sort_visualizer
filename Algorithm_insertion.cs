using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Визуализатор_сортировки
{
    /// <summary>
    /// Алгоритм сортировки вставками.
    /// </summary>
    internal class Algorithm_insertion : ISort
    {
        /// <summary>
        /// Сортирует копию массива Numbers алгоритмом вставок и
        /// возвращает список обменов (index1, index2, value1, value2)
        /// для пошаговой визуализации.
        /// </summary>
        public List<(int, int, double, double)> Sort(double[] Numbers)
        {
            var actions = new List<(int, int, double, double)>();

            // Работаем с копией, чтобы не изменять исходный массив.
            double[] arr = (double[])Numbers.Clone();

            for (int i = 1; i < arr.Length; i++)
            {
                int j = i;
                // Перемещаем arr[j] влево, пока элементы слева больше его.
                while (j > 0 && arr[j - 1] > arr[j])
                {
                    // Обмен соседних элементов
                    double tmp = arr[j - 1];
                    arr[j - 1] = arr[j];
                    arr[j] = tmp;

                    // Записываем шаг для визуализатора:
                    // индексы обмененных элементов и их новые значения
                    actions.Add((j - 1, j, arr[j - 1], arr[j]));

                    j--; // продолжаем двигаться влево
                }
            }

            return actions;
        }
    }
}

