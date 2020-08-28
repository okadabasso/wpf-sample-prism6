using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace WpfSamples.Models.Entities
{
    [Table("dbo.Employees")]
    public partial class Employee
    {
        public Employee()
        {
            EmployeeId = 0;
            LastName = null;
            FirstName = null;
            Title = null;
            TitleOfCourtesy = null;
            BirthDate = null;
            HireDate = null;
            Address = null;
            City = null;
            Region = null;
            PostalCode = null;
            Country = null;
            HomePhone = null;
            Extension = null;
            Photo = null;
            Notes = null;
            ReportsTo = null;
            PhotoPath = null;
            ReportsToEmployees = new HashSet<Employee>();
            EmployeeTerritories = new HashSet<EmployeeTerritory>();
            Orders = new HashSet<Order>();

        }

        ///<summary>column:EmployeeID</summary>
        [Key]
        [Column("EmployeeID", Order = 0, TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        ///<summary>column:LastName</summary>
        [Required]
        [Column("LastName", Order = 1, TypeName = "nvarchar")]
        public string LastName { get; set; }

        ///<summary>column:FirstName</summary>
        [Required]
        [Column("FirstName", Order = 2, TypeName = "nvarchar")]
        public string FirstName { get; set; }

        ///<summary>column:Title</summary>
        [Column("Title", Order = 3, TypeName = "nvarchar")]
        public string Title { get; set; }

        ///<summary>column:TitleOfCourtesy</summary>
        [Column("TitleOfCourtesy", Order = 4, TypeName = "nvarchar")]
        public string TitleOfCourtesy { get; set; }

        ///<summary>column:BirthDate</summary>
        [Column("BirthDate", Order = 5, TypeName = "datetime")]
        public DateTime? BirthDate { get; set; }

        ///<summary>column:HireDate</summary>
        [Column("HireDate", Order = 6, TypeName = "datetime")]
        public DateTime? HireDate { get; set; }

        ///<summary>column:Address</summary>
        [Column("Address", Order = 7, TypeName = "nvarchar")]
        public string Address { get; set; }

        ///<summary>column:City</summary>
        [Column("City", Order = 8, TypeName = "nvarchar")]
        public string City { get; set; }

        ///<summary>column:Region</summary>
        [Column("Region", Order = 9, TypeName = "nvarchar")]
        public string Region { get; set; }

        ///<summary>column:PostalCode</summary>
        [Column("PostalCode", Order = 10, TypeName = "nvarchar")]
        public string PostalCode { get; set; }

        ///<summary>column:Country</summary>
        [Column("Country", Order = 11, TypeName = "nvarchar")]
        public string Country { get; set; }

        ///<summary>column:HomePhone</summary>
        [Column("HomePhone", Order = 12, TypeName = "nvarchar")]
        public string HomePhone { get; set; }

        ///<summary>column:Extension</summary>
        [Column("Extension", Order = 13, TypeName = "nvarchar")]
        public string Extension { get; set; }

        ///<summary>column:Photo</summary>
        [Column("Photo", Order = 14, TypeName = "image")]
        public byte[] Photo { get; set; }

        ///<summary>column:Notes</summary>
        [Column("Notes", Order = 15, TypeName = "ntext")]
        public string Notes { get; set; }

        ///<summary>column:ReportsTo</summary>
        [Column("ReportsTo")]
        public int? ReportsTo { get; set; }

        ///<summary>column:PhotoPath</summary>
        [Column("PhotoPath", Order = 17, TypeName = "nvarchar")]
        public string PhotoPath { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> ReportsToEmployees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Employee ReportsToEmployee { get; set; }
    }
}
