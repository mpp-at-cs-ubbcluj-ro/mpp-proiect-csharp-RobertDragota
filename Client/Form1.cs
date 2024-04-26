using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppDomain.Domain;
using AppServices.Service;

namespace Client
{
    public partial class Form1 : Form
    {
        private readonly ServiceAppInterface _serviceController;
        private  App _app;
        private  Register _register;
        private readonly Form1 _instance;
        public Form1(ServiceAppInterface serviceController)
        {
            this._serviceController = serviceController;
            
           
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
            var account = _serviceController.FindAccount(username, password);
            try
            {
                this._app = new App(_serviceController);
                if (_serviceController.Login(account, _app))
                {
                    _app.SetAccount(account);
                    _app.Show();
                }
               
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
         
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            this._register= new Register(_serviceController);
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