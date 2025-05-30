using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Визуализатор_сортировки
{
    internal class Algorithm_insert //: ISort
    {/*
        double[] Numbers;

        public Algorithm_insert(int lenght)
        {
            Numbers = (lenght > 0) ? new double[lenght] : new double[100];
        }

        public Algorithm_insert()
        {
            Numbers = new double[100];
        }*/

        public void Sort(double[] Array)
        {
            int temp;
            double Current;
            for (int i = 0; i < Array.Length; i++)
            {
                temp = i;
                Current= Array[i];
                while (temp > 0 && Current < Array[temp - 1])
                {
                    Array[temp] = Array[temp - 1];
                    temp--;
                }
            }
        }
    }
}
