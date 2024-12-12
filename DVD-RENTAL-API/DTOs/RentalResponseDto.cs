namespace DVD_RENTAL_API.DTOs
{
    public class RentalResponseDto
    {
        public int RentalId { get; set; }
        public int CustomerId { get; set; }
        public int DVDId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool? IsOverdue { get; set; } = false;
        public string status { get; set; } = "Pending";
    }
}
