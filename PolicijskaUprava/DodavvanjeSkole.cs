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
using Oracle.Web;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.EntityFramework;

namespace PolicijskaUprava
{
    public partial class DodavvanjeSkole : Form
    {
        public DodavvanjeSkole()
        {
            InitializeComponent();
            OsveziGrid();
        }
        void OsveziGrid()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from SKOLA", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridSkola.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Dodaj
        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
        }

        //Osvezi
        private void button3_Click(object sender, EventArgs e)
        {
            OsveziGrid();
        }

        //Brisanje
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.GridSkola.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridSkola.SelectedRows[0].Index;

                int rowID = int.Parse(GridSkola[5, selectedIndex].Value.ToString());
                try
                {
                    ISession s = DataLayer.GetSession();

                    Entiteti.Skola u = s.Load<Entiteti.Skola>(rowID);

                    //brise se objekat iz baze ali ne i instanca objekta u memroiji
                    s.Delete(u);
                    //s.Delete("from Odeljenje");

                    s.Flush();
                    s.Close();
                    GridSkola.Rows.RemoveAt(selectedIndex);
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

        //Zatvori
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Izmeni
        private void buttonIzmeni_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox1.Visible = true;
            
            if (this.GridSkola.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridSkola.SelectedRows[0].Index;

                string naziv = GridSkola[0, selectedIndex].Value.ToString();
                textBox6.Text = naziv;

                string tip = GridSkola[1, selectedIndex].Value.ToString();
                textBox5.Text = tip;

                string osobaZaKontakt = GridSkola[2, selectedIndex].Value.ToString();
                textBox4.Text = osobaZaKontakt;

                string broj = GridSkola[3, selectedIndex].Value.ToString();
                textBox3.Text = broj;


                string adresa = GridSkola[4, selectedIndex].Value.ToString();
                textBox2.Text = adresa;


                string idSkole = GridSkola[5, selectedIndex].Value.ToString();
                textBox1.Text = idSkole;

                OsveziGrid();
            }
            else
            {
                MessageBox.Show("Nije selektovana nijedna kolona.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //Potvrdi dodaju
        private void button5_Click(object sender, EventArgs e)
        {
            ISession s = DataLayer.GetSession();

            Entiteti.Skola st = new Entiteti.Skola()
            {  
            Naziv = textBox18.Text,
            Tip = textBox17.Text,
            OsobaZaKontakt = textBox16.Text,
            Broj = int.Parse(textBox15.Text),
            Adresa=textBox10.Text,
            IdSkole = int.Parse(textBox9.Text),
            };

            s.Save(st);
            s.Flush();
            s.Close();
            MessageBox.Show("Škola " + st.Naziv + " dodata.");
            OsveziGrid();
        }

        //Potvrdi izmenu
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Entiteti.Skola st = s.Load<Entiteti.Skola>(int.Parse(textBox1.Text));

                st.Tip = textBox5.Text;
                st.Naziv = textBox6.Text; ;
                st.OsobaZaKontakt = textBox4.Text;
                st.Broj = int.Parse(textBox3.Text);
                st.Adresa = textBox2.Text;
               

                s.Update(st);
                s.Flush();
                s.Close();
                OsveziGrid();

               
                textBox6.Clear();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
    }
}
    
