using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2130385.Models
{
    [Table("Modele", Schema = "Magasins")]
    public partial class Modele
    {
        public Modele()
        {
            Drones = new HashSet<Drone>();
        }

        [Key]
        [Column("ModeleID")]
        public int ModeleId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Nom { get; set; }
        public int? Vitesse { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Prix { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateSortie { get; set; }
        [Column("ImageID")]
        public int? ImageId { get; set; }

        [ForeignKey("ImageId")]
        [InverseProperty("Modeles")]
        public virtual Image? Image { get; set; }
        [InverseProperty("Modele")]
        public virtual ICollection<Drone> Drones { get; set; }
    }
}
