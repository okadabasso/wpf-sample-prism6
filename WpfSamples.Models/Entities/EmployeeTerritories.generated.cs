using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace WpfSamples.Models.Entities
{
    [Table("dbo.EmployeeTerritories")]
    public partial class EmployeeTerritory
    {
        public EmployeeTerritory()
        {
            EmployeeId = 0;
            TerritoryId = null;

        }

        ///<summary>column:EmployeeID</summary>
        [Key]
        [Column("EmployeeID", Order = 0, TypeName = "int")]
        public int EmployeeId { get; set; }

        ///<summary>column:TerritoryID</summary>
        [Key]
        [Required]
        [Column("TerritoryID", Order = 1, TypeName = "nvarchar")]
        public string TerritoryId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Territory Territory { get; set; }
    }
}
