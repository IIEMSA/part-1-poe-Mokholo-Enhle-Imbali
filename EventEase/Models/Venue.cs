namespace EventEase.Models
{
    public class Venue
    {
        //in correlation with database fields
        public int venueId { get; set; }
        public string venueName { get; set; }
        public string location { get; set; }
        public string capacity { get; set; }
        public string imageurl { get; set; }
    }
}
