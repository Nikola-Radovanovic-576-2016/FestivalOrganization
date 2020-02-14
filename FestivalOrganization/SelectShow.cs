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
    public partial class SelectShow : Form
    {
        private const string connectionString = @"Data Source=PC-PC\SQLEXPRESS;Initial Catalog=SeminarskiBaze;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection;

        public SelectShow()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            initCombo1();
        }
        private void initCombo1()
        {
            string query = @"select id_predstave, naziv_predstave FROM Predstava";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataSet set = new DataSet();
            adapter.Fill(set);
            comboBox1.DisplayMember = "naziv_predstave";
            comboBox1.ValueMember = "id_predstave";
            comboBox1.DataSource = set.Tables[0];
        }
        private void SelectShow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            UpdateShow prikaz = new UpdateShow();
            prikaz.predstavaId = Convert.ToInt32(comboBox1.SelectedValue);
            prikaz.Show();

        }
    }
}
