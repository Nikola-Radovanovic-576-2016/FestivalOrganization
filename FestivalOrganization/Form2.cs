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
    public partial class Form2 : Form
    {
        public int festivalId { get; set; }
        private const string connectionString = @"Data Source=PC-PC\SQLEXPRESS;Initial Catalog=SeminarskiBaze;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Form2()
        {
            
            InitializeComponent();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var select = @"SELECT ime_izvodjaca, vreme_pocetka, vreme_zavrsetka, ime_koncerta, ime_bine, ime_festivala FROM izvodjac,svira,koncert, bina, festival 
                         where svira.id_izvodjaca=izvodjac.id_izvodjaca AND svira.id_koncerta=koncert.id_koncerta AND koncert.id_bine = bina.id_bine AND
                         koncert.id_festivala=" + festivalId + " AND festival.id_festivala=" + festivalId + " ORDER BY vreme_pocetka;";
            var c = new SqlConnection(connectionString);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            select = @"SELECT ime_glumca + ' ' + prezime_glumca AS glumac, vreme_pocetka, vreme_zavrsetka, naziv_predstave, broj_sedista, ime_festivala FROM glumac, glumi, predstava, scena, festival
                     where glumi.id_glumca=glumac.id_glumca AND glumi.id_predstave=predstava.id_predstave AND scena.id_scene=predstava.id_scene AND 
                     predstava.id_festivala=" + festivalId + " AND festival.id_festivala=" + festivalId + " ORDER BY vreme_pocetka;";
            dataAdapter = new SqlDataAdapter(select, c);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
          
        }
    }
}
