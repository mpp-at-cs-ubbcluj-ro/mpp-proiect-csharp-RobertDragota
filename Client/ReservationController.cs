using System;
using System.ComponentModel;
using System.Windows.Forms;
using AppDomain.Domain;
using AppServices.Service;

namespace Client;

public partial class ReservationController : Form
{
    private Trip _trip;
    private ServiceAppInterface _serviceController;
    private Account _account;
    private App _app;
    public ReservationController(Trip trip, ServiceAppInterface serviceController, App app)
    {
        _trip = trip;
        _serviceController = serviceController;
        _app = app;
        InitializeComponent();
    }
    public void setAccount(Account account)
    {
        _account = account;
    }
   

    private void PhoneText_TextChanged(object sender, EventArgs e)
    {
        return;
    }

    private void TicketsText_TextChanged(object sender, EventArgs e)
    {
        return;
    }

    private void ReserveButton_Click(object sender, EventArgs e)
    {
        
        try
        {
            string clientName = ClientNameText.Text;
            string phone = PhoneText.Text;
            int tickets = Convert.ToInt32(TicketsText.Text);
            if (clientName.Length == 0 || phone.Length == 0)
                throw new Exception("Client name or phone cannot be empty");
            if (tickets <= 0)
                throw new Exception("Number of tickets must be positive");
            if (tickets > _trip.AvailableSeats)
                throw new Exception("Not enough available seats");
            // Reservation reservation = new Reservation(_account, phone, tickets, _trip, clientName);
            _serviceController.MakeReservation(_account,  clientName,  phone,  tickets,  _trip);
            
            MessageBox.Show("Reservation created");
           // _app.updateDataGrid();
            Close();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

    private void ClientNameText_TextChanged_1(object sender, EventArgs e)
    {
        return;
    }
}