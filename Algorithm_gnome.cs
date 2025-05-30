using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Визуализатор_сортировки
{
    internal class Algorithm_gnome : ISort
    {
        public List<(int, int, double, double)> Sort(double[] Numbers)
        {
            int index = 0;
            List<(int, int, double, double)> To_Return = new List<(int, int, double, double)>();
            double[] Numbers_ = new double[Numbers.Length];
            for (int i = 0; i < Numbers_.Length; i++)
            {
                Numbers_[i] = Numbers[i];
            }
            while (index < Numbers_.Length)
            {
                if (index == 0) 
                {
                    index++;
                }

                if (Numbers_[index] >= Numbers_[index - 1])
                {
                    index++;
                }
                else
                {
                    double temp;
                    temp = Numbers_[index];
                    Numbers_[index] = Numbers_[index - 1];
                    Numbers_[index - 1] = temp;
                    var To_Write = (index, index -1, Numbers_[index], Numbers_[index -1]);
                    To_Return.Add(To_Write);
                    index--;
                }
            }
            return To_Return;
        }
    }
}
