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
    public partial class AddMusician : Form
    {
        private const string connectionString = @"Data Source=PC-PC\SQLEXPRESS;Initial Catalog=SeminarskiBaze;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection;
        public AddMusician()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            connection.Open();
            initCombo1();
            initCombo2();
        }

        private void AddMusician_Load(object sender, EventArgs e)
        {

        }
        private void initCombo1()
        {

            string query = @"select id_koncerta, ime_koncerta from koncert";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet set = new DataSet();
            adapter.Fill(set);
            comboBox1.DisplayMember = "ime_koncerta";
            comboBox1.ValueMember = "id_koncerta";
            comboBox1.DataSource = set.Tables[0];

        }
        private void initCombo2()
        {

            string query = @"select id_izvodjaca, ime_izvodjaca from izvodjac";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet set = new DataSet();
            adapter.Fill(set);
            comboBox2.DisplayMember = "ime_izvodjaca";
            comboBox2.ValueMember = "id_izvodjaca";
            comboBox2.DataSource = set.Tables[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                int plata = Convert.ToInt32(textBox1.Text);
                int id_konc = Convert.ToInt32(comboBox1.SelectedValue);
                int id_izv = Convert.ToInt32(comboBox2.SelectedValue);
                string query = @"insert into svira(id_koncerta, id_izvodjaca, plata) VALUES " + "('" + id_konc + "','" + id_izv + "','" + plata + "')";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
                command.Dispose();
            }
            catch
            {
                MessageBox.Show("Plata mora biti broj!");
            }
        }
        
    }
}
