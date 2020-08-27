
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace WpfSamples.Models.Entities
{
    [Table("dbo.CustomerDemographics")]
    public partial class CustomerDemographic
    {
        public CustomerDemographic()
        {

            CustomerTypeId = null;
            CustomerDesc = null;

            CustomerCustomerDemos = new HashSet<CustomerCustomerDemo>();

        }


        ///<summary>column:CustomerTypeID</summary>

        [Key]
        [Required]
        [Column("CustomerTypeID", Order = 0, TypeName = "nchar")]
        public string CustomerTypeId { get; set; }


        ///<summary>column:CustomerDesc</summary>

        [Column("CustomerDesc", Order = 1, TypeName = "ntext")]
        public string CustomerDesc { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }
    }
}
