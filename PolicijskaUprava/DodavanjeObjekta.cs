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
    public partial class DodavanjeObjekta : Form
    {
        public DodavanjeObjekta()
        {
            InitializeComponent();
            OsveziGrid();
        }
        void OsveziGrid()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from OBJEKAT", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridObjekat.DataSource = dataTable;

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
            OsveziGrid();
        }
        //dodaj
        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox1.Visible = false;
        }
        //zatvori
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        //izmeni
        private void buttonIzmeni_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;

            if (this.GridObjekat.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridObjekat.SelectedRows[0].Index;

                string tip = GridObjekat[0, selectedIndex].Value.ToString();
                textBox1.Text = tip;

                string  idObjekta= GridObjekat[1, selectedIndex].Value.ToString();
                textBox2.Text = idObjekta;

                string povrsina = GridObjekat[2, selectedIndex].Value.ToString();
                textBox3.Text = povrsina;

                string ime = GridObjekat[3, selectedIndex].Value.ToString();
                textBox4.Text = ime;

               
                string prezime = GridObjekat[4, selectedIndex].Value.ToString();
                textBox5.Text = prezime;


                string telefon = GridObjekat[5, selectedIndex].Value.ToString();
                textBox11.Text = telefon;


                string adresa = GridObjekat[6, selectedIndex].Value.ToString();
                textBox12.Text = adresa;

                string idStanice = GridObjekat[7, selectedIndex].Value.ToString();
                textBox13.Text = idStanice;


                string serijskiBrojAlarma = GridObjekat[8, selectedIndex].Value.ToString();
                textBox14.Text = serijskiBrojAlarma;

                OsveziGrid();


            }
            else
            {
                MessageBox.Show("Nije selektovana nijedna kolona.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        //brisanje
        private void button4_Click(object sender, EventArgs e)
        {

            if (this.GridObjekat.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridObjekat.SelectedRows[0].Index;

                int rowID = int.Parse(GridObjekat[1, selectedIndex].Value.ToString());
                try
                {
                    ISession s = DataLayer.GetSession();

                    Entiteti.Objekat u = s.Load<Entiteti.Objekat>(rowID);

                    //brise se objekat iz baze ali ne i instanca objekta u memroiji
                    s.Delete(u);
                    //s.Delete("from Odeljenje");

                    s.Flush();
                    s.Close();
                    GridObjekat.Rows.RemoveAt(selectedIndex);
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
        //potvrid dodaju
        private void button5_Click(object sender, EventArgs e)
        {
            ISession s = DataLayer.GetSession();

            int idStanice = int.Parse(textBox7.Text);
            int serijskiBrojAlarma = int.Parse(textBox6.Text);

            Entiteti.Objekat st = new Entiteti.Objekat()
            {
                Tip = textBox18.Text,
                IdObjekta = int.Parse(textBox17.Text),
                Povrsina = int.Parse(textBox16.Text),
                Ime = textBox15.Text,
                Prezime = textBox10.Text,
                Telefon = textBox9.Text,
                Adresa=textBox8.Text,

                ObezbedjujeStanica=s.Load<Entiteti.Stanica>(idStanice),

        InstaliranJeAlarmniSistem = s.Load<Entiteti.AlarmniSistem>(serijskiBrojAlarma)
            };

            s.Save(st);
            s.Flush();
            s.Close();
            MessageBox.Show("Objekat " + st.Ime + " dodata.");
            OsveziGrid();
        }
        //potvriizmenu
        private void button1_Click(object sender, EventArgs e)
        {
        try
            {
                ISession s = DataLayer.GetSession();

                Entiteti.Objekat st = s.Load<Entiteti.Objekat>(int.Parse(textBox2.Text));

                st.Tip = textBox1.Text;
                st.Povrsina = int.Parse(textBox3.Text);
                st.Ime = textBox4.Text;
                st.Prezime= textBox5.Text;
                st.Telefon = textBox11.Text;
                st.Adresa = textBox12.Text;
                st.ObezbedjujeStanica.IdStanice = int.Parse(textBox13.Text);
                st.InstaliranJeAlarmniSistem.SerijskiBroj = int.Parse(textBox14.Text);

                s.Update(st);
                s.Flush();
                s.Close();
                OsveziGrid();

                textBox14.Clear();
                textBox13.Clear();
                textBox12.Clear();
                textBox11.Clear();
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


