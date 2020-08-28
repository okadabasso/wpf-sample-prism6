using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace WpfSamples.Models.Entities
{
    [Table("dbo.Shippers")]
    public partial class Shipper
    {
        public Shipper()
        {
            ShipperId = 0;
            CompanyName = null;
            Phone = null;
            ShipViaOrders = new HashSet<Order>();

        }

        ///<summary>column:ShipperID</summary>
        [Key]
        [Column("ShipperID", Order = 0, TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShipperId { get; set; }

        ///<summary>column:CompanyName</summary>
        [Required]
        [Column("CompanyName", Order = 1, TypeName = "nvarchar")]
        public string CompanyName { get; set; }

        ///<summary>column:Phone</summary>
        [Column("Phone", Order = 2, TypeName = "nvarchar")]
        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> ShipViaOrders { get; set; }
    }
}
