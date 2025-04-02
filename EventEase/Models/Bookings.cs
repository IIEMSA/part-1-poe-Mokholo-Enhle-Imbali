namespace EventEase.Models
{
    public class Bookings
    {
        //in correlation with database fields
        public int bookingsID { get; set; }
        public int eventsID { get; set; }
        public int venueID { get; set; }
        public string bookingDate { get; set; }
    }
}
