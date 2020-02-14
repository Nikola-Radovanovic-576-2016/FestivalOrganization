using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FestivalOrganization
{
    public partial class AddFestival : Form
    {
        private const string connectionString = @"Data Source=PC-PC\SQLEXPRESS;Initial Catalog=SeminarskiBaze;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection;
        public AddFestival()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            connection.Open();
            initCombo1();
            initCombo2();
        }

        private void initCombo1()
        {
            
            string query = @"select ptt, naziv from grad";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet set = new DataSet();
            adapter.Fill(set);
            comboBox1.DisplayMember = "naziv";
            comboBox1.ValueMember = "ptt";
            comboBox1.DataSource = set.Tables[0];

        }
        private void initCombo2()
        {
            string query = @"select id_organizatora, ime_organizatora from organizator";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet set = new DataSet();
            adapter.Fill(set);
            comboBox2.DisplayMember = "ime_organizatora";
            comboBox2.ValueMember = "id_organizatora";
            comboBox2.DataSource = set.Tables[0];

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void AddFestival_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime pocetak = dateTimePicker1.Value;
            DateTime kraj = dateTimePicker2.Value;
            string naziv = textBox1.Text;
            int ptt = Convert.ToInt32(comboBox1.SelectedValue);
            int id_org = Convert.ToInt32(comboBox2.SelectedValue);
            if (DateTime.Compare(pocetak, kraj) < 0)
            {
                string query = @"insert into festival(ime_festivala, datum_pocetka, datum_kraja, ptt, id_org) VALUES " 
                                + "('" + naziv + "','" + pocetak + "','" + kraj + "','" + ptt + "','" + id_org + "')";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
                command.Dispose();
                MessageBox.Show("Dodat festival");
                this.Close();
            }
            else
                MessageBox.Show("Datum kraja festivala mora biti nakon pocetka");
        }
    }
}
