using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace WpfSamples.Models.Entities
{
    [Table("dbo.Territories")]
    public partial class Territory
    {
        public Territory()
        {
            TerritoryId = null;
            TerritoryDescription = null;
            RegionId = 0;
            EmployeeTerritories = new HashSet<EmployeeTerritory>();

        }

        ///<summary>column:TerritoryID</summary>
        [Key]
        [Required]
        [Column("TerritoryID", Order = 0, TypeName = "nvarchar")]
        public string TerritoryId { get; set; }

        ///<summary>column:TerritoryDescription</summary>
        [Required]
        [Column("TerritoryDescription", Order = 1, TypeName = "nchar")]
        public string TerritoryDescription { get; set; }

        ///<summary>column:RegionID</summary>
        [Column("RegionID")]
        public int RegionId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
        public virtual Region Region { get; set; }
    }
}
