using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2130385.Models
{
    [Table("Commande", Schema = "Magasins")]
    public partial class Commande
    {
        [Key]
        [Column("CommandeID")]
        public int CommandeId { get; set; }
        [Column("ClientID")]
        public int? ClientId { get; set; }
        [Column("DroneID")]
        public int? DroneId { get; set; }
        [Column("MagasinID")]
        public int? MagasinId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateCommande { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Montant { get; set; }

        [ForeignKey("ClientId")]
        [InverseProperty("Commandes")]
        public virtual Client? Client { get; set; }
        [ForeignKey("DroneId")]
        [InverseProperty("Commandes")]
        public virtual Drone? Drone { get; set; }
        [ForeignKey("MagasinId")]
        [InverseProperty("Commandes")]
        public virtual Magasin? Magasin { get; set; }
    }
}
