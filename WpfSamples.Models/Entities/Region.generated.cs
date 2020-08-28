using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace WpfSamples.Models.Entities
{
    [Table("dbo.Region")]
    public partial class Region
    {
        public Region()
        {
            RegionId = 0;
            RegionDescription = null;
            Territories = new HashSet<Territory>();

        }

        ///<summary>column:RegionID</summary>
        [Key]
        [Column("RegionID", Order = 0, TypeName = "int")]
        public int RegionId { get; set; }

        ///<summary>column:RegionDescription</summary>
        [Required]
        [Column("RegionDescription", Order = 1, TypeName = "nchar")]
        public string RegionDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Territory> Territories { get; set; }
    }
}
