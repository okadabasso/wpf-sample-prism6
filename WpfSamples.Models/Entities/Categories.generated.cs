


















using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace WpfSamples.Models.Entities
{
    [Table("dbo.Categories")]
    public partial class Category
    {
        public Category()
        {

            CategoryId = 0;
            CategoryName = null;
            Description = null;
            Picture = null;

            Products = new HashSet<Product>();

        }


        ///<summary>column:CategoryID</summary>

        [Key]
        [Column("CategoryID", Order = 0, TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }


        ///<summary>column:CategoryName</summary>

        [Required]
        [Column("CategoryName", Order = 1, TypeName = "nvarchar")]
        public string CategoryName { get; set; }


        ///<summary>column:Description</summary>

        [Column("Description", Order = 2, TypeName = "ntext")]
        public string Description { get; set; }


        ///<summary>column:Picture</summary>

        [Column("Picture", Order = 3, TypeName = "image")]
        public byte[] Picture { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
