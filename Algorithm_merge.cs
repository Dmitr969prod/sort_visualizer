using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Визуализатор_сортировки
{
    /// <summary>
    /// Алгоритм «сортировка слиянием».
    /// </summary>
    internal class Algorithm_merge : ISort
    {
        public List<(int, int, double, double)> Sort(double[] Numbers)
        {
            // Лог действий для покадровой визуализации.
            var actions = new List<(int, int, double, double)>();

            // Работаем с копией, чтобы не трогать оригинальный массив.
            double[] arr = (double[])Numbers.Clone();
            double[] temp = new double[arr.Length];

            MergeSort(arr, temp, 0, arr.Length - 1, actions);

            return actions;
        }

        /* ---------------------  private helpers  --------------------- */

        private void MergeSort(double[] arr,
                               double[] temp,
                               int left,
                               int right,
                               List<(int, int, double, double)> actions)
        {
            if (left >= right) return;

            int mid = (left + right) / 2;

            MergeSort(arr, temp, left, mid, actions);
            MergeSort(arr, temp, mid + 1, right, actions);

            Merge(arr, temp, left, mid, right, actions);
        }

        /// <summary>
        /// Сливает отрезки [left..mid] и [mid+1..right].
        /// </summary>
        private void Merge(double[] arr,
                           double[] temp,
                           int left,
                           int mid,
                           int right,
                           List<(int, int, double, double)> actions)
        {
            int i = left;      // текущий индекс левой половины
            int j = mid + 1;   // текущий индекс правой половины
            int k = left;      // позиция в temp

            // Формируем temp упорядоченно
            while (i <= mid && j <= right)
            {
                if (arr[i] <= arr[j])
                    temp[k++] = arr[i++];
                else
                    temp[k++] = arr[j++];
            }

            while (i <= mid) temp[k++] = arr[i++];
            while (j <= right) temp[k++] = arr[j++];

            // Копируем temp обратно и логируем только реально изменившиеся ячейки
            for (k = left; k <= right; k++)
            {
                if (arr[k] != temp[k])
                {
                    arr[k] = temp[k];
                    actions.Add((k, k, arr[k], arr[k]));  // idx1 == idx2
                }
            }
        }
    }
}