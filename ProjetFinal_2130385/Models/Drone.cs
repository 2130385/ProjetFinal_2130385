using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2130385.Models
{
    [Table("Drone", Schema = "Magasins")]
    public partial class Drone
    {
        public Drone()
        {
            Commandes = new HashSet<Commande>();
        }

        [Key]
        [Column("DroneID")]
        public int DroneId { get; set; }
        [Column("ModeleID")]
        public int? ModeleId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? NumSerie { get; set; }

        [ForeignKey("ModeleId")]
        [InverseProperty("Drones")]
        public virtual Modele? Modele { get; set; }
        [InverseProperty("Drone")]
        public virtual ICollection<Commande> Commandes { get; set; }
    }
}
