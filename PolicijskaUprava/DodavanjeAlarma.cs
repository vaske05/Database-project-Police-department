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
using Oracle.Web;
using Oracle.ManagedDataAccess.Client;
namespace PolicijskaUprava
{
    public partial class DodavanjeAlarma : Form
    {
        public DodavanjeAlarma()
        {
            InitializeComponent();
            OsveziGrid();
        }

        //Izmeni
        private void buttonIzmeni_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            //groupBox1.Visible = false;

            if (this.GridAlarmniSistem.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridAlarmniSistem.SelectedRows[0].Index;

                string opis = GridAlarmniSistem[0, selectedIndex].Value.ToString();
                textBox1.Text = opis;

                DateTime datumAtesta = DateTime.Parse(GridAlarmniSistem[1, selectedIndex].Value.ToString());
                dateTimePicker1.Value = new DateTime(datumAtesta.Year, datumAtesta.Month, datumAtesta.Day);

                DateTime datumInstalacije = DateTime.Parse(GridAlarmniSistem[2, selectedIndex].Value.ToString());
                dateTimePicker2.Value = new DateTime(datumInstalacije.Year, datumInstalacije.Month, datumInstalacije.Day);

                DateTime pocetniDatum = DateTime.Parse(GridAlarmniSistem[3, selectedIndex].Value.ToString());
                dateTimePicker3.Value = new DateTime(pocetniDatum.Year, pocetniDatum.Month, pocetniDatum.Day);

                DateTime krajnjiDatum = DateTime.Parse(GridAlarmniSistem[4, selectedIndex].Value.ToString());
                dateTimePicker4.Value = new DateTime(krajnjiDatum.Year, krajnjiDatum.Month, krajnjiDatum.Day);

                string serijskiBroj = GridAlarmniSistem[5, selectedIndex].Value.ToString();
                textBox2.Text = serijskiBroj;

                string proizvodjac = GridAlarmniSistem[6, selectedIndex].Value.ToString();
                textBox3.Text = proizvodjac;

                string model = GridAlarmniSistem[7, selectedIndex].Value.ToString();
                textBox4.Text = model;

                string godinaProizvodnje = GridAlarmniSistem[8, selectedIndex].Value.ToString();
                textBox5.Text = godinaProizvodnje;


                OsveziGrid();
            }
            else

            {
                MessageBox.Show("Nije selektovana nijedna kolona.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Dodaj
        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox1.Visible = false;
        }

        void OsveziGrid()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from ALARMNI_SISTEM", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridAlarmniSistem.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OsveziGridToplote()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from DETEKCIJA_TOPLOTNOG_ODRAZA", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridAlarmniSistem.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OsveziGridPokrata()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from DETEKCIJA_POKRETA", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridAlarmniSistem.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } void OsveziGridUltrazvucni()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from ULTRAZVUCNI", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridAlarmniSistem.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Zatvori
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Potvrdi dodaju
        private void button5_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
            
                detekcijaToplote();
                OsveziGridToplote();
            }
            else
                if (radioButton2.Checked == true)
            {
              
                detekcijaPokreta();
                OsveziGridPokrata();
            }
            else
                if (radioButton3.Checked == true)
            {
                
                ultra();
                OsveziGridUltrazvucni();
            }
            else
                MessageBox.Show("nmze");

        }
        void dodajAlarm()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Entiteti.AlarmniSistem a = new Entiteti.AlarmniSistem()
                {
                    SerijskiBroj = int.Parse(textBox9.Text),
                    DatumAtesta = new DateTime(dateTimePicker8.Value.Year, dateTimePicker8.Value.Month, dateTimePicker8.Value.Day),
                    DatumInstalacije = new DateTime(dateTimePicker7.Value.Year, dateTimePicker7.Value.Month, dateTimePicker7.Value.Day),
                    PocetniDatum = new DateTime(dateTimePicker6.Value.Year, dateTimePicker6.Value.Month, dateTimePicker6.Value.Day),
                    KrajnjiDatum = new DateTime(dateTimePicker5.Value.Year, dateTimePicker5.Value.Month, dateTimePicker5.Value.Day),
                    Opis = textBox10.Text,
                    Proizvodjac = textBox8.Text,
                    Model = textBox7.Text,
                    GodinaProizvodnje = int.Parse(textBox6.Text)
                };
                s.Save(a);
                s.Flush();
                s.Close();
                MessageBox.Show("alarm" + a.Opis + " dodat.");
                OsveziGrid();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        void ultra()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Entiteti.Ultrazvucni a = new Entiteti.Ultrazvucni()
                {
                    SerijskiBroj = int.Parse(textBox9.Text),
                    DatumAtesta = new DateTime(dateTimePicker8.Value.Year, dateTimePicker8.Value.Month, dateTimePicker8.Value.Day),
                    DatumInstalacije = new DateTime(dateTimePicker7.Value.Year, dateTimePicker7.Value.Month, dateTimePicker7.Value.Day),
                    PocetniDatum = new DateTime(dateTimePicker6.Value.Year, dateTimePicker6.Value.Month, dateTimePicker6.Value.Day),
                    KrajnjiDatum = new DateTime(dateTimePicker5.Value.Year, dateTimePicker5.Value.Month, dateTimePicker5.Value.Day),
                    Opis = textBox10.Text,
                    Proizvodjac = textBox8.Text,
                    Model = textBox7.Text,
                    OpsegFrekvencija = int.Parse(textBox11.Text),
                    GodinaProizvodnje = int.Parse(textBox6.Text)

                };
                s.Save(a);
                s.Flush();
                s.Close();
                MessageBox.Show("Osoba " + a.Opis + " dodata.");
                OsveziGrid();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                textBox13.Clear();
                textBox14.Clear();
                textBox11.Clear();

            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        void detekcijaToplote()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Entiteti.DetekcijaToplotnogOdraza a = new Entiteti.DetekcijaToplotnogOdraza()
                {
                    SerijskiBroj = int.Parse(textBox9.Text),
                    DatumAtesta = new DateTime(dateTimePicker8.Value.Year, dateTimePicker8.Value.Month, dateTimePicker8.Value.Day),
                    DatumInstalacije = new DateTime(dateTimePicker7.Value.Year, dateTimePicker7.Value.Month, dateTimePicker7.Value.Day),
                    PocetniDatum = new DateTime(dateTimePicker6.Value.Year, dateTimePicker6.Value.Month, dateTimePicker6.Value.Day),
                    KrajnjiDatum = new DateTime(dateTimePicker5.Value.Year, dateTimePicker5.Value.Month, dateTimePicker5.Value.Day),
                    Opis = textBox10.Text,
                    Proizvodjac = textBox8.Text,
                    Model = textBox7.Text,
                    VertikalnaRezolucija = int.Parse(textBox14.Text),
                    HorizontalnaRezolucija = int.Parse(textBox13.Text),
                    GodinaProizvodnje = int.Parse(textBox6.Text)

                };
                s.Save(a);
                s.Flush();
                s.Close();
                MessageBox.Show("Osoba " + a.Opis + " dodata.");
                OsveziGrid();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                textBox13.Clear();
                textBox14.Clear();

            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }

        void detekcijaPokreta()
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Entiteti.DetekcijaPokreta a = new Entiteti.DetekcijaPokreta()
                {
                    SerijskiBroj = int.Parse(textBox9.Text),
                    DatumAtesta = new DateTime(dateTimePicker8.Value.Year, dateTimePicker8.Value.Month, dateTimePicker8.Value.Day),
                    DatumInstalacije = new DateTime(dateTimePicker7.Value.Year, dateTimePicker7.Value.Month, dateTimePicker7.Value.Day),
                    PocetniDatum = new DateTime(dateTimePicker6.Value.Year, dateTimePicker6.Value.Month, dateTimePicker6.Value.Day),
                    KrajnjiDatum = new DateTime(dateTimePicker5.Value.Year, dateTimePicker5.Value.Month, dateTimePicker5.Value.Day),
                    Opis = textBox10.Text,
                    Proizvodjac = textBox8.Text,
                    Model = textBox7.Text,
                    Osetljivost = int.Parse(textBox12.Text),
                    GodinaProizvodnje = int.Parse(textBox6.Text)

                };
                s.Save(a);
                s.Flush();
                s.Close();
                MessageBox.Show("Osoba " + a.Opis + " dodata.");
                OsveziGrid();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                textBox13.Clear();
                textBox14.Clear();
                textBox12.Clear();

            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        //Brisanje
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.GridAlarmniSistem.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridAlarmniSistem.SelectedRows[0].Index;

                int rowID = int.Parse(GridAlarmniSistem[5, selectedIndex].Value.ToString());
                try
                {
                    ISession s = DataLayer.GetSession();

                    Entiteti.AlarmniSistem u = s.Load<Entiteti.AlarmniSistem>(rowID);

                    //brise se objekat iz baze ali ne i instanca objekta u memroiji
                    s.Delete(u);
                    //s.Delete("from Odeljenje");

                    s.Flush();
                    s.Close();
                    GridAlarmniSistem.Rows.RemoveAt(selectedIndex);
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
                izmenitoplotni();
            else
                if (radioButton4.Checked == true)
                izmenipokret();
            else
                if (radioButton5.Checked == true)
                izmeniultra();
            else
                MessageBox.Show("nije nista stiklirano");
        }
        void izmenitoplotni()
        {
            try
            {
                ISession s = DataLayer.GetSession();

                int serijskiBroj = int.Parse(textBox2.Text);
                Entiteti.DetekcijaToplotnogOdraza a = s.Load<Entiteti.DetekcijaToplotnogOdraza>(serijskiBroj);

                a.Opis = textBox1.Text;
                a.DatumAtesta = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
                a.DatumInstalacije = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
                a.PocetniDatum = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day);
                a.KrajnjiDatum = new DateTime(dateTimePicker4.Value.Year, dateTimePicker4.Value.Month, dateTimePicker4.Value.Day);
                a.Proizvodjac = textBox3.Text;
                a.Model = textBox4.Text;
                a.GodinaProizvodnje = int.Parse(textBox5.Text);
                a.VertikalnaRezolucija = int.Parse(textBox18.Text);
                a.HorizontalnaRezolucija = int.Parse(textBox17.Text);

                s.Update(a);
                s.Flush();
                s.Close();
                OsveziGrid();

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
        void izmenipokret()
        {
            try
            {
                ISession s = DataLayer.GetSession();

                int serijskiBroj = int.Parse(textBox2.Text);
                Entiteti.DetekcijaPokreta a = s.Load<Entiteti.DetekcijaPokreta>(serijskiBroj);

                a.Opis = textBox1.Text;
                a.DatumAtesta = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
                a.DatumInstalacije = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
                a.PocetniDatum = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day);
                a.KrajnjiDatum = new DateTime(dateTimePicker4.Value.Year, dateTimePicker4.Value.Month, dateTimePicker4.Value.Day);
                a.Proizvodjac = textBox3.Text;
                a.Model = textBox4.Text;
                a.GodinaProizvodnje = int.Parse(textBox5.Text);
                a.Osetljivost = int.Parse(textBox16.Text);

                s.Update(a);
                s.Flush();
                s.Close();
                OsveziGrid();

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
        void izmeniultra()
        {
            try
            {
                ISession s = DataLayer.GetSession();

                int serijskiBroj = int.Parse(textBox2.Text);
                Entiteti.Ultrazvucni a = s.Load<Entiteti.Ultrazvucni>(serijskiBroj);

                a.Opis = textBox1.Text;
                a.DatumAtesta = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);
                a.DatumInstalacije = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day);
                a.PocetniDatum = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day);
                a.KrajnjiDatum = new DateTime(dateTimePicker4.Value.Year, dateTimePicker4.Value.Month, dateTimePicker4.Value.Day);
                a.Proizvodjac = textBox3.Text;
                a.Model = textBox4.Text;
                a.GodinaProizvodnje = int.Parse(textBox5.Text);
                a.OpsegFrekvencija = int.Parse(textBox15.Text);

                s.Update(a);
                s.Flush();
                s.Close();
                OsveziGrid();

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
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        { if (radioButton1.Checked == true || radioButton6.Checked == true)
                OsveziGridToplote();
            else
                if (radioButton2.Checked == true || radioButton4.Checked == true)
                OsveziGridPokrata();
            else
                if (radioButton3.Checked == true || radioButton5.Checked == true)
                OsveziGridUltrazvucni();
        else
                OsveziGrid();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox11.Enabled = true;
            textBox12.Enabled = false;
            textBox14.Enabled = false;
            textBox13.Enabled = false;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox12.Enabled = false;
            textBox11.Enabled = false;
            textBox14.Enabled = true;
            textBox13.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox12.Enabled = true;
            textBox11.Enabled = false;
            textBox14.Enabled = false;
            textBox13.Enabled = false;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
