using System;
using System.Collections.Generic;

namespace Визуализатор_сортировки
{

    internal class Algorithm_bubble : ISort
    {
        public List<(int, int, double, double)> Sort(double[] numbers)
        {
            var steps = new List<(int, int, double, double)>();
            double[] arr = (double[])numbers.Clone();   

            int n = arr.Length;
            for (int pass = 0; pass < n - 1; pass++)
            {
                for (int j = 0; j < n - pass - 1; j++)
                {
                    
                    if (arr[j] > arr[j + 1])                  
                    {
                        double tmp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tmp;

                        steps.Add((j, j + 1, arr[j], arr[j + 1]));   
                    }
                    else                                       
                    {
                        steps.Add((j, j + 1, arr[j], arr[j + 1]));   
                    }
                }
            }

            return steps;
        }
    }
}
