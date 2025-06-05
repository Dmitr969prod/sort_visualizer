using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace Визуализатор_сортировки
{
    public partial class MainForm : Form 
    {
        private Presenter P;

        public MainForm(Presenter P_)
        {
            P=P_;


            this.Controls.Add(P.DrawTrackBar1Label());

            this.Controls.Add(P.DrawTrackBar2Label());

            this.Controls.Add(P.DrawChart());

            this.Controls.Add(P.DrawLabel1());

            this.Controls.Add(P.DrawLabel2());

            this.Controls.Add(P.DrawButton());

            this.Controls.Add(P.DrawRichTextBox());

            this.Controls.Add(P.DrawTrackBar_1());

            this.Controls.Add(P.DrawTrackBar_2());

            this.Controls.Add(P.DrawCheckBox1());

            this.Controls.Add(P.DrawCheckBox2());

            this.Controls.Add(P.DrawCheckBox3());

            this.Controls.Add(P.DrawCheckBox4());

            this.Controls.Add(P.DrawLabel3());

            this.Controls.Add(P.DrawLabel4());

            this.Controls.Add(P.DrawLabel5());

            this.Controls.Add(P.DrawLabel6());

            this.Controls.Add(P.DrawDescriptionBox());

            

            this.Controls.Add(P.DrawTrackBar_1());
            this.Controls.Add(P.DrawTrackBar_2());
            this.Controls.Add(P.DrawLegendWithColors());
            






            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen; 
            //this.Size = new Size(1000, 700);                     


        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
