﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GroceryStoreSystem.Entities
{
    internal partial class Product
    {
        public Product()
        {
            OrderLists = new HashSet<OrderList>();
        }

        [Key]
        public int ProductID { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Column(TypeName = "money")]
        public decimal Discount { get; set; }
        [Required]
        [StringLength(20)]
        public string UnitSize { get; set; }
        public int CategoryID { get; set; }
        public bool Taxable { get; set; }
        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        [ForeignKey(nameof(CategoryID))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
        [InverseProperty(nameof(OrderList.Product))]
        public virtual ICollection<OrderList> OrderLists { get; set; }
    }
}