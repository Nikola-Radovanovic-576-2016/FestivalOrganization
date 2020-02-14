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
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace FestivalOrganization
{
    public partial class ExportForm : Form
    {
        private const string connectionString = @"Data Source=PC-PC\SQLEXPRESS;Initial Catalog=SeminarskiBaze;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection;
        public ExportForm()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        private void ExportForm_Load(object sender, EventArgs e)
        {
            var select = @"SELECT  ime_koncerta, ime_izvodjaca, vreme_pocetka, vreme_zavrsetka, ime_festivala FROM izvodjac,svira,koncert,Festival where svira.id_izvodjaca=izvodjac.id_izvodjaca AND svira.id_koncerta=koncert.id_koncerta and koncert.id_festivala=Festival.id_festivala ORDER BY ime_festivala;";
            var c = new SqlConnection(connectionString);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            DataTable dtData = new DataTable();
            dataAdapter.Fill(dtData);
            dataGridView1.DataSource = dtData;

            select = @"SELECT naziv_predstave, ime_glumca + ' ' + prezime_glumca AS puno_ime, vreme_pocetka, vreme_zavrsetka, ime_festivala FROM glumac,glumi,predstava,Festival where glumi.id_glumca=glumac.id_glumca AND glumi.id_predstave=Predstava.id_predstave and Predstava.id_festivala=Festival.id_festivala ORDER BY ime_festivala;";
            dataAdapter = new SqlDataAdapter(select, c);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "izlaz.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Nije moguce upisati podatke." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            
                            PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            //pdfTable.DefaultCell.BackgroundColor = BaseColor.LIGHT_GRAY;

                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (cell.Value != null)
                                        pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            PdfPTable pdfTable2 = new PdfPTable(dataGridView2.Columns.Count);
                            pdfTable2.DefaultCell.Padding = 3;
                            pdfTable2.WidthPercentage = 100;
                            pdfTable2.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView2.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                pdfTable2.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView2.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (cell.Value != null)
                                        pdfTable2.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();

                                iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 32);
                                iTextSharp.text.Font regularFont = FontFactory.GetFont("Arial", 36);
                                Paragraph title;
                                title = new Paragraph("Organizovani festivali", titleFont);
                                title.Alignment = Element.ALIGN_CENTER;
                                pdfDoc.Add(title);
                                pdfDoc.Add(new Phrase("Koncerti"));
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Add(new Phrase(""));
                                pdfDoc.Add(new Phrase(""));
                                pdfDoc.Add(new Phrase("Predstave"));
                                pdfDoc.Add(pdfTable2);
                                pdfDoc.Close();
                                stream.Close();

                            }

                            MessageBox.Show("Uspesno exportovano!", "Info");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Greska :" + ex.Message);
                        }
                        
                    }
                }
            }
            else
            {
                MessageBox.Show("Tabele su prazne!", "Info");
            }
        }
    }
}
