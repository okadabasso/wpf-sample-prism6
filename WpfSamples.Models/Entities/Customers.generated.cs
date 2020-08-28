using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace WpfSamples.Models.Entities
{
    [Table("dbo.Customers")]
    public partial class Customer
    {
        public Customer()
        {
            CustomerId = null;
            CompanyName = null;
            ContactName = null;
            ContactTitle = null;
            Address = null;
            City = null;
            Region = null;
            PostalCode = null;
            Country = null;
            Phone = null;
            Fax = null;
            CustomerCustomerDemos = new HashSet<CustomerCustomerDemo>();
            Orders = new HashSet<Order>();

        }

        ///<summary>column:CustomerID</summary>
        [Key]
        [Required]
        [Column("CustomerID", Order = 0, TypeName = "nchar")]
        public string CustomerId { get; set; }

        ///<summary>column:CompanyName</summary>
        [Required]
        [Column("CompanyName", Order = 1, TypeName = "nvarchar")]
        public string CompanyName { get; set; }

        ///<summary>column:ContactName</summary>
        [Column("ContactName", Order = 2, TypeName = "nvarchar")]
        public string ContactName { get; set; }

        ///<summary>column:ContactTitle</summary>
        [Column("ContactTitle", Order = 3, TypeName = "nvarchar")]
        public string ContactTitle { get; set; }

        ///<summary>column:Address</summary>
        [Column("Address", Order = 4, TypeName = "nvarchar")]
        public string Address { get; set; }

        ///<summary>column:City</summary>
        [Column("City", Order = 5, TypeName = "nvarchar")]
        public string City { get; set; }

        ///<summary>column:Region</summary>
        [Column("Region", Order = 6, TypeName = "nvarchar")]
        public string Region { get; set; }

        ///<summary>column:PostalCode</summary>
        [Column("PostalCode", Order = 7, TypeName = "nvarchar")]
        public string PostalCode { get; set; }

        ///<summary>column:Country</summary>
        [Column("Country", Order = 8, TypeName = "nvarchar")]
        public string Country { get; set; }

        ///<summary>column:Phone</summary>
        [Column("Phone", Order = 9, TypeName = "nvarchar")]
        public string Phone { get; set; }

        ///<summary>column:Fax</summary>
        [Column("Fax", Order = 10, TypeName = "nvarchar")]
        public string Fax { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
