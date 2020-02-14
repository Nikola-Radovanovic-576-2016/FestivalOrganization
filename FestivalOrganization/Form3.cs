using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FestivalOrganization
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddShow prikaz = new AddShow();
            prikaz.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 prikaz = new Form1();
            prikaz.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddFestival prikaz = new AddFestival();
            prikaz.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddConcert prikaz = new AddConcert();
            prikaz.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddMusician prikaz = new AddMusician();
            prikaz.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddActor prikaz = new AddActor();
            prikaz.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SelectFestival prikaz = new SelectFestival();
            prikaz.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DeleteFestival prikaz = new DeleteFestival();
            prikaz.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SelectConcert prikaz = new SelectConcert();
            prikaz.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DeleteConcert prikaz = new DeleteConcert();
            prikaz.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SelectShow prikaz = new SelectShow();
            prikaz.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DeleteShow prikaz = new DeleteShow();
            prikaz.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ExportForm prikaz = new ExportForm();
            prikaz.Show();
        }
    }
}
