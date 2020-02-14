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
    public partial class AddConcert : Form
    {
        private const string connectionString = @"Data Source=PC-PC\SQLEXPRESS;Initial Catalog=SeminarskiBaze;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection;
        public AddConcert()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            connection.Open();
            initCombo1();
            initCombo2();
        }
        private void initCombo1()
        {

            string query = @"select id_festivala, ime_festivala from festival";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet set = new DataSet();
            adapter.Fill(set);
            comboBox1.DisplayMember = "ime_festivala";
            comboBox1.ValueMember = "id_festivala";
            comboBox1.DataSource = set.Tables[0];

        }
        private void initCombo2()
        {

            string query = @"select id_bine, ime_bine from bina";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet set = new DataSet();
            adapter.Fill(set);
            comboBox2.DisplayMember = "ime_bine";
            comboBox2.ValueMember = "id_bine";
            comboBox2.DataSource = set.Tables[0];

        }
        private void AddConcert_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime pocetak = dateTimePicker1.Value;
            DateTime kraj = dateTimePicker2.Value;
            string naziv = textBox1.Text;
            int id_fest = Convert.ToInt32(comboBox1.SelectedValue);
            int id_bine = Convert.ToInt32(comboBox2.SelectedValue);

            string fest_query = @"select datum_pocetka, datum_kraja from festival where id_festivala=" + id_fest;
            SqlCommand myCommand = new SqlCommand(fest_query, connection);
            SqlDataReader myReader = myCommand.ExecuteReader();
            myReader.Read();
            DateTime fest_pocetak = Convert.ToDateTime(myReader["datum_pocetka"]);
            DateTime fest_kraj = Convert.ToDateTime(myReader["datum_kraja"]);
            myReader.Close();
            //validacija podataka vreme kraja nakon vremena pocetka, takodje trajanje koncerta mora biti u okviru festivala
            if (DateTime.Compare(pocetak, kraj) < 0 && DateTime.Compare(pocetak, fest_pocetak) > 0 && DateTime.Compare(kraj, fest_kraj) < 0)
            {
                string query = @"insert into koncert(ime_koncerta, vreme_pocetka, vreme_zavrsetka, id_festivala, id_bine) VALUES " 
                               + "('" + naziv + "','" + pocetak + "','" + kraj + "','" + id_fest + "','" + id_bine + "')";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
                command.Dispose();
            }
            else
                MessageBox.Show("Datumi nisu izabrani dobro");
        }
    }
}
