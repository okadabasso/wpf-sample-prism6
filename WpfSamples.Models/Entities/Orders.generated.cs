using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace WpfSamples.Models.Entities
{
    [Table("dbo.Orders")]
    public partial class Order
    {
        public Order()
        {
            OrderId = 0;
            CustomerId = null;
            EmployeeId = null;
            OrderDate = null;
            RequiredDate = null;
            ShippedDate = null;
            ShipVia = null;
            Freight = null;
            ShipName = null;
            ShipAddress = null;
            ShipCity = null;
            ShipRegion = null;
            ShipPostalCode = null;
            ShipCountry = null;
            OrderDetails = new HashSet<OrderDetail>();

        }

        ///<summary>column:OrderID</summary>
        [Key]
        [Column("OrderID", Order = 0, TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        ///<summary>column:CustomerID</summary>
        [Column("CustomerID")]
        public string CustomerId { get; set; }

        ///<summary>column:EmployeeID</summary>
        [Column("EmployeeID")]
        public int? EmployeeId { get; set; }

        ///<summary>column:OrderDate</summary>
        [Column("OrderDate", Order = 3, TypeName = "datetime")]
        public DateTime? OrderDate { get; set; }

        ///<summary>column:RequiredDate</summary>
        [Column("RequiredDate", Order = 4, TypeName = "datetime")]
        public DateTime? RequiredDate { get; set; }

        ///<summary>column:ShippedDate</summary>
        [Column("ShippedDate", Order = 5, TypeName = "datetime")]
        public DateTime? ShippedDate { get; set; }

        ///<summary>column:ShipVia</summary>
        [Column("ShipVia")]
        public int? ShipVia { get; set; }

        ///<summary>column:Freight</summary>
        [Column("Freight", Order = 7, TypeName = "money")]
        public decimal? Freight { get; set; }

        ///<summary>column:ShipName</summary>
        [Column("ShipName", Order = 8, TypeName = "nvarchar")]
        public string ShipName { get; set; }

        ///<summary>column:ShipAddress</summary>
        [Column("ShipAddress", Order = 9, TypeName = "nvarchar")]
        public string ShipAddress { get; set; }

        ///<summary>column:ShipCity</summary>
        [Column("ShipCity", Order = 10, TypeName = "nvarchar")]
        public string ShipCity { get; set; }

        ///<summary>column:ShipRegion</summary>
        [Column("ShipRegion", Order = 11, TypeName = "nvarchar")]
        public string ShipRegion { get; set; }

        ///<summary>column:ShipPostalCode</summary>
        [Column("ShipPostalCode", Order = 12, TypeName = "nvarchar")]
        public string ShipPostalCode { get; set; }

        ///<summary>column:ShipCountry</summary>
        [Column("ShipCountry", Order = 13, TypeName = "nvarchar")]
        public string ShipCountry { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Shipper ShipViaShipper { get; set; }
    }
}
