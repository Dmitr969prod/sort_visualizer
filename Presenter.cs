using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Визуализатор_сортировки
{
    public class Presenter
    {
        public ISort Algorithm = null;
        private Random r = new Random();
        private bool paused = false;
        private bool stepByStep = false;
        private TaskCompletionSource<bool> stepSignal;
        private bool stepPending = false;   // запрос на один шаг
 
        private readonly Button _resetBtn = new Button();
        private volatile bool _cancelRequested = false;   // сигнал «остановить сортировку»






        private bool stepMode = false;       // включён ли режим "только один шаг"
        

        private readonly Button _startBtn = new Button();
        private readonly Button _pauseBtn = new Button();
        private readonly Button _stepBtn = new Button();


        private bool _isPaused = false;
        private readonly Button _pauseButton = new Button();


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


        // ───────── кнопки ─────────


        public Button DrawResetButton()
        {
            _resetBtn.Text = "Сброс";
            _resetBtn.Size = new Size(85, 35);
            _resetBtn.Location = new Point(490, 400);          // рядом с «Шаг →»
            _resetBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _resetBtn.Click += (s, e) =>
            {
                // 1) сообщаем Work(), что нужно прервать цикл
                _cancelRequested = true;
                // 2) снимаем все режимы ожидания, чтобы Work() не «спал»
                paused = false;
                stepPending = false;
            };
            return _resetBtn;
        }

        public Button DrawStartButton()
        {
            _startBtn.Text = "Старт";
            _startBtn.Size = new Size(85, 35);
            _startBtn.Location = new Point(510, 285);
            _startBtn.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            _startBtn.Click += (s, e) =>
            {
                paused = false;
                stepMode = false;                    // автоматический режим
                stepSignal?.TrySetResult(true);      // если сортировка ждёт – продолжаем
            };
            return _startBtn;
        }

        public Button DrawPauseButton()
        {
            _pauseBtn.Text = "Пауза";
            _pauseBtn.Size = new Size(85, 35);
            _pauseBtn.Location = new Point(595, 285);
            _pauseBtn.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            _pauseBtn.Click += (s, e) =>
            {
                paused = !paused;
                
                if (!paused) stepSignal?.TrySetResult(true);
            };
            return _pauseBtn;
        }

        public Button DrawStepButton()
        {
            _stepBtn.Text = "Шаг →";
            _stepBtn.Size = new Size(85, 35);
            _stepBtn.Location = new Point(685, 285);
            _stepBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _stepBtn.Click += (s, e) =>
            {
                stepPending = true;   // просим выполнить ОДИН шаг
                paused = false;  // снимаем паузу ровно на этот шаг
            };
            return _stepBtn;
        }




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
                    "Сортировка пузырьком (Bubble sort) — выполняется некоторое количество проходов по массиву: начиная от начала, последовательно сравниваются пары соседних элементов. Если первый элемент больше второго — они меняются местами. Процесс повторяется до тех пор, пока при очередном проходе не будет выполнено ни одной перестановки. Каждый проход \"выталкивает\" наибольший элемент к концу массива, формируя отсортированную часть.",
                    "Сортировка пузырьком"
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
                   "Сортировка вставками (Insertion sort) — осуществляется проход по массиву слева направо. Каждый новый элемент вставляется в уже отсортированную часть массива на своё место: сравнивается с элементами слева, и при необходимости сдвигает их вправо. Алгоритм эффективен при частично отсортированных данных и работает \"на месте\", без выделения дополнительной памяти.",
                   "Описание алгоритма: Вставками"
                   //MessageBoxButtons.OK,
                   //MessageBoxIcon.Information
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
            RCB.Size = new Size(250, 120);
            RCB.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            RCB.ReadOnly = true;
            RCB.Font = new Font("Segoe UI", 9);

            return RCB;
        }
        public Panel DrawLegendWithColors()
        {
            Panel legendPanel = new Panel();
            legendPanel.Size = new Size(250, 100);
            legendPanel.Location = new Point(520, 185); // ниже RichTextBox
            legendPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            string[] texts = {
        "обычный элемент",
        "сравнение (левый)",
        "сравнение (правый)",
        "обмен"
    };

            Color[] colors = {
        Color.Blue,
        Color.Red,
        Color.Orange,
        Color.Green
    };

            for (int i = 0; i < texts.Length; i++)
            {
                Panel colorDot = new Panel();
                colorDot.Size = new Size(12, 12);
                colorDot.Location = new Point(0, i * 20 + 4);
                colorDot.BackColor = colors[i];

                Label label = new Label();
                label.Text = texts[i];
                label.Location = new Point(20, i * 20);
                label.Size = new Size(200, 20);

                legendPanel.Controls.Add(colorDot);
                legendPanel.Controls.Add(label);
            }

            return legendPanel;
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
            label_speed.Text = "Задержка (мс): " + Trackbar2.Value.ToString();
            return label_speed;
        }

        public System.Windows.Forms.Label DrawLabel3()
        {
            label_Bubble.Size = new Size(90, 30);
            label_Bubble.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label_Bubble.Location = new Point(575, 330);
            label_Bubble.Text = "Пузырьком";
            return label_Bubble;
        }

        public System.Windows.Forms.Label DrawLabel4()
        {
            label_Choose.Size = new Size(85, 30);
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
            label_Comb.Text = "Вставками";
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
            var label = new Label
            {
                Text = "Количество элементов",
                Location = new Point(10, 320),
                Size = new Size(200, 20),

                
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            return label;
        }

        public Label DrawTrackBar2Label()
        {
            var label = new Label
            {
                Text = "Время задержки (мс)",
                Location = new Point(10, 370),
                Size = new Size(200, 20),

                
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

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
        private void LogLine(string msg)
        {
            RCB.AppendText(msg + Environment.NewLine);
            RCB.SelectionStart = RCB.TextLength;
            RCB.ScrollToCaret();
        }


        public async void Work()
        {
            /* ---------- 1. проверка выбранного алгоритма ---------- */
            if (Algorithm == null)
            {
                MessageBox.Show("Сначала выберите алгоритм сортировки.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            /* ---------- 2. подготовка исходных данных ---------- */
            Start_work(Math.Max(2, Trackbar1.Value));      // минимум 2 элемента
            Data.Series[0].Points.Clear();
            for (int i = 0; i < Numbers.Length; i++)
            {
                Data.Series[0].Points.AddXY(i, Numbers[i]);
                Data.Series[0].Points[i].Color = Color.Blue;
            }

            /* ---------- 2-b. сброс внутренних флагов ---------- */
            _cancelRequested = false;      // ← флаг, который ставит кнопка «Сброс»
            paused = false;
            stepPending = false;

            /* ---------- 3. получаем последовательность шагов ---------- */
            var path = Algorithm.Sort(Numbers);            // (i1, i2, v1, v2)
            Iters = 0;

            /* ---------- 4. основной цикл визуализации ---------- */
            foreach ((int i1, int i2, double v1, double v2) in path)
            {
                if (_cancelRequested) break;               // мгновенное завершение

                Iters++;

                /* 4.1 подсветка сравнения */
                for (int k = 0; k < Numbers.Length; k++)
                    Data.Series[0].Points[k].Color = Color.Blue;

                Data.Series[0].Points[i1].Color = Color.Red;      // левый
                Data.Series[0].Points[i2].Color = Color.Orange;   // правый
                LogLine($"Сравнение: [{i1}] = {Numbers[i1]:0.00} и [{i2}] = {Numbers[i2]:0.00}");
                Data.Update();

                /* 4.2 пауза / один шаг / авто-режим */
                while (paused && !stepPending && !_cancelRequested)
                    await Task.Delay(50);

                if (_cancelRequested) break;

                if (stepPending)                // выполнить ровно ОДИН шаг
                {
                    stepPending = false;
                    paused = true;         // вернуться в паузу
                }
                else
                {
                    await Task.Delay(Math.Max(10, Trackbar2.Value));
                    if (_cancelRequested) break;
                }

                /* 4.3 анимация обмена (если требуется) */
                bool needSwap = Numbers[i1] != v1 || Numbers[i2] != v2;
                if (needSwap)
                {
                    double old1 = Numbers[i1];
                    double old2 = Numbers[i2];
                    LogLine($"Обмен: [{i1}] ⇄ [{i2}]");

                    const int frames = 10;
                    int frameDelay = Math.Max(10, Trackbar2.Value) / frames;

                    for (int f = 1; f <= frames; f++)
                    {
                        if (_cancelRequested) break;

                        double t = f / (double)frames;
                        Numbers[i1] = old1 + (v1 - old1) * t;
                        Numbers[i2] = old2 + (v2 - old2) * t;

                        for (int k = 0; k < Numbers.Length; k++)
                            Data.Series[0].Points[k].YValues[0] = Numbers[k];

                        Data.Series[0].Points[i1].Color = Color.Green;
                        Data.Series[0].Points[i2].Color = Color.Green;
                        Data.Update();

                        await Task.Delay(frameDelay);
                    }
                    if (_cancelRequested) break;

                    Numbers[i1] = v1;
                    Numbers[i2] = v2;
                }
                else
                {
                    LogLine("→ Без обмена");
                }

                LogLine("");   // пустая строка-разделитель
            }

            /* ---------- 5. завершение или сброс ---------- */
            if (_cancelRequested)
            {
                // Полный сброс визуализации и логов
                Data.Series[0].Points.Clear();
                Data.Update();
                RCB.Clear();
            }
            else
            {
                for (int k = 0; k < Numbers.Length; k++)
                    Data.Series[0].Points[k].Color = Color.Blue;

                Data.Update();
                LogLine($"Сортировка {What_Kind()} завершена за {Iters} шагов");
            }

            /* ---------- 6. финальная очистка внутренних состояний ---------- */
            paused = false;
            stepPending = false;
            _cancelRequested = false;        // чтобы следующий запуск начинал «с чистого листа»
        }









        public string What_Kind()
        {
            switch (Index)
            {
                case 0:
                    return "пузырьком";
                case 1:
                    return "выбором";
                case 2:
                    return "вставками";
                case 3:
                    return "методом слияния";
            }
            return "";
        }
    }
}
