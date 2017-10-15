using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using NHibernate;
namespace PolicijskaUprava
{
    public partial class DodavanjeSektora : Form
    {
        public DodavanjeSektora()
        {
            InitializeComponent();
            OsveziGrid();
        }
        void OsveziGrid()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from SEKTOR", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridSektor.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OsveziGridSa()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from SAOBRACAJ", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridSektor.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OsveziGridV()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from VANREDNE_SITUACIJE", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridSektor.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //osvezi

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true ||radioButton4.Checked==true)
            {
                OsveziGridSa();
            }
            else
                 if (radioButton2.Checked == true||radioButton4.Checked==true)
            {
                OsveziGridV();
            }
            else
                if (radioButton5.Checked == true)
            {
                OsveziGrid();
            }
        }
        //dodaj
        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox1.Visible = false;


        }
        //izmeni
        private void buttonIzmeni_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            if (radioButton3.Checked == true)
            {
                MessageBox.Show("Nije selektovana nijedna kolona ili sektor ne moze da se menja ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                izmeniSaobracaj();
            }
        }





        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //izbrisi
        private void button4_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            { brisemSaobracaj();
            }
            else 
                if(radioButton2.Checked==true)
            { BrisemSektor(); }
            else
            {
                MessageBox.Show("Nije selektovana nijedna kolona.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //potvrdi izmenu
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
               
                ISession s = DataLayer.GetSession();

                Entiteti.Saobracaj st = s.Load<Entiteti.Saobracaj>(int.Parse(textBox2.Text));

                st.NazivUlice = textBox1.Text;
                s.Update(st);
                s.Flush();
                s.Close();
                OsveziGridSa();

                textBox1.Clear();

            }
            else
                MessageBox.Show("Nije moguce menjati podatke u ovom sektoru.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //potvrdi  dodaju

        private void button5_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                ISession s = DataLayer.GetSession();
              



                Entiteti.Saobracaj st = new Entiteti.Saobracaj()
                {
                    IdSektora = int.Parse(textBox10.Text),
                    NazivUlice = textBox9.Text,
                };

                s.Save(st);
                s.Flush();
                s.Close();
                MessageBox.Show("Sektor " + st.NazivUlice + " dodat.");
                OsveziGridSa();
            }
            else
            {
                if (radioButton2.Checked == true)
                {
                   
                   
                    ISession s = DataLayer.GetSession();



                    Entiteti.VanredneSituacije st = new Entiteti.VanredneSituacije()
                    {
                        IdSektora = int.Parse(textBox10.Text),

                    };

                    s.Save(st);
                    s.Flush();
                    s.Close();
                    MessageBox.Show("Sektor " + st.IdSektora + " dodat.");
                    OsveziGrid();
                }
            }
        }

        private void DodavanjeSektora_Load(object sender, EventArgs e)
        {

        }
        void brisemSaobracaj()
        {
            if (this.GridSektor.SelectedRows.Count > 0)
            {


                int selectedIndex = this.GridSektor.SelectedRows[0].Index;
                int rowID = int.Parse(GridSektor[1, selectedIndex].Value.ToString());


                try
                {
                    ISession s = DataLayer.GetSession();

                    Entiteti.Sektor u = s.Load<Entiteti.Sektor>(rowID);

                    //brise se objekat iz baze ali ne i instanca objekta u memroiji
                    s.Delete(u);
                    //s.Delete("from Odeljenje");

                    s.Flush();
                    s.Close();
                    GridSektor.Rows.RemoveAt(selectedIndex);
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
        void BrisemSektor()
        {
           
            
                if (this.GridSektor.SelectedRows.Count > 0)
                {


                    int selectedIndex = this.GridSektor.SelectedRows[0].Index;
                    int rowID = int.Parse(GridSektor[0, selectedIndex].Value.ToString());


                    try
                    {
                        ISession s = DataLayer.GetSession();

                        Entiteti.Sektor u = s.Load<Entiteti.Sektor>(rowID);

                        //brise se objekat iz baze ali ne i instanca objekta u memroiji
                        s.Delete(u);
                        //s.Delete("from Odeljenje");

                        s.Flush();
                        s.Close();
                        GridSektor.Rows.RemoveAt(selectedIndex);
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
        void izmeniSaobracaj()
        {

            if (this.GridSektor.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridSektor.SelectedRows[0].Index;

                string nazivulice = GridSektor[0, selectedIndex].Value.ToString();
                textBox1.Text = nazivulice;
                string idSektora = GridSektor[1, selectedIndex].Value.ToString();
                textBox2.Text = idSektora;

                OsveziGridSa();


            }
            else
            {
                MessageBox.Show("Nije selektovana nijedna kolona ili nije moguce menjati podatke iz tog sektora", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox9.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox9.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox9.Enabled =false;
        }
    }
}