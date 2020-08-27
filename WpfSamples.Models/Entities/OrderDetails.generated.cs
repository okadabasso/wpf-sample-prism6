
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace WpfSamples.Models.Entities
{
    [Table("dbo.Order Details")]
    public partial class OrderDetail
    {
        public OrderDetail()
        {

            OrderId = 0;
            ProductId = 0;
            UnitPrice = 0;
            Quantity = 0;
            Discount = 0;


        }


        ///<summary>column:OrderID</summary>

        [Key]
        [Column("OrderID", Order = 0, TypeName = "int")]
        public int OrderId { get; set; }


        ///<summary>column:ProductID</summary>

        [Key]
        [Column("ProductID", Order = 1, TypeName = "int")]
        public int ProductId { get; set; }


        ///<summary>column:UnitPrice</summary>

        [Column("UnitPrice", Order = 2, TypeName = "money")]
        public decimal UnitPrice { get; set; }


        ///<summary>column:Quantity</summary>

        [Column("Quantity", Order = 3, TypeName = "smallint")]
        public short Quantity { get; set; }


        ///<summary>column:Discount</summary>

        [Column("Discount", Order = 4, TypeName = "real")]
        public float Discount { get; set; }


        public virtual Order Orders { get; set; }
        public virtual Product Products { get; set; }
    }
}
