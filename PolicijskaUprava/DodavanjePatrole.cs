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
    public partial class DodavanjePatrole : Form
    {
        public DodavanjePatrole()
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
                    using (OracleCommand cmd = new OracleCommand("select * from PATROLA", conn))
                    {
                        conn.Open();
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            GridPatrola.DataSource = dataTable;

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.GridPatrola.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridPatrola.SelectedRows[0].Index;

                int rowID = int.Parse(GridPatrola[0, selectedIndex].Value.ToString());
                try
                {
                    ISession s = DataLayer.GetSession();

                    Entiteti.Patrola u = s.Load<Entiteti.Patrola>(rowID);

                    //brise se objekat iz baze ali ne i instanca objekta u memroiji
                    s.Delete(u);
                    //s.Delete("from Odeljenje");

                    s.Flush();
                    s.Close();
                    GridPatrola.Rows.RemoveAt(selectedIndex);
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

        private void buttonIzmeni_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            //groupBox1.Visible = false;

            if (this.GridPatrola.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridPatrola.SelectedRows[0].Index;

                string registracijaVozila = GridPatrola[1, selectedIndex].Value.ToString();
                textBox1.Text = registracijaVozila;

                string idPatrole = GridPatrola[0, selectedIndex].Value.ToString();
                textBox2.Text = idPatrole;

               

                OsveziGrid();


            }
            else
            {
                MessageBox.Show("Nije selektovana nijedna kolona.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ISession s = DataLayer.GetSession();

           
            int registraciaVozila = int.Parse(textBox3.Text);

            Entiteti.Patrola st = new Entiteti.Patrola()
            {
                IdPatrole = int.Parse(textBox10.Text),


                ZaduzujeVozilo = s.Load<Entiteti.Vozilo>(registraciaVozila),
            };

            s.Save(st);
            s.Flush();
            s.Close();
            MessageBox.Show("Patrola " + st.IdPatrole + " dodata.");
            OsveziGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Entiteti.Patrola st = s.Load<Entiteti.Patrola>(int.Parse(textBox2.Text));
               
               

                st.ZaduzujeVozilo.Registracija = int.Parse(textBox1.Text);
                
                s.Update(st);
                s.Flush();
                s.Close();
                OsveziGrid();

             
                textBox2.Clear();
                textBox1.Clear();
               
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
    }
    }
    

