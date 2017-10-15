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
using PolicijskaUprava.Entiteti;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.EntityFramework;
namespace PolicijskaUprava
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                
                PolicijskaUprava.Entiteti.Uprava p1 = s.Load<PolicijskaUprava.Entiteti.Uprava>(10);
                PolicijskaUprava.Entiteti.SkolskiPolicajac p2 = s.Load<PolicijskaUprava.Entiteti.SkolskiPolicajac>(121);

                //MessageBox.Show(p2.NazivSkole);
                MessageBox.Show(p2.Adresa);
                MessageBox.Show(p1.Grad);
                s.Close();
            }
            catch (Exception ec)
            {

                MessageBox.Show(ec.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LogIn.Username == null && LogIn.Password == null)
            {
                MessageBox.Show("You are not logged in.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DodavanjeOsobe f1 = new DodavanjeOsobe();
                f1.Show();
            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (LogIn.Username == null && LogIn.Password == null)
            {
                MessageBox.Show("You are not logged in.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DodavanjeUprave f1 = new DodavanjeUprave();
                f1.Show();
            }
        }
   

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(LogIn.Username);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            LogInForm Log = new LogInForm();
            Log.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (LogIn.Username == null && LogIn.Password == null)
            {
                MessageBox.Show("You are not logged in.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DodavanjeStanice f1 = new DodavanjeStanice();
                f1.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (LogIn.Username == null && LogIn.Password == null)
            {
                MessageBox.Show("You are not logged in.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DodavanjeAlarma f1 = new DodavanjeAlarma();
                f1.Show();
            }
        }
        //objekat
        private void button8_Click(object sender, EventArgs e)
        {
            if (LogIn.Username == null && LogIn.Password == null)
            {
                MessageBox.Show("You are not logged in.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DodavanjeObjekta f1 = new DodavanjeObjekta();
                f1.Show();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (LogIn.Username == null && LogIn.Password == null)
            {
                MessageBox.Show("You are not logged in.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DodavvanjeSkole f1 = new DodavvanjeSkole();
                f1.Show();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (LogIn.Username == null && LogIn.Password == null)
            {
                MessageBox.Show("You are not logged in.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DodavanjeIntervencije f1 = new DodavanjeIntervencije();
                f1.Show();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {

            if (LogIn.Username == null && LogIn.Password == null)
            {
                MessageBox.Show("You are not logged in.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DodavanjeVozila f1 = new DodavanjeVozila();
                f1.Show();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (LogIn.Username == null && LogIn.Password == null)
            {
                MessageBox.Show("You are not logged in.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
               DodavanjeSektora f1 = new DodavanjeSektora();
                f1.Show();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (LogIn.Username == null && LogIn.Password == null)
            {
                MessageBox.Show("You are not logged in.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DodavanjePatrole f1 = new DodavanjePatrole();
                f1.Show();
            }
        }
    }
}




/*
                Objekat o = new Objekat();
                o.IdObjekta = 9;

                ISession s = DataLayer.GetSession();

                AlarmniSistem a = new AlarmniSistem();
                a.DatumAtesta = new DateTime(1988, 11, 05);
                a.Model = "A110";
                a.Opis = "Nema";
                a.Proizvodjac = "Motorola";
                a.SerijskiBroj = 5033;
                a.PocetniDatum = new DateTime(1988, 11, 05);
                a.KrajnjiDatum = new DateTime(1988, 11, 05);
                a.GodinaProizvodnje = 1922;
                a.InstaliranJeObjekat.Add(o);

                s.Save(a);
                s.SaveOrUpdate(a);

                s.Flush();
                s.Close();
                */
