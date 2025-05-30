using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Визуализатор_сортировки
{
    internal class Algorithm_comb : ISort
    {

        public List<(int, int, double, double)> Sort(double[] Numbers)
        {
            List<(int, int, double, double)> To_Return = new List<(int, int, double, double)>();
            double[] Numbers_ = new double[Numbers.Length];
            for (int i = 0; i < Numbers_.Length; i++)
            {
                Numbers_[i] = Numbers[i];
            }

            double gap = Numbers_.Length;
            bool swapped = true;
            double temp;
            while (gap > 1 || swapped)
            {
                gap /= 1.247;
                if (gap < 1)
                {
                    gap = 1;
                }
                int i = 0;
                swapped = false;
                while (i + gap < Numbers_.Length)
                {
                    int j = i + (int)gap;
                    if (Numbers_[i] > Numbers_[j])
                    {
                        temp = Numbers_[i];
                        Numbers_[i] = Numbers_[j];
                        Numbers_[j] = temp;
                        var To_Write = (i,j, Numbers_[i], Numbers_[j]);
                        To_Return.Add(To_Write);
                        swapped = true;
                    }
                    i++;
                }
            }


            return To_Return;
        }
    }
}
