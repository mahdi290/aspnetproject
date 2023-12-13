using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aspnetproject.model
{
    public class CartItem
    {
        public int Id { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int Quantity { get; set; } // Add this line


        [Required]
        public Client Client { get; set; }

        [Required]
        public Product Product { get; set; }
    }
}

