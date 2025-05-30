using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Визуализатор_сортировки
{
    internal class Algorithm_choose : ISort
    {
        public List<(int, int, double, double)> Sort(double[] Numbers)
        {
            double temp;
            int Min;
            List<(int, int, double, double)> To_Return = new List<(int, int, double, double)>();
            double[] Numbers_ = new double[Numbers.Length];
            for (int i = 0; i < Numbers_.Length; i++)
            {
                Numbers_[i] = Numbers[i];
            }


            for (int i = 0; i < Numbers_.Length; i++)
            {
                Min = i;
                for (int j = i + 1; j < Numbers_.Length; j++)
                {
                    if (Numbers_[j] < Numbers_[Min])
                        Min = j;
                }
                temp = Numbers_[Min];
                Numbers_[Min] = Numbers_[i];
                Numbers_[i] = temp;
                var To_Write = (Min, i, Numbers_[Min], Numbers_[i]);
                To_Return.Add(To_Write);
            }
            return To_Return;
        }
    }
}
