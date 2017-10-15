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
using Oracle.ManagedDataAccess.Client;
namespace PolicijskaUprava
{
    public partial class DodavanjeVozila : Form
    {
        public DodavanjeVozila()
        {
            InitializeComponent();
            OsveziGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OsveziGrid();
        }
        void OsveziGrid()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from VOZILO", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridVozilo.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //zatvori
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //dodaj
        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
        }
        //izbrisi
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.GridVozilo.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridVozilo.SelectedRows[0].Index;

                int rowID = int.Parse(GridVozilo[3, selectedIndex].Value.ToString());
                try
                {
                    ISession s = DataLayer.GetSession();

                    Entiteti.Uprava u = s.Load<Entiteti.Uprava>(rowID);

                    //brise se objekat iz baze ali ne i instanca objekta u memroiji
                    s.Delete(u);
                    //s.Delete("from Odeljenje");

                    s.Flush();
                    s.Close();
                    GridVozilo.Rows.RemoveAt(selectedIndex);
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
        //izmeni
        private void buttonIzmeni_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox1.Visible = true;

            if (this.GridVozilo.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridVozilo.SelectedRows[0].Index;

                string model = GridVozilo[0, selectedIndex].Value.ToString();
                textBox6.Text = model;

                string tip = GridVozilo[1, selectedIndex].Value.ToString();
                textBox4.Text = tip;

                string proizvodjac = GridVozilo[2, selectedIndex].Value.ToString();
                textBox3.Text = proizvodjac;

                string registracija = GridVozilo[3, selectedIndex].Value.ToString();
                textBox2.Text = registracija;


                string boja = GridVozilo[4, selectedIndex].Value.ToString();
                textBox1.Text = boja;


              

                OsveziGrid();
            }
            else
            {
                MessageBox.Show("Nije selektovana nijedna kolona.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        //potvrdi dodaju
        private void button5_Click(object sender, EventArgs e)
        {
            ISession s = DataLayer.GetSession();

            Entiteti.Vozilo st = new Entiteti.Vozilo()
            {
                Model = textBox18.Text,
                Tip = textBox16.Text,
                Proizvodjac = textBox15.Text,
                Registracija = int.Parse(textBox10.Text),
                Boja = textBox9.Text,
               
            };

            s.Save(st);
            s.Flush();
            s.Close();
            MessageBox.Show("Vozilo " + st.Model +st.Tip + " Dodata.");
            OsveziGrid();
        }
        //izmeni vozilo
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Entiteti.Vozilo st = s.Load<Entiteti.Vozilo>(int.Parse(textBox2.Text));

                st.Model = textBox6.Text;
                st.Tip = textBox4.Text; ;
                st.Proizvodjac = textBox3.Text;
                st.Boja = textBox1.Text;


                s.Update(st);
                s.Flush();
                s.Close();
                OsveziGrid();


                textBox6.Clear();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
    }
    
}
