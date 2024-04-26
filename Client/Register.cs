using System;
using System.Windows.Forms;
using AppDomain.Domain;
using AppServices.Service;

namespace Client;

public partial class Register : Form
{
    private readonly ServiceAppInterface _serviceController;

    public Register(ServiceAppInterface serviceController)
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
            _serviceController.MakeAccount(username, password);
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