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
    public partial class DodavanjeUprave : Form
    {
        public DodavanjeUprave()
        {
            InitializeComponent();
            OsveziGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Entiteti.Uprava p = new Entiteti.Uprava()
                {
                    IdUprave = int.Parse(textBox1.Text),
                    Grad = textBox2.Text
                };

                s.Save(p);
                s.Flush();
                s.Close();
                //MessageBox.Show("Uprava " + p.Grad + " dodata.");
                textBox1.Clear();
                textBox2.Clear();
                OsveziGrid();
            }

            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DodavanjeUprave_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pOLICIJSKAUPRAVABindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        //Osvezi
        private void button3_Click(object sender, EventArgs e)
        {
            OsveziGrid();
        }

        //Izbrisi
        private void button4_Click(object sender, EventArgs e)
        {
            if(this.GridUprava.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridUprava.SelectedRows[0].Index;
                
                int rowID = int.Parse(GridUprava[1,selectedIndex].Value.ToString());
                try
                {
                    ISession s = DataLayer.GetSession();

                    Entiteti.Uprava u = s.Load<Entiteti.Uprava>(rowID);

                    //brise se objekat iz baze ali ne i instanca objekta u memroiji
                    s.Delete(u);
                    //s.Delete("from Odeljenje");

                    s.Flush();
                    s.Close();
                    GridUprava.Rows.RemoveAt(selectedIndex);
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
        private void button6_Click(object sender, EventArgs e)
        {
            
            groupBox1.Visible = true;
            groupBox2.Visible = false;

        }

        //Izmeni
        private void button5_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;


            if (this.GridUprava.SelectedRows.Count > 0)
            {
                int selectedIndex = this.GridUprava.SelectedRows[0].Index;

                string idUprave = GridUprava[1, selectedIndex].Value.ToString();
                string grad = GridUprava[0, selectedIndex].Value.ToString();
                textBox3.Text = idUprave;
                textBox4.Text = grad;
            }
            else
            {
                MessageBox.Show("Nije selektovana nijedna kolona.", "Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                int idUprave = int.Parse(textBox3.Text);
                string grad = textBox4.Text;

                Entiteti.Uprava u = s.Load<Entiteti.Uprava>(idUprave);                
                u.Grad = grad;
                s.Update(u);
                s.Flush();
                s.Close();
                textBox3.Clear();
                textBox4.Clear();
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
                using (OracleCommand cmd = new OracleCommand("select * from POLICIJSKA_UPRAVA", conn))
                {
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        GridUprava.DataSource = dataTable;

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
