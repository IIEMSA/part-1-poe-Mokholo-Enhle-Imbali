namespace EventEase.Models
{
    public class Events
    {
        //in correlation with database fields
        public int eventsID { get; set; }
        public string eventName { get; set; }
        public string eventDate { get; set; }
        public string description { get; set; }
        public string venueID { get; set; }
    }
}
