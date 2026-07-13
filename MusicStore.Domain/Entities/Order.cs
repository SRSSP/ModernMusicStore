using System;
using System.Collections.Generic;

namespace MusicStore.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Username { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal Total { get; set; }

        // Navigation property
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
