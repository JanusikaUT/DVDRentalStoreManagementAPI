namespace DVD_RENTAL_API.DTOs
{
    public class UpdateDvdDto
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int AvailableCopies { get; set; }
    }
}
