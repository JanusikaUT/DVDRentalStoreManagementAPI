namespace DVD_RENTAL_API.Models
{
    public class Notification
    {
        public int NotificationId { get; set; } // Primary Key

        public int RentalId { get; set; } // Foreign Key to Rental
        public Rental Rental { get; set; } // Navigation property

        public string Message { get; set; } // Notification message
        public DateTime Timestamp { get; set; } // Timestamp of the notification
    }
}
