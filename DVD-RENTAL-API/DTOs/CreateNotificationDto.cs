namespace DVD_RENTAL_API.DTOs
{
    public class CreateNotificationDto
    {
        public int RentalId { get; set; }
        public string Message { get; set; }
    }
}
