using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Визуализатор_сортировки
{
    public class Presenter
    {
        public ISort Algorithm = new Algorithm_bubble();
        private Random r = new Random();

        Chart Data = new Chart();
        private System.Windows.Forms.Label label_count { get; set; } = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label label_speed { get; set; } = new System.Windows.Forms.Label();

        private System.Windows.Forms.Label label_Bubble { get; set; } = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label label_Choose { get; set; } = new System.Windows.Forms.Label();

        private System.Windows.Forms.Label label_Comb { get; set; } = new System.Windows.Forms.Label();

        private System.Windows.Forms.Label label_Gnome { get; set; } = new System.Windows.Forms.Label();
        private System.Windows.Forms.Button Generate { get; set; } = new System.Windows.Forms.Button();
        private RichTextBox RCB { get; set; } = new RichTextBox();
        private System.Windows.Forms.TrackBar Trackbar1 { get; set; } = new System.Windows.Forms.TrackBar();
        private System.Windows.Forms.TrackBar Trackbar2 { get; set; } = new System.Windows.Forms.TrackBar();
        private CheckBox AlBubble = new CheckBox();
        private CheckBox AlChoose = new CheckBox();
        private CheckBox AlGnome = new CheckBox();
        private CheckBox AlComb = new CheckBox();
        private TextBox DescriptionBox = new TextBox();

        private int Index = 0, Iters;

        public TextBox DrawDescriptionBox()
        {
            DescriptionBox.Multiline = true;
            DescriptionBox.ReadOnly = true;
            DescriptionBox.ScrollBars = ScrollBars.Vertical;
            DescriptionBox.Size = new Size(250, 120);
            DescriptionBox.Location = new Point(520, 470); // размещаем ниже остальных
            DescriptionBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            DescriptionBox.Font = new Font("Segoe UI", 9);
            DescriptionBox.Text = ""; // начальное пустое

            return DescriptionBox;
        }

        public CheckBox DrawCheckBox1()
        {
            AlBubble.Enabled = true;
            AlBubble.Location = new Point(600, 350/*380*/);
            AlBubble.Checked = false;
            AlBubble.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AlBubble.CheckedChanged += (sender, e) =>
            {
                if (AlBubble.Checked)
                {
                    AlChoose.Checked = false;
                    AlGnome.Checked = false;
                    AlComb.Checked = false;
                    Algorithm = new Algorithm_bubble();
                    Index = 0;
                    MessageBox.Show(
                    "Сортировка вставками (Insertion sort) — осуществляется проход по массиву слева направо. Каждый новый элемент вставляется в уже отсортированную часть массива на своё место: сравнивается с элементами слева, и при необходимости сдвигает их вправо. Алгоритм эффективен при частично отсортированных данных и работает \"на месте\", без выделения дополнительной памяти.",
                    "Описание алгоритма: Вставками",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                }
            };

            return AlBubble;
        }

        public CheckBox DrawCheckBox2()
        {
            AlChoose.Enabled = true;
            AlChoose.Location = new Point(600, 410);
            AlChoose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AlChoose.CheckedChanged += (sender, e) =>
            {
                if (AlChoose.Checked)
                {
                    AlBubble.Checked = false;
                    AlGnome.Checked = false;
                    AlComb.Checked = false;
                    Algorithm = new Algorithm_choose();
                    Index = 1;
                    MessageBox.Show(
                    "Сортировка выбором (Selection sort) — на каждом шаге из неотсортированной части массива выбирается минимальный элемент и меняется местами с первым элементом этой части. Далее алгоритм повторяется для оставшихся элементов. Этот метод легко реализуется, но не является стабильным и требует O(n²) времени.",
                    "Сортировка выбором"
                );
                }
            };
            return AlChoose;
        }

        public CheckBox DrawCheckBox3()
        {
            AlComb.Enabled = true;
            AlComb.Location = new Point(710, 350);
            AlComb.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AlComb.CheckedChanged += (sender, e) =>
            {
                if (AlComb.Checked)
                {
                    AlBubble.Checked = false;
                    AlChoose.Checked = false;
                    AlGnome.Checked = false;
                    Algorithm = new Algorithm_insertion();
                    Index = 2;
                    MessageBox.Show(
                "Сортировка пузырьком (Bubble sort) — выполняется некоторое количество проходов по массиву: начиная от начала, последовательно сравниваются пары соседних элементов. Если первый элемент больше второго — они меняются местами. Процесс повторяется до тех пор, пока при очередном проходе не будет выполнено ни одной перестановки. Каждый проход \"выталкивает\" наибольший элемент к концу массива, формируя отсортированную часть.",
                "Сортировка пузырьком"
            );
                }
            };
            return AlComb;
        }

        public CheckBox DrawCheckBox4()
        {
            AlGnome.Enabled = true;
            AlGnome.Location = new Point(710, 410);
            AlGnome.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AlGnome.CheckedChanged += (sender, e) =>
            {
                if (AlGnome.Checked)
                {
                    AlBubble.Checked = false;
                    AlChoose.Checked = false;
                    AlComb.Checked = false;
                    Algorithm = new Algorithm_merge();
                    Index = 3;
                    MessageBox.Show(
                    "Сортировка слиянием (Merge sort) — реализует принцип «разделяй и властвуй»: массив рекурсивно делится на две части до тех пор, пока не останутся элементы по одному. Затем пары объединяются в отсортированном порядке. Алгоритм требует дополнительную память, но гарантирует время выполнения O(n log n) и является стабильным.",
                    "Сортировка слиянием"
                );
                }
            };
            return AlGnome;
        }



        public RichTextBox DrawRichTextBox()
        {
            RCB.Location = new Point(520, 60);
            RCB.Size = new Size(250, 250);
            RCB.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            return RCB;
        }

        public System.Windows.Forms.TrackBar DrawTrackBar_1()
        {
            Trackbar1.Location = new Point(10, 340);
            Trackbar1.Size = new Size(500, 200);
            Trackbar1.Minimum = 0;
            Trackbar1.Maximum = 500;
            Trackbar1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            Trackbar1.ValueChanged += (sender, e) =>
            {
                label_count.Text = "Количество: " + Trackbar1.Value.ToString();
            };
            Trackbar1.TickFrequency = 100;
            return Trackbar1;
        }

        public System.Windows.Forms.TrackBar DrawTrackBar_2()
        {
            Trackbar2.Location = new Point(10, 400);
            Trackbar2.Size = new Size(300, 200);
            Trackbar2.Minimum = 0;
            Trackbar2.Maximum = 1000;
            Trackbar2.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            Trackbar2.ValueChanged += (sender, e) =>
            {
                label_speed.Text = "Скорость (мс): " + Trackbar2.Value.ToString();
            };
            return Trackbar2;
        }

        public System.Windows.Forms.Button DrawButton()
        {
            Generate.Text = "Генерация чисел";
            Generate.Size = new Size(100, 50);
            Generate.Location = new Point(670, 10);
            Generate.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            Generate.Click += (sender, e) =>
            {
                Work();
            };
            return Generate;
        }

        public Chart DrawChart()
        {
            Data.Location = new Point(10, 10);
            Data.Size = new Size(500, 300);
            Data.ChartAreas.Add(new ChartArea("area"));
            Data.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            // Настройка типа диаграммы
            Data.Series.Add(new Series("Сортировка"));
            Data.Series["Сортировка"].ChartType = SeriesChartType.Column;
            Data.Titles.Add("Сортировка");


            // Настройка меток осей
            Data.ChartAreas["area"].AxisX.Title = "Номера элементов";
            Data.ChartAreas["area"].AxisY.Title = "Значения элементов";
            return Data;
        }

        public System.Windows.Forms.Label DrawLabel1()
        {
            label_count.Size = new Size(150, 30);
            label_count.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label_count.Location = new Point(520, 10);
            label_count.Text = "Количество: " + Trackbar1.Value.ToString();
            return label_count;
        }


        public System.Windows.Forms.Label DrawLabel2()
        {
            label_speed.Size = new Size(150, 20);
            label_speed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label_speed.Location = new Point(520, 40);
            label_speed.Text = "Скорость (мс): " + Trackbar2.Value.ToString();
            return label_speed;
        }

        public System.Windows.Forms.Label DrawLabel3()
        {
            label_Bubble.Size = new Size(70, 30);
            label_Bubble.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label_Bubble.Location = new Point(575, 330);
            label_Bubble.Text = "Вставками";
            return label_Bubble;
        }

        public System.Windows.Forms.Label DrawLabel4()
        {
            label_Choose.Size = new Size(70, 30);
            label_Choose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label_Choose.Location = new Point(580, 390);
            label_Choose.Text = "Выбором";
            return label_Choose;
        }

        public System.Windows.Forms.Label DrawLabel5()
        {
            label_Comb.Size = new Size(70, 30);
            label_Comb.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label_Comb.Location = new Point(683, 330);
            label_Comb.Text = "Пузырек";
            return label_Comb;
        }

        public System.Windows.Forms.Label DrawLabel6()
        {
            label_Gnome.Size = new Size(70, 30);
            label_Gnome.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label_Gnome.Location = new Point(697, 390);
            label_Gnome.Text = "Слиянием";
            return label_Gnome;
        }
        public Label DrawTrackBar1Label()
        {
            Label label = new Label();
            label.Text = "Количество элементов";
            label.Location = new Point(10, 320);
            label.Size = new Size(200, 20);
            return label;
        }

        public Label DrawTrackBar2Label()
        {
            Label label = new Label();

            label.Text = "Скорость сортировки (мс)";
            label.Location = new Point(10, 370); // было 380, сдвинули выше
            label.Size = new Size(200, 20);
            label.BringToFront();

            return label;
        }


        public double[] Numbers;
        public double[] Start_work(int Lenght)
        {
            Numbers = new double[Lenght];
            for (int i = 0; i < Lenght; i++)
            {
                Numbers[i] = r.NextDouble();
            }
            return Numbers;
        }

        public string Return_String()
        {
            return (String.Join("\n", Numbers));
        }

        public async void Work()
        {
            Start_work(Trackbar1.Value);
            Data.Series[0].Points.Clear();

            for (int i = 0; i < Numbers.Length; i++)
            {
                Data.Series[0].Points.AddXY(i, Numbers[i]);
                Data.Series[0].Points[i].Color = Color.Blue;
            }

            List<(int, int, double, double)> Path = Algorithm.Sort(Numbers);
            Iters = 0;

            foreach ((int i1, int i2, double v1, double v2) in Path)
            {
                Iters++;

                Numbers[i1] = v1;
                Numbers[i2] = v2;

                for (int i = 0; i < Numbers.Length; i++)
                {
                    Data.Series[0].Points[i].YValues[0] = Numbers[i];
                    Data.Series[0].Points[i].Color = Color.Blue;
                }

                Data.Series[0].Points[i1].Color = Color.Red;
                Data.Series[0].Points[i2].Color = Color.Green;

                Data.Update();

                await Task.Delay(Trackbar2.Value); // ⬅️ не блокирует интерфейс
            }

            RCB.Text += $"Сортировка {What_Kind()} на {Trackbar1.Value} элементов завершена за {Iters} итераций\n";
        }




        public string What_Kind()
        {
            switch (Index)
            {
                case 0:
                    return "вставками";
                case 1:
                    return "выбором";
                case 2:
                    return "пузырьком";
                case 3:
                    return "методом слияния";
            }
            return "";
        }
    }
}
