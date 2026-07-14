using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        [ValidateNever]
        public string Username { get; set; } = null!;
        [ValidateNever]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = null!;
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = null!;
        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; } = null!;
        [Required(ErrorMessage = "Postal code is required.")]
        public string PostalCode { get; set; } = null!;
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } = null!;
        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal Total { get; set; }

        // Navigation property
        [ValidateNever]
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
