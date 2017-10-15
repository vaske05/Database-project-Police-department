using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHibernate;
//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using Oracle.Web;
using Oracle.ManagedDataAccess.EntityFramework;
namespace PolicijskaUprava
{
    public partial class DodavanjeIntervencije : Form
    {
        public DodavanjeIntervencije()
        {
            InitializeComponent();
            OsveziGrid();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
        //osvezi
        private void button3_Click(object sender, EventArgs e)
        {
            OsveziGrid();
        }
        //osvezigrid
        void OsveziGrid()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from INTERVENCIJA", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridIntervencija.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //dodaj
        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            //groupBox1.Visible = false;
            groupBox2.Visible = true;
        }
        //izbrisi
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.GridIntervencija.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridIntervencija.SelectedRows[0].Index;

                int rowID = int.Parse(GridIntervencija[4, selectedIndex].Value.ToString());
                try
                {
                    ISession s = DataLayer.GetSession();

                    Entiteti.Intervencija u = s.Load<Entiteti.Intervencija>(rowID);

                    //brise se objekat iz baze ali ne i instanca objekta u memroiji
                    s.Delete(u);
                    //s.Delete("from Odeljenje");

                    s.Flush();
                    s.Close();
                    GridIntervencija.Rows.RemoveAt(selectedIndex);
                }

                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message);
                }
            }
            else
            {
                MessageBox.Show("Nije selektovana nijedna kolona.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    
    //zatvori
    private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //izmeni
        private void buttonIzmeni_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox1.Visible = true;

            if (this.GridIntervencija.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridIntervencija.SelectedRows[0].Index;

                string opis = GridIntervencija[0, selectedIndex].Value.ToString();
                textBox6.Text = opis;

                string vreme = GridIntervencija[2, selectedIndex].Value.ToString();
                textBox4.Text = vreme;

                string imeObjekta = GridIntervencija[3, selectedIndex].Value.ToString();
                textBox3.Text = imeObjekta;

                string idIntervencije = GridIntervencija[4, selectedIndex].Value.ToString();
                textBox2.Text = idIntervencije;

                string idPatrole = GridIntervencija[5, selectedIndex].Value.ToString();
                textBox1.Text = idPatrole;

                DateTime datum = DateTime.Parse(GridIntervencija[1, selectedIndex].Value.ToString());
                dateTimePicker1.Value = new DateTime(datum.Year, datum.Month, datum.Day);
            }
            else
            {
                MessageBox.Show("Nije selektovana nijedna kolona.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //potvrdi izmenu

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Entiteti.Intervencija st = s.Load<Entiteti.Intervencija>(int.Parse(textBox2.Text));

                st.Opis = textBox6.Text;
                st.Vreme = textBox4.Text;
                st.imeObjekta = textBox3.Text;
                st.Datum = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
                
                st.IntervencijaUPatroli.IdPatrole = int.Parse(textBox1.Text);
    
                s.Update(st);
                s.Flush();
                s.Close();
                OsveziGrid();

                textBox6.Clear();
                textBox4.Clear();
                textBox3.Clear();
                textBox2.Clear();
                textBox1.Clear();
             
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        //potvrdidodaju
        private void button5_Click(object sender, EventArgs e)
        {
            ISession s = DataLayer.GetSession();

            int idPatrole = int.Parse(textBox9.Text);
           

            Entiteti.Intervencija st = new Entiteti.Intervencija()
            {
                Opis = textBox18.Text,
                Vreme = textBox16.Text,
                imeObjekta = textBox15.Text,
                IdIntervencije=int.Parse(textBox10.Text),
                Datum = new DateTime(dateTimePicker6.Value.Year, dateTimePicker6.Value.Month, dateTimePicker6.Value.Day),
                IntervencijaUPatroli=s.Load<Entiteti.Patrola>(idPatrole),
            };

            s.Save(st);
            s.Flush();
            s.Close();
            MessageBox.Show("Intervencija " + st.Opis + " dodata.");
            OsveziGrid();
        }
    }
}
