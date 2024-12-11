namespace DVD_RENTAL_API.DTOs
{
    public class RentalDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int DVDId { get; set; }
        public string DVDTitle { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsOverdue { get; set; }
        public string Status { get; set; }
    }
}
