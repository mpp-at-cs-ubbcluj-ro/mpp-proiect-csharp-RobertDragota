using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab3.Service;

namespace Lab3
{
    public partial class Form1 : Form
    {
        private readonly ServiceController _serviceController;
        private readonly App _app;
        private readonly Register _register;
        private readonly Form1 _instance;
        public Form1(ServiceController serviceController)
        {
            this._serviceController = serviceController;
            this._app = new App(serviceController);
            this._register= new Register(serviceController);
            _instance= this;
            InitializeComponent();
        }

        
        private void label1_Click(object sender, EventArgs e)
        {
            return ;
        }


        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UserName.Text;
            string password = Password.Text;
            var account = _serviceController.getAccountService().findByUsername(username);
            if (account!=null && account!.Password.Equals(password))
            {
                _app.SetAccount(account);
                _app.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
         
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            _register.Show();
      
        }

        private void UserName_TextChanged(object sender, EventArgs e)
        {
            return ;
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            return ;
        }


        private void Login_Load(object sender, EventArgs e)
        {
            return ;
        }
    }
}