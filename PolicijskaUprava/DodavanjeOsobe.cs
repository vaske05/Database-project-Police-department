//using Oracle.DataAccess.Client;
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
    public partial class DodavanjeOsobe : Form
    {
        public DodavanjeOsobe()
        {
            InitializeComponent();
            OsveziGrid();
        }

        //Osvezi
        private void button1_Click(object sender, EventArgs e)
        {
            OsveziGrid();
        }

        //Brisanje
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.GridOsoba.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridOsoba.SelectedRows[0].Index;

                int rowID = int.Parse(GridOsoba[0, selectedIndex].Value.ToString());
                try
                {
                    ISession s = DataLayer.GetSession();

                    Entiteti.Osoba u = s.Load<Entiteti.Osoba>(rowID);

                    //brise se objekat iz baze ali ne i instanca objekta u memroiji
                    s.Delete(u);
                    //s.Delete("from Odeljenje");

                    s.Flush();
                    s.Close();
                    GridOsoba.Rows.RemoveAt(selectedIndex);
                    MessageBox.Show("Osoba " + u.Ime + " izbrisana!");
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

        //Dodaj
        private void button2_Click(object sender, EventArgs e)
        {
            groupBoxDodaj.Visible = true;
            groupBoxIzmeni.Visible = false;
        }

        //Zatvori
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        //Izmeni
        private void button3_Click(object sender, EventArgs e)
        {
            groupBoxIzmeni.Visible = true;
            textBox15.Enabled = false;
            comboBox2.Enabled = false;

            if (this.GridOsoba.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridOsoba.SelectedRows[0].Index;

                string jmbg = GridOsoba[0, selectedIndex].Value.ToString();
                textBox15.Text = jmbg;

                string ime = GridOsoba[1, selectedIndex].Value.ToString();
                textBox16.Text = ime;

                string prezime = GridOsoba[2, selectedIndex].Value.ToString();
                textBox14.Text = prezime;

                string imeRoditelja = GridOsoba[3, selectedIndex].Value.ToString();
                textBox13.Text = imeRoditelja;

                string pol = GridOsoba[4, selectedIndex].Value.ToString();
                if (pol == "M")
                    radioButton4.Checked = true;
                else
                    radioButton3.Checked = true;

                string nazivStanice = GridOsoba[5, selectedIndex].Value.ToString();
                textBox12.Text = nazivStanice;

                DateTime datumPrijema = DateTime.Parse(GridOsoba[6, selectedIndex].Value.ToString());
                dateTimePicker6.Value = new DateTime(datumPrijema.Year,datumPrijema.Month,datumPrijema.Day);

                string adresa = GridOsoba[7, selectedIndex].Value.ToString();
                textBox11.Text = adresa;

                string nazivSkole = GridOsoba[8, selectedIndex].Value.ToString();
                textBox10.Text = nazivSkole;

                DateTime datumZavrsetka = DateTime.Parse(GridOsoba[9, selectedIndex].Value.ToString());
                dateTimePicker5.Value = new DateTime(datumZavrsetka.Year, datumZavrsetka.Month, datumZavrsetka.Day);

                string cin = GridOsoba[10, selectedIndex].Value.ToString();
                textBox9.Text = cin;

                DateTime datumSticanja = DateTime.Parse(GridOsoba[11, selectedIndex].Value.ToString());
                dateTimePicker4.Value = new DateTime(datumSticanja.Year, datumSticanja.Month, datumSticanja.Day);

                string pom = GridOsoba[12, selectedIndex].Value.ToString();
                int uprava;
                uprava = int.Parse(pom);
                if (uprava == 50)
                    comboBox2.Text = "Beograd";
                else if (uprava == 40)
                    comboBox2.Text = "Kragujevac";
                else if (uprava == 30)
                    comboBox2.Text = "Novi Sad";
                else if (uprava == 20)
                    comboBox2.Text = "Niš";
                else
                    comboBox2.Text = "Trstenik";

                OsveziGrid();
            }
            else
            {
                MessageBox.Show("Nije selektovana nijedna kolona.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        void dodajOsobu()
        {
            string polOsobe;
            int idUprave = 0;
            if (radioButton1.Checked == true)
            {
                polOsobe = "M";
            }
            else
            {
                polOsobe = "Ž";
            }

            if (comboBox1.Text == "Trstenik")
                idUprave = 10;
            else if (comboBox1.Text == "Niš")
                idUprave = 20;
            else if (comboBox1.Text == "Novi Sad")
                idUprave = 30;
            else if (comboBox1.Text == "Beograd")
                idUprave = 50;
            else if (comboBox1.Text == "Kragujevac")
                idUprave = 40;

            try
            {
                ISession s = DataLayer.GetSession();

                Entiteti.Osoba o = new Entiteti.Osoba()
                {
                    Jmbg = int.Parse(textBox1.Text),
                    Ime = textBox2.Text,
                    Prezime = textBox3.Text,
                    ImeRoditelja = textBox4.Text,
                    Pol = polOsobe,
                    NazivStanice = textBox5.Text,
                    DatumPrijema = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day),
                    Adresa = textBox6.Text,
                    NazivSkole = textBox7.Text,
                    DatumZavrsetka = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day),
                    Cin = textBox8.Text,
                    DatumSticanja = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day),

                    ZaposljenUUprava = s.Load<Entiteti.Uprava>(idUprave)
                };

                s.Save(o);
                s.Flush();
                s.Close();
                MessageBox.Show("Osoba " + o.Ime + " dodata.");
                OsveziGrid();

            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        //Potvrdi dodavanje
        private void button6_Click(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
            {
                // dodajOsobu();

                dodajNacelnika();
                OsveziGridNacelnik();
            }
            else
                if (radioButton8.Checked == true)
            { 
                dodajzamenikaNacelnika();
                OsveziGridzaNacelnik();
            }
            else
                if (radioButton13.Checked == true)
            {
                dodajVanrednog();
                OsveziGridVanredni(); }
            else
                if (radioButton11.Checked == true)
            {
                dodajPozornik();
                OsveziGridPozornik(); }
            else
                if (radioButton7.Checked == true)
            {
                dodajSkolskog();
                OsveziGridSkolski();
            }
            else
                if (radioButton9.Checked == true)
            {
                dodajOstali();
                OsveziGridOstali();
            }
            else
                if (radioButton12.Checked == true)
            {
                dodajVodja();
                OsveziGridVodja();
            }
            else
                if (radioButton6.Checked == true)
            {
                dodajzamenikSefa();
                OsveziGridzamenikSefa(); }
            else
                if (radioButton10.Checked == true)
            {
                dodajSef();
                OsveziGridSef(); }
            else
                if (radioButton14.Checked == true)
            { OsveziGrid(); }
            else
                OsveziGrid();
        }

       
        //Potvrdi izmenu
        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                ISession s = DataLayer.GetSession();

                int jmbg = int.Parse(textBox15.Text);
                string ime = textBox16.Text;
                string prezime = textBox14.Text;
                string imeRoditelja = textBox13.Text;
                string pol;
                if (radioButton4.Checked == true)
                    pol = "M";
                else
                    pol = "Ž";
                string nazivStanice = textBox12.Text;
                DateTime datumPrijema = new DateTime(dateTimePicker6.Value.Year, dateTimePicker6.Value.Month, dateTimePicker6.Value.Day);
                string adresa = textBox11.Text;
                string nazivSkole = textBox10.Text;
                DateTime datumZavrsetka = new DateTime(dateTimePicker5.Value.Year, dateTimePicker5.Value.Month, dateTimePicker5.Value.Day);
                string cin = textBox9.Text;
                DateTime datumSticanja = new DateTime(dateTimePicker4.Value.Year, dateTimePicker4.Value.Month, dateTimePicker4.Value.Day);
                int idUprave2=0;

                if (comboBox2.Text == "Trstenik")
                    idUprave2 = 10;
                else if (comboBox2.Text == "Niš")
                    idUprave2 = 20;
                else if (comboBox2.Text == "Novi Sad")
                    idUprave2 = 30;
                else if (comboBox2.Text == "Beograd")
                    idUprave2 = 50;
                else if (comboBox2.Text == "Kragujevac")
                    idUprave2 = 40;

                Entiteti.Osoba o = s.Load<Entiteti.Osoba>(jmbg);
                o.Ime = ime;
                o.Prezime = prezime;
                o.ImeRoditelja = imeRoditelja;
                o.Pol = pol;
                o.NazivStanice = nazivStanice;
                o.DatumPrijema = new DateTime(datumPrijema.Year, datumPrijema.Month, datumPrijema.Day);
                o.Adresa = adresa;
                o.NazivSkole = nazivSkole;
                o.DatumZavrsetka = new DateTime(datumZavrsetka.Year, datumZavrsetka.Month, datumZavrsetka.Day);
                o.Cin = cin;
                o.DatumSticanja = new DateTime(datumSticanja.Year, datumSticanja.Month, datumSticanja.Day);

                o.ZaposljenUUprava.IdUprave = idUprave2;
                
                
                
                s.Update(o);
                s.Flush();
                s.Close();
                OsveziGrid();

                textBox15.Clear();
                textBox16.Clear();
                textBox14.Clear();
                textBox13.Clear();
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                textBox12.Clear();
                textBox11.Clear();
                textBox9.Clear();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }

        }
        void dodajNacelnika()
        {
            string polOsobe;
            if (radioButton1.Checked == true)
            {
                polOsobe = "M";
            }
            else
            {
                polOsobe = "Ž";
            }
            int idUprave = 0;
            if (comboBox1.Text == "Trstenik")
                idUprave = 10;
            else if (comboBox1.Text == "Niš")
                idUprave = 20;
            else if (comboBox1.Text == "Novi Sad")
                idUprave = 30;
            else if (comboBox1.Text == "Beograd")
                idUprave = 50;
            else if (comboBox1.Text == "Kragujevac")
                idUprave = 40;
            try {
                ISession s = DataLayer.GetSession();


                Entiteti.Nacelnik st = new Entiteti.Nacelnik()
                {
                    Jmbg = int.Parse(textBox1.Text),
                    Ime = textBox2.Text,
                    Prezime = textBox3.Text,
                    ImeRoditelja = textBox4.Text,
                    Pol = polOsobe,
                    NazivStanice = textBox5.Text,
                    DatumPrijema = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day),
                    Adresa = textBox6.Text,
                    NazivSkole = textBox7.Text,
                    DatumZavrsetka = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day),
                    Cin = textBox8.Text,
                    DatumSticanja = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day),

                    ZaposljenUUprava = s.Load<Entiteti.Uprava>(idUprave)

                  
                 
                    
                };

                s.Save(st);
                s.Flush();
                s.Close();
                MessageBox.Show("Naceknik " + st.Ime + " dodata.");
                OsveziGrid();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        void dodajzamenikaNacelnika()
        {
            string polOsobe;
            if (radioButton1.Checked == true)
            {
                polOsobe = "M";
            }
            else
            {
                polOsobe = "Ž";
            }
            int idUprave = 0;
            if (comboBox1.Text == "Trstenik")
                idUprave = 10;
            else if (comboBox1.Text == "Niš")
                idUprave = 20;
            else if (comboBox1.Text == "Novi Sad")
                idUprave = 30;
            else if (comboBox1.Text == "Beograd")
                idUprave = 50;
            else if (comboBox1.Text == "Kragujevac")
                idUprave = 40;
            try
            {
                ISession s = DataLayer.GetSession();


                Entiteti.ZamenikNacelnika st = new Entiteti.ZamenikNacelnika()
                {
                    Jmbg = int.Parse(textBox1.Text),
                    Ime = textBox2.Text,
                    Prezime = textBox3.Text,
                    ImeRoditelja = textBox4.Text,
                    Pol = polOsobe,
                    NazivStanice = textBox5.Text,
                    DatumPrijema = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day),
                    Adresa = textBox6.Text,
                    NazivSkole = textBox7.Text,
                    DatumZavrsetka = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day),
                    Cin = textBox8.Text,
                    DatumSticanja = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day),
                  
                    ZaposljenUUprava = s.Load<Entiteti.Uprava>(idUprave)
                    




                };

                s.Save(st);
                s.Flush();
                s.Close();
                MessageBox.Show("zamenik Naceknik " + st.Ime + " dodat.");
                OsveziGrid();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        void dodajVanrednog()
        {
            string polOsobe;
            if (radioButton1.Checked == true)
            {
                polOsobe = "M";
            }
            else
            {
                polOsobe = "Ž";
            }
            int idUprave = 0;
            if (comboBox1.Text == "Trstenik")
                idUprave = 10;
            else if (comboBox1.Text == "Niš")
                idUprave = 20;
            else if (comboBox1.Text == "Novi Sad")
                idUprave = 30;
            else if (comboBox1.Text == "Beograd")
                idUprave = 50;
            else if (comboBox1.Text == "Kragujevac")
                idUprave = 40;
            try
            {
                ISession s = DataLayer.GetSession();


                Entiteti.VanredniPolicajac st = new Entiteti.VanredniPolicajac()
                {
                    Jmbg = int.Parse(textBox1.Text),
                    Ime = textBox2.Text,
                    Prezime = textBox3.Text,
                    ImeRoditelja = textBox4.Text,
                    Pol = polOsobe,
                    NazivStanice = textBox5.Text,
                    DatumPrijema = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day),
                    Adresa = textBox6.Text,
                    NazivSkole = textBox7.Text,
                    DatumZavrsetka = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day),
                    Cin = textBox8.Text,
                    DatumSticanja = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day),

                    ZaposljenUUprava = s.Load<Entiteti.Uprava>(idUprave)





                };

                s.Save(st);
                s.Flush();
                s.Close();
                MessageBox.Show("Vanredni policajac" + st.Ime + " dodata.");
                OsveziGrid();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        void dodajPozornik()
        {
            string polOsobe;
            if (radioButton1.Checked == true)
            {
                polOsobe = "M";
            }
            else
            {
                polOsobe = "Ž";
            }
            int idUprave = 0;
            if (comboBox1.Text == "Trstenik")
                idUprave = 10;
            else if (comboBox1.Text == "Niš")
                idUprave = 20;
            else if (comboBox1.Text == "Novi Sad")
                idUprave = 30;
            else if (comboBox1.Text == "Beograd")
                idUprave = 50;
            else if (comboBox1.Text == "Kragujevac")
                idUprave = 40;
            try
            {
                ISession s = DataLayer.GetSession();


                Entiteti.Pozornik st = new Entiteti.Pozornik()
                {
                    Jmbg = int.Parse(textBox1.Text),
                    Ime = textBox2.Text,
                    Prezime = textBox3.Text,
                    ImeRoditelja = textBox4.Text,
                    Pol = polOsobe,
                    NazivStanice = textBox5.Text,
                    DatumPrijema = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day),
                    Adresa = textBox6.Text,
                    NazivSkole = textBox7.Text,
                    DatumZavrsetka = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day),
                    Cin = textBox8.Text,
                    DatumSticanja = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day),

                    ZaposljenUUprava = s.Load<Entiteti.Uprava>(idUprave)





                };

                s.Save(st);
                s.Flush();
                s.Close();
                MessageBox.Show("pozornik " + st.Ime + " dodata.");
                OsveziGrid();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        void dodajSkolskog()
        {
            string polOsobe;
            if (radioButton1.Checked == true)
            {
                polOsobe = "M";
            }
            else
            {
                polOsobe = "Ž";
            }
            int idUprave = 0;
            if (comboBox1.Text == "Trstenik")
                idUprave = 10;
            else if (comboBox1.Text == "Niš")
                idUprave = 20;
            else if (comboBox1.Text == "Novi Sad")
                idUprave = 30;
            else if (comboBox1.Text == "Beograd")
                idUprave = 50;
            else if (comboBox1.Text == "Kragujevac")
                idUprave = 40;
            try
            {
                ISession s = DataLayer.GetSession();


                Entiteti.SkolskiPolicajac st = new Entiteti.SkolskiPolicajac()
                {
                    Jmbg = int.Parse(textBox1.Text),
                    Ime = textBox2.Text,
                    Prezime = textBox3.Text,
                    ImeRoditelja = textBox4.Text,
                    Pol = polOsobe,
                    NazivStanice = textBox5.Text,
                    DatumPrijema = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day),
                    Adresa = textBox6.Text,
                    NazivSkole = textBox7.Text,
                    DatumZavrsetka = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day),
                    Cin = textBox8.Text,
                    DatumSticanja = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day),

                    ZaposljenUUprava = s.Load<Entiteti.Uprava>(idUprave)





                };

                s.Save(st);
                s.Flush();
                s.Close();
                MessageBox.Show("Skolski " + st.Ime + " dodata.");
                OsveziGrid();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        void dodajOstali()
        {
            string polOsobe;
            if (radioButton1.Checked == true)
            {
                polOsobe = "M";
            }
            else
            {
                polOsobe = "Ž";
            }
            int idUprave = 0;
            if (comboBox1.Text == "Trstenik")
                idUprave = 10;
            else if (comboBox1.Text == "Niš")
                idUprave = 20;
            else if (comboBox1.Text == "Novi Sad")
                idUprave = 30;
            else if (comboBox1.Text == "Beograd")
                idUprave = 50;
            else if (comboBox1.Text == "Kragujevac")
                idUprave = 40;
            try
            {
                ISession s = DataLayer.GetSession();


                Entiteti.OstaliPolicajci st = new Entiteti.OstaliPolicajci()
                {
                    Jmbg = int.Parse(textBox1.Text),
                    Ime = textBox2.Text,
                    Prezime = textBox3.Text,
                    ImeRoditelja = textBox4.Text,
                    Pol = polOsobe,
                    NazivStanice = textBox5.Text,
                    DatumPrijema = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day),
                    Adresa = textBox6.Text,
                    NazivSkole = textBox7.Text,
                    DatumZavrsetka = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day),
                    Cin = textBox8.Text,
                    DatumSticanja = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day),

                  
                    ZaposljenUUprava = s.Load<Entiteti.Uprava>(idUprave)
                    




                };

                s.Save(st);
                s.Flush();
                s.Close();
                MessageBox.Show("Ostali policajci " + st.Ime + " dodata.");
                OsveziGrid();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        void dodajVodja()
        {
            string polOsobe;
            if (radioButton1.Checked == true)
            {
                polOsobe = "M";
            }
            else
            {
                polOsobe = "Ž";
            }
            int idUprave = 0;
            if (comboBox1.Text == "Trstenik")
                idUprave = 10;
            else if (comboBox1.Text == "Niš")
                idUprave = 20;
            else if (comboBox1.Text == "Novi Sad")
                idUprave = 30;
            else if (comboBox1.Text == "Beograd")
                idUprave = 50;
            else if (comboBox1.Text == "Kragujevac")
                idUprave = 40;
            try
            {
                ISession s = DataLayer.GetSession();


                Entiteti.Vodja st = new Entiteti.Vodja()
                {
                    Jmbg = int.Parse(textBox1.Text),
                    Ime = textBox2.Text,
                    Prezime = textBox3.Text,
                    ImeRoditelja = textBox4.Text,
                    Pol = polOsobe,
                    NazivStanice = textBox5.Text,
                    DatumPrijema = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day),
                    Adresa = textBox6.Text,
                    NazivSkole = textBox7.Text,
                    DatumZavrsetka = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day),
                    Cin = textBox8.Text,
                    DatumSticanja = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day),

                    ZaposljenUUprava = s.Load<Entiteti.Uprava>(idUprave)





                };

                s.Save(st);
                s.Flush();
                s.Close();
                MessageBox.Show("Vodja " + st.Ime + " dodata.");
                OsveziGrid();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        void dodajzamenikSefa()
        {
            string polOsobe;
            if (radioButton1.Checked == true)
            {
                polOsobe = "M";
            }
            else
            {
                polOsobe = "Ž";
            }
            int idUprave = 0;
            if (comboBox1.Text == "Trstenik")
                idUprave = 10;
            else if (comboBox1.Text == "Niš")
                idUprave = 20;
            else if (comboBox1.Text == "Novi Sad")
                idUprave = 30;
            else if (comboBox1.Text == "Beograd")
                idUprave = 50;
            else if (comboBox1.Text == "Kragujevac")
                idUprave = 40;
            try
            {
                ISession s = DataLayer.GetSession();


                Entiteti.ZamenikSefa st = new Entiteti.ZamenikSefa()
                {
                    Jmbg = int.Parse(textBox1.Text),
                    Ime = textBox2.Text,
                    Prezime = textBox3.Text,
                    ImeRoditelja = textBox4.Text,
                    Pol = polOsobe,
                    NazivStanice = textBox5.Text,
                    DatumPrijema = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day),
                    Adresa = textBox6.Text,
                    NazivSkole = textBox7.Text,
                    DatumZavrsetka = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day),
                    Cin = textBox8.Text,
                    DatumSticanja = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day),

                    ZaposljenUUprava = s.Load<Entiteti.Uprava>(idUprave)





                };

                s.Save(st);
                s.Flush();
                s.Close();
                MessageBox.Show("Zamenik sefa " + st.Ime + " dodata.");
                OsveziGrid();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        void dodajSef()
        {
            string polOsobe;
            if (radioButton1.Checked == true)
            {
                polOsobe = "M";
            }
            else
            {
                polOsobe = "Ž";
            }
            int idUprave = 0;
            if (comboBox1.Text == "Trstenik")
                idUprave = 10;
            else if (comboBox1.Text == "Niš")
                idUprave = 20;
            else if (comboBox1.Text == "Novi Sad")
                idUprave = 30;
            else if (comboBox1.Text == "Beograd")
                idUprave = 50;
            else if (comboBox1.Text == "Kragujevac")
                idUprave = 40;
            try
            {
                ISession s = DataLayer.GetSession();


                Entiteti.Sef st= new Entiteti.Sef()
                {
                    Jmbg = int.Parse(textBox1.Text),
                    Ime = textBox2.Text,
                    Prezime = textBox3.Text,
                    ImeRoditelja = textBox4.Text,
                    Pol = polOsobe,
                    NazivStanice = textBox5.Text,
                    DatumPrijema = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day),
                    Adresa = textBox6.Text,
                    NazivSkole = textBox7.Text,
                    DatumZavrsetka = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day),
                    Cin = textBox8.Text,
                    DatumSticanja = new DateTime(dateTimePicker3.Value.Year, dateTimePicker3.Value.Month, dateTimePicker3.Value.Day),
                    
                    ZaposljenUUprava = s.Load<Entiteti.Uprava>(idUprave)





                };

                s.Save(st);
                s.Flush();
                s.Close();
                MessageBox.Show("Sef " + st.Ime + " dodata.");
                OsveziGrid();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
        void OsveziGrid()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from OSOBA", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridOsoba.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OsveziGridNacelnik()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from NACELNIK", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridOsoba.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DodavanjeOsobe_Load(object sender, EventArgs e)
        {

        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

        }
        void OsveziGridzaNacelnik()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from ZAMENIK_NACELNIKA", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridOsoba.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OsveziGridVanredni()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from VANREDNI_POLICAJAC", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridOsoba.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OsveziGridPozornik()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from POZORNIK", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridOsoba.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OsveziGridSkolski()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from SKOLSKI_POLICAJAC", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridOsoba.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OsveziGridOstali()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from OSTALI_POLICAJCI", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridOsoba.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OsveziGridVodja()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from VODJA", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridOsoba.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OsveziGridzamenikSefa()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from ZAMENIK_SEFA", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridOsoba.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OsveziGridSef()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=" + LogIn.Username + ";Password=" + LogIn.Password))
                using (OracleCommand cmd = new OracleCommand("select * from SEF", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridOsoba.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

