using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;

namespace fauxTPICarEcoleIncompetente
{
    public partial class login : Form
    {
        MySQLConnect db = new MySQLConnect();
        public login()
        {
            InitializeComponent();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            string email = tbxEmail.Text;
            string password = Hash(tbxPassword.Text);
            if (db.CheckLogin(email, password))
            {
                btnConnexion.Text = "ya";
            }
        }
        static string Hash(string input)
        {
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (tbxRegisterPassword.Text == tbxRegisterPasswordV.Text)
            {
                db.Register(tbxRegisterEmail.Text, Hash(tbxRegisterPassword.Text));
                tbxRegisterEmail.Text = "";
                tbxRegisterPassword.Text = "";
                tbxRegisterPasswordV.Text = "";
            }
            else
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.");
            }
        }

    }
}
