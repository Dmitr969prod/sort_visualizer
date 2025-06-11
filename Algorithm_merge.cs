using System;
using System.Collections.Generic;

namespace Визуализатор_сортировки
{
    /// <summary>
    /// Сортировка слиянием с подробным логом:
    /// • каждый comparator;
    /// • каждая фактическая перезапись ячейки.
    /// </summary>
    internal class Algorithm_merge : ISort
    {
        public List<(int, int, double, double)> Sort(double[] numbers)
        {
            var actions = new List<(int, int, double, double)>();

            // Работаем с копией, чтобы не трогать исходный массив формы.
            var arr = (double[])numbers.Clone();
            var temp = new double[arr.Length];

            MergeSort(arr, temp, 0, arr.Length - 1, actions);
            return actions;
        }

        /* ---------------- helpers ---------------- */

        private void MergeSort(double[] arr,
                               double[] temp,
                               int left,
                               int right,
                               List<(int, int, double, double)> log)
        {
            if (left >= right) return;

            int mid = (left + right) / 2;
            MergeSort(arr, temp, left, mid, log);
            MergeSort(arr, temp, mid + 1, right, log);
            Merge(arr, temp, left, mid, right, log);
        }

        /// <summary>
        /// Слияние отрезков [left..mid] и [mid+1..right].
        /// </summary>
        private void Merge(double[] arr,
                           double[] temp,
                           int left,
                           int mid,
                           int right,
                           List<(int, int, double, double)> log)
        {
            int i = left;        // индекс в левой половине
            int j = mid + 1;     // индекс в правой половине
            int k = left;        // позиция в temp

            // Формируем temp, попутно логируя КАЖДЫЙ comparator
            while (i <= mid && j <= right)
            {
                // 1) лог сравнения
                log.Add((i, j, arr[i], arr[j]));          // без обмена

                // 2) выбор меньшего
                if (arr[i] <= arr[j])
                    temp[k++] = arr[i++];
                else
                    temp[k++] = arr[j++];
            }

            // Остатки одной из половин
            while (i <= mid) temp[k++] = arr[i++];
            while (j <= right) temp[k++] = arr[j++];

            /* ---------- копирование temp → arr с логом ---------- */
            for (k = left; k <= right; k++)
            {
                if (arr[k] != temp[k])                    // реальное изменение
                {
                    arr[k] = temp[k];
                    log.Add((k, k, arr[k], arr[k]));      // «обновление ячейки»
                }
            }
        }
    }
}
