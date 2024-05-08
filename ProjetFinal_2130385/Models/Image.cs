using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2130385.Models
{
    [Table("Image", Schema = "Magasins")]
    [Index("Identifiant", Name = "UC_Image_Identifiant", IsUnique = true)]
    public partial class Image
    {
        [Key]
        [Column("ImageID")]
        public int ImageId { get; set; }
        [StringLength(100)]
        public string Nom { get; set; } = null!;
        public Guid Identifiant { get; set; }
        public byte[]? FichierImage { get; set; }
    }
}
