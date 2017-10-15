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
    public partial class DodavanjeStanice : Form
    {
        public DodavanjeStanice()
        {
            InitializeComponent();
            OsveziGrid();
        }

        //Dodaj
        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            
        }

        //Zatvori
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Osvezi
        private void button3_Click(object sender, EventArgs e)
        {
            OsveziGrid();
        }

        //Izmeni
        private void buttonIzmeni_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            //groupBox1.Visible = false;

            if (this.GridStanica.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridStanica.SelectedRows[0].Index;

                string idStanice = GridStanica[1, selectedIndex].Value.ToString();
                textBox13.Text = idStanice;

                string naziv = GridStanica[0, selectedIndex].Value.ToString();
                textBox14.Text = naziv;

                string adresa = GridStanica[2, selectedIndex].Value.ToString();
                textBox12.Text = adresa;

                string opstina = GridStanica[3, selectedIndex].Value.ToString();
                textBox11.Text = opstina;

                DateTime datumOsnivanja = DateTime.Parse(GridStanica[4, selectedIndex].Value.ToString());
                dateTimePicker2.Value = new DateTime(datumOsnivanja.Year, datumOsnivanja.Month, datumOsnivanja.Day);

                string brVozila = GridStanica[5, selectedIndex].Value.ToString();
                textBox10.Text = brVozila;


                string jmbgSefa = GridStanica[6, selectedIndex].Value.ToString();
                textBox9.Text = jmbgSefa;


                string idUprave = GridStanica[7, selectedIndex].Value.ToString();
                textBox8.Text = idUprave;

                OsveziGrid();

                
            }
            else
            {
                MessageBox.Show("Nije selektovana nijedna kolona.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }



        void OsveziGrid()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from POLICIJSKA_STANICA", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridStanica.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Potvrdi dodavanje
        private void buttonSubmitDodaj_Click(object sender, EventArgs e)
        {
            ISession s = DataLayer.GetSession();

            int jmbgSefa = int.Parse(textBox6.Text);
            int idUprave = int.Parse(textBox7.Text);

            Entiteti.Stanica st = new Entiteti.Stanica()
            {
                IdStanice = int.Parse(textBox1.Text),
                Naziv = textBox2.Text,
                Adresa = textBox3.Text,
                Opstina = textBox4.Text,
                DatumOsnivanja = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day),
                BrojVozila = int.Parse(textBox5.Text),

               SefujeSef = s.Load<Entiteti.Sef>(jmbgSefa),
               SastojiSeUprava = s.Load<Entiteti.Uprava>(idUprave)
            };

            s.Save(st);
            s.Flush();
            s.Close();
            MessageBox.Show("Stanica " + st.Naziv + " dodata.");
            OsveziGrid();
        }

        //Brisanje
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.GridStanica.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridStanica.SelectedRows[0].Index;

                int rowID = int.Parse(GridStanica[1, selectedIndex].Value.ToString());
                try
                {
                    ISession s = DataLayer.GetSession();

                    Entiteti.Stanica u = s.Load<Entiteti.Stanica>(rowID);

                    //brise se objekat iz baze ali ne i instanca objekta u memroiji
                    s.Delete(u);
                    //s.Delete("from Odeljenje");

                    s.Flush();
                    s.Close();
                    GridStanica.Rows.RemoveAt(selectedIndex);
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

        //Potvrdi izmenu
        private void buttonSubmitIzmeni_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Entiteti.Stanica st = s.Load<Entiteti.Stanica>(int.Parse(textBox13.Text));

                st.Naziv = textBox14.Text;
                st.Adresa = textBox12.Text;
                st.Opstina = textBox11.Text;
                st.DatumOsnivanja = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
                st.BrojVozila = int.Parse(textBox10.Text);

                st.SefujeSef.Jmbg = int.Parse(textBox9.Text);
                st.SastojiSeUprava.IdUprave = int.Parse(textBox8.Text);

                s.Update(st);
                s.Flush();
                s.Close();
                OsveziGrid();

                textBox14.Clear();
                textBox13.Clear();
                textBox12.Clear();
                textBox11.Clear();
                textBox10.Clear();
                textBox9.Clear();
                textBox8.Clear();
            }
            catch(Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
    }
}
