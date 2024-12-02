namespace DVD_RENTAL_API.DTOs
{
    public class CreateRentalDto
    {
        public int CustomerId { get; set; }
        public int DVDId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
