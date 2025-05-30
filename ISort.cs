using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Визуализатор_сортировки
{
    public interface ISort
    {
        //(int, int, double, double) Sort(double[] Array, int i);
        //double[] Sort(double[] Array);

        //List<double[]> Sort(double[] Array);

        List<(int, int, double, double)> Sort(double[] Numbers);

        //(int, int, double, double)[] Sort(double[] Array);

        //void Sort(double[] Numbers);

    }
}
