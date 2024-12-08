﻿namespace DVD_RENTAL_API.Models
{
    public class DVD
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; } // Add this property
        public DateTime ReleaseDate { get; set; }
        public int AvailableCopies { get; set; }
        public ICollection<Rental> Rentals { get; set; } // Navigation property
    }
}
