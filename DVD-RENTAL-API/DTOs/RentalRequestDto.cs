namespace DVD_RENTAL_API.DTOs
{
    public class RentalRequestDto
    {
        public int CustomerId {  get; set; }
        public int DVDId {  get; set; }
        public DateTime RentalDate {  get; set; }
    }
}
