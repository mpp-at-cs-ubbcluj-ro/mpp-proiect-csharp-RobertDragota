using System;

namespace AppDomain.Domain
{
    [Serializable]
    public class Reservation : Entity<long>
    {
    
        private Account _account;
        private string _phoneNumber;
        private string _clientName;
        private long _tickets;
        private Trip _trip;

        public Reservation(Account account, string phoneNumber, long tickets, Trip trip, string clientName)
        {
            this._account = account;
            this._phoneNumber = phoneNumber;
            this._tickets = tickets;
            this._trip = trip;
            _clientName = clientName;
        }

        public Account Account
        {
            get { return _account; }
            set { _account = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public long Tickets
        {
            get { return _tickets; }
            set { _tickets = value; }
        }

        public Trip Trip
        {
            get { return _trip; }
            set { _trip = value; }
        }
        
        public string ClientName
        {
            get { return _clientName; }
            set { _clientName = value; }
        }
    }
}
