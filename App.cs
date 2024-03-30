using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab3.Domain;
using Lab3.Service;

namespace Lab3;

public partial class App : Form
{
    readonly DataSet _dataSet = new DataSet();
    private readonly ServiceController _serviceController;
    private Account _account;
    private App instance;
    public App(ServiceController serviceController)
    {
        this._serviceController = serviceController;
        instance = this;
        InitializeComponent();
    }
    
    public void SetAccount(Account account)
    {
        _account = account;
    }

    private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            // Get the selected Trip from the row
            DataGridViewRow selectedRow = dataGridView.Rows[e.RowIndex];
        
            Trip selectedTrip = new Trip(
                Convert.ToString(selectedRow.Cells["Destination"].Value),
                Convert.ToString(selectedRow.Cells["TransportCompany"].Value),
                Convert.ToDouble(selectedRow.Cells["Price"].Value),
                Convert.ToInt32(selectedRow.Cells["AvailableSeats"].Value),
                Convert.ToDateTime(selectedRow.Cells["Date"].Value),
                Convert.ToDateTime(selectedRow.Cells["StartHour"].Value),
                Convert.ToDateTime(selectedRow.Cells["FinishHour"].Value)
            );

            // Assuming the Trip class has an Id property, set it as well
            selectedTrip.Id = Convert.ToInt64(selectedRow.Cells["Id"].Value);

            // Open the ReservationController form with the selected Trip
            ReservationController reservationController = new ReservationController(selectedTrip,_serviceController,instance);
            reservationController.setAccount(_account);
            reservationController.ShowDialog(); // Open as a modal dialog
        }
    }
 


    private void App_Load(object sender, EventArgs e)
    {
        // Retrieve all trips from the service.
        List<Trip> trips = _serviceController.getTripService().GetAll();

        // Convert the list of trips to a DataTable.
        DataTable tripsTable = ConvertToDataTable(trips);

        // Clear the existing DataSet.
        _dataSet.Clear();

        // Add the DataTable to the DataSet.
        _dataSet.Tables.Add(tripsTable);

        // Set the DataSource of the dataGridView to the DataTable within the DataSet.
        dataGridView.DataSource = _dataSet.Tables[0];
    }

    private DataTable ConvertToDataTable(List<Trip> trips)
    {
        // Create a new DataTable.
        DataTable table = new DataTable();

        // Define the columns in the DataTable to match the properties of the Trip object.
        table.Columns.Add("Id", typeof(long));
        table.Columns.Add("Destination", typeof(string));
        table.Columns.Add("TransportCompany", typeof(string));
        table.Columns.Add("Price", typeof(double));
        table.Columns.Add("AvailableSeats", typeof(int));
        table.Columns.Add("Date", typeof(DateTime));
        table.Columns.Add("StartHour", typeof(DateTime));
        table.Columns.Add("FinishHour", typeof(DateTime));

        // Populate the DataTable with Trip data.
        foreach (Trip trip in trips)
        {
            table.Rows.Add(trip.Id, trip.Destination, trip.TransportCompany, trip.Price, trip.AvailableSeats, trip.Date,
                trip.StartHour, trip.FinishHour);
        }

        return table;
    }

   

   private void SearchButton_Click(object sender, EventArgs e)
{
    // Check if any of the input fields are empty
    if (string.IsNullOrEmpty(DestinationText.Text) || string.IsNullOrEmpty(FromText.Text) || string.IsNullOrEmpty(ToText.Text))
    {
        MessageBox.Show("Please fill in all the fields.");
        return;
    }

    if (!int.TryParse(FromText.Text, out int from) || !int.TryParse(ToText.Text, out int to))
    {
        MessageBox.Show("Invalid hour format. Please enter numeric values.");
        return;
    }

    // Validate hour values
    if (from > to || from < 0 || from > 24 || to > 24)
    {
        MessageBox.Show("Invalid hours. The hours should be between 0 and 24, and 'From' hour should be less than 'To' hour.");
        return;
    }

    try
    {
        // Retrieve filtered trips from the service
        List<Trip> trips = _serviceController.getTripService().filterTrips(DestinationText.Text, from, to);
        
        if (trips.Count == 0)
        {
            MessageBox.Show("No trips found matching the criteria.");
            dataGridView.DataSource = null;  // Clear the DataGridView if no results found
        }
        else
        {
            // Convert the list of trips to a DataTable
            DataTable tripsTable = ConvertToDataTable(trips);

            // Reset DataSource
            dataGridView.DataSource = null;

            // Prepare the DataSet
            _dataSet.Clear();
            if (_dataSet.Tables.Count == 0)
            {
                _dataSet.Tables.Add(tripsTable);
            }
            else
            {
                _dataSet.Tables[0].Clear();
                foreach (DataRow row in tripsTable.Rows)
                {
                    _dataSet.Tables[0].ImportRow(row);
                }
            }

            // Update the DataGridView DataSource
            dataGridView.DataSource = _dataSet.Tables[0];
        }
    }
    
    
    catch (Exception ex)
    {
        MessageBox.Show($"An error occurred while searching for trips: {ex.Message}");
    }

    // Consider whether you still want to clear the input fields after search
    // DestinationText.Clear();
    // FromText.Clear();
    // ToText.Clear();
}

public void updateDataGrid()
{
    // Retrieve all trips from the service.
    List<Trip> trips = _serviceController.getTripService().GetAll();

    // Convert the list of trips to a DataTable.
    DataTable tripsTable = ConvertToDataTable(trips);

    // Reset DataSource
    dataGridView.DataSource = null;

    // Prepare the DataSet
    _dataSet.Clear();
    if (_dataSet.Tables.Count == 0)
    {
        _dataSet.Tables.Add(tripsTable);
    }
    else
    {
        _dataSet.Tables[0].Clear();
        foreach (DataRow row in tripsTable.Rows)
        {
            _dataSet.Tables[0].ImportRow(row);
        }
    }

    // Update the DataGridView DataSource
    dataGridView.DataSource = _dataSet.Tables[0];
}

  
    private void FromText_TextChanged(object sender, EventArgs e)
    {
       return;
    }

    private void ToText_TextChanged(object sender, EventArgs e)
    {
       return;
    }

    private void DestinationText_TextChanged(object sender, EventArgs e)
    {
       return;
    }
}