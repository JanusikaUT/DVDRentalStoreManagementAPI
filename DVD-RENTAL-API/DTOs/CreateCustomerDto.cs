﻿namespace DVD_RENTAL_API.DTOs
{
    public class CreateCustomerDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string NIC { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
