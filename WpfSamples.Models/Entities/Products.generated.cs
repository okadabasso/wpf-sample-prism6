
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace WpfSamples.Models.Entities
{
    [Table("dbo.Products")]
    public partial class Product
    {
        public Product()
        {

            ProductId = 0;
            ProductName = null;
            SupplierId = null;
            CategoryId = null;
            QuantityPerUnit = null;
            UnitPrice = null;
            UnitsInStock = null;
            UnitsOnOrder = null;
            ReorderLevel = null;
            Discontinued = false;

            OrderDetails = new HashSet<OrderDetail>();

        }


        ///<summary>column:ProductID</summary>

        [Key]
        [Column("ProductID", Order = 0, TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }


        ///<summary>column:ProductName</summary>

        [Required]
        [Column("ProductName", Order = 1, TypeName = "nvarchar")]
        public string ProductName { get; set; }


        ///<summary>column:SupplierID</summary>

        [Column("SupplierID")]
        public int? SupplierId { get; set; }


        ///<summary>column:CategoryID</summary>

        [Column("CategoryID")]
        public int? CategoryId { get; set; }


        ///<summary>column:QuantityPerUnit</summary>

        [Column("QuantityPerUnit", Order = 4, TypeName = "nvarchar")]
        public string QuantityPerUnit { get; set; }


        ///<summary>column:UnitPrice</summary>

        [Column("UnitPrice", Order = 5, TypeName = "money")]
        public decimal? UnitPrice { get; set; }


        ///<summary>column:UnitsInStock</summary>

        [Column("UnitsInStock", Order = 6, TypeName = "smallint")]
        public short? UnitsInStock { get; set; }


        ///<summary>column:UnitsOnOrder</summary>

        [Column("UnitsOnOrder", Order = 7, TypeName = "smallint")]
        public short? UnitsOnOrder { get; set; }


        ///<summary>column:ReorderLevel</summary>

        [Column("ReorderLevel", Order = 8, TypeName = "smallint")]
        public short? ReorderLevel { get; set; }


        ///<summary>column:Discontinued</summary>

        [Column("Discontinued", Order = 9, TypeName = "bit")]
        public bool Discontinued { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Category Categories { get; set; }
        public virtual Supplier Suppliers { get; set; }
    }
}
