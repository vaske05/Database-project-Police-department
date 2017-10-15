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
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using PolicijskaUprava.Mapiranja;
using PolicijskaUprava.Entiteti;
using Oracle.ManagedDataAccess.EntityFramework;
using Oracle.ManagedDataAccess.Client;
namespace PolicijskaUprava
{


    public partial class LogInForm : Form
    {


        public LogInForm()
        {
            InitializeComponent();
            textBox1.Text = "S14785";
            textBox2.Text = "S14785";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter your username.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Clear();
                textBox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter your Password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Clear();
                textBox2.Focus();
                return;
            }

            if(textBox1.Text == "S14785" && textBox2.Text == "S14785")
            {
                
                LogIn.Username = textBox1.Text;
                LogIn.Password = textBox2.Text;
                //ISession s = DataLayer.GetSession();
                MessageBox.Show("You have been successfully logged in.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
                
                
                
            }
            else
            {

            MessageBox.Show("Wrong username or password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBox2.Clear();
            textBox1.Clear();
            textBox1.Focus();
            return;
            }

        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            
        }

        private void LogInForm_Load_1(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }
    }
}
