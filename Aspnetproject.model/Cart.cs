// Inside Cart.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspnetproject.model
{
    public class Cart
    {
        public int Id { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
