using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace WpfSamples.Models.Entities
{
    [Table("dbo.CustomerCustomerDemo")]
    public partial class CustomerCustomerDemo
    {
        public CustomerCustomerDemo()
        {
            CustomerId = null;
            CustomerTypeId = null;

        }

        ///<summary>column:CustomerID</summary>
        [Key]
        [Required]
        [Column("CustomerID", Order = 0, TypeName = "nchar")]
        public string CustomerId { get; set; }

        ///<summary>column:CustomerTypeID</summary>
        [Key]
        [Required]
        [Column("CustomerTypeID", Order = 1, TypeName = "nchar")]
        public string CustomerTypeId { get; set; }

        public virtual CustomerDemographic CustomerDemographic { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
