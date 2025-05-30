using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Визуализатор_сортировки
{
    public class Algorithm_bubble : ISort
    {

        public List<(int, int, double, double)> Sort(double[] Numbers)
        {
            List<(int, int, double, double)> To_Return = new List<(int, int, double, double)>();
            double temp;
            double[] Numbers_ = new double[Numbers.Length];
            for (int i = 0; i < Numbers_.Length; i++)
            {
                Numbers_[i] = Numbers[i];
            }


            for (int i = 0; i < Numbers_.Length + 1; i++)
            {
                for (int j = i + 1; j < Numbers_.Length; j++)
                {
                    if (Numbers_[i] > Numbers_[j])
                    {
                        temp = Numbers_[i];
                        Numbers_[i] = Numbers_[j];
                        Numbers_[j] = temp;
                        var To_Write = (i,j, Numbers_[i], Numbers_[j]);
                        To_Return.Add(To_Write);
                    }
                }
            }
            return To_Return;
        }
    }
}
