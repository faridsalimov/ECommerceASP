using ECommerce.Core.Abstraction;
using System;
using System.Collections.Generic;

namespace ECommerce.Entities.Models
{
    public partial class Category:IEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
