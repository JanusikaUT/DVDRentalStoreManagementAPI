namespace DVD_RENTAL_API.DTOs
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }
        public int RentalId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
