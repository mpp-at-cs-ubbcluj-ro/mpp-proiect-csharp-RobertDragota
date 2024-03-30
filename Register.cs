using System;
using System.Windows.Forms;
using Lab3.Domain;
using Lab3.Service;

namespace Lab3;

public partial class Register : Form
{
    private readonly ServiceController _serviceController;

    public Register(ServiceController serviceController)
    {
        this._serviceController = serviceController;
        InitializeComponent();
    }

    private void label2_Click(object sender, EventArgs e)
    {
        return;
    }

    private void label1_Click(object sender, EventArgs e)
    {
        return;
    }

    private void RegisterButton_Click(object sender, EventArgs e)
    {
        try
        {
            string username = UserNameText.Text;
            string password = PasswordText.Text;
            if (username.Length == 0 || password.Length == 0)
                throw new Exception("Username or password cannot be empty");
            var account = _serviceController.getAccountService().findByUsername(username);
            if (account != null)
                throw new Exception("Username already exists");
            
            account = new Account(username, password);
            _serviceController.getAccountService().Save(account);
            MessageBox.Show("Account created");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

    private void PasswordText_TextChanged(object sender, EventArgs e)
    {
        return;
    }

    private void UserNameText_TextChanged(object sender, EventArgs e)
    {
        return;
    }

    private void Register_Load(object sender, EventArgs e)
    {
        return;
    }
}