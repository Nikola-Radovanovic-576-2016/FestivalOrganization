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
    public partial class DeleteConcert : Form
    {
        private const string connectionString = @"Data Source=PC-PC\SQLEXPRESS;Initial Catalog=SeminarskiBaze;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection;
        public DeleteConcert()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            connection.Open();
            initCombo1();

        }
        private void initCombo1()
        {
            string query = @"select id_koncerta, ime_koncerta from Koncert";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet set = new DataSet();
            adapter.Fill(set);
            comboBox1.DisplayMember = "ime_koncerta";
            comboBox1.ValueMember = "id_koncerta";
            comboBox1.DataSource = set.Tables[0];
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int id_koncerta = Convert.ToInt32(comboBox1.SelectedValue);
            string query = "Delete Koncert where id_koncerta='" + id_koncerta + "'";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.DeleteCommand = command;
            adapter.DeleteCommand.ExecuteNonQuery();
            command.Dispose();
            MessageBox.Show("Obrisan izabrani koncert");
            this.Close();
        }

        private void DeleteConcert_Load(object sender, EventArgs e)
        {

        }
    }
}
