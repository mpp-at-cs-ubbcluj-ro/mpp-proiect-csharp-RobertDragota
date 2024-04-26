using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AppDomain.Domain;
using AppServices.Service;

namespace Client;

 public partial class App : Form, AppObserverInterface
    {
        private readonly ServiceAppInterface _serviceController;
        private Account _account;
        readonly DataSet _dataSet = new DataSet();
        public event EventHandler<EventArgs> updateEvent; 
        public App(ServiceAppInterface serviceController)
        {
            _serviceController = serviceController;
            InitializeComponent();
            //UpdateDataGrid(serviceController.Get_All_Trips());
            this.updateEvent += UpdateTrips;
            dataGridView.DataSource = ConvertToDataTable(serviceController.Get_All_Trips().ToList());
        }

        protected virtual void OnUserEvent(EventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Event fired");
        }
        public void SetAccount(Account account)
        {
            _account = account;
        }
        private void UpdateDataGrid(IEnumerable<Trip> list)
        {
            try{
                DataTable tripsTable = ConvertToDataTable(list.ToList());
                dataGridView.DataSource = null;
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
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trips: " + ex.Message);
            }
        }

        private DataTable ConvertToDataTable(List<Trip> trips)
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(long));
            table.Columns.Add("Destination", typeof(string));
            table.Columns.Add("TransportCompany", typeof(string));
            table.Columns.Add("Price", typeof(double));
            table.Columns.Add("AvailableSeats", typeof(int));
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("StartHour", typeof(DateTime));
            table.Columns.Add("FinishHour", typeof(DateTime));

            foreach (var trip in trips)
            {
                table.Rows.Add(trip.Id, trip.Destination, trip.TransportCompany, trip.Price, trip.AvailableSeats, trip.Date, trip.StartHour, trip.FinishHour);
            }

            return table;
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView.Rows[e.RowIndex];
                var trip = new Trip(
                    Convert.ToString(selectedRow.Cells["Destination"].Value),
                    Convert.ToString(selectedRow.Cells["TransportCompany"].Value),
                    Convert.ToDouble(selectedRow.Cells["Price"].Value),
                    Convert.ToInt32(selectedRow.Cells["AvailableSeats"].Value),
                    Convert.ToDateTime(selectedRow.Cells["Date"].Value),
                    Convert.ToDateTime(selectedRow.Cells["StartHour"].Value),
                    Convert.ToDateTime(selectedRow.Cells["FinishHour"].Value)
                );

                // Set Id if available
                trip.Id = Convert.ToInt64(selectedRow.Cells["Id"].Value);

                // Assume ReservationController is another Form that takes a trip
                var reservationController = new ReservationController(trip, _serviceController, this);
                reservationController.setAccount(_account);
                reservationController.ShowDialog();
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var destination = DestinationText.Text;
            if (!int.TryParse(FromText.Text, out int from) || !int.TryParse(ToText.Text, out int to))
            {
                MessageBox.Show("Invalid hour format. Please enter numeric values.");
                return;
            }

            if (from > to || from < 0 || from > 24 || to > 24)
            {
                MessageBox.Show("Invalid hours. 'From' hour should be less than 'To' hour and between 0 and 24.");
                return;
            }

            try
            {
                var filteredTrips = _serviceController.Get_All_Trip_By_Destination_From_To(destination, from, to).ToList();
                dataGridView.DataSource = ConvertToDataTable(filteredTrips);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public virtual  void UpdateTrips(object sender, EventArgs e)
        {
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            if (this.InvokeRequired)
            {
                // InvokeRequired is true if the current thread is not the UI thread.
                // Use BeginInvoke to marshal the call to the UI thread.
                this.BeginInvoke(new MethodInvoker(() => UpdateDataGrid(_serviceController.Get_All_Trips().ToList())));
            }
            else
            {
                // Update your UI here, e.g. update a data grid view.
                // You might need to convert the list to a data source format that your UI understands.
                dataGridView.DataSource = ConvertToDataTable(_serviceController.Get_All_Trips().ToList());
            }
        }


        public void UpdateTrips(IEnumerable<Trip> list)
        {
            EventArgs e = new EventArgs();
            OnUserEvent(e);
        }
    }
