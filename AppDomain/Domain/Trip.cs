using System;

namespace AppDomain.Domain
{
    [Serializable]
    public class Trip : Entity<long>
    {
   
        public string Destination { get; set; }
        public string TransportCompany { get; set; }
        public double Price { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime FinishHour { get; set; }

        public Trip(string destination, string transportCompany, double price, int availableSeats, DateTime date, DateTime startHour, DateTime finishHour)
        {
        
            Destination = destination;
            TransportCompany = transportCompany;
            Price = price;
            AvailableSeats = availableSeats;
            Date = date;
            StartHour = startHour;
            FinishHour = finishHour;
        }
    }
}
