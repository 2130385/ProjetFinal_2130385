using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2130385.Models
{
    [Table("Magasin", Schema = "Magasins")]
    public partial class Magasin
    {
        public Magasin()
        {
            Commandes = new HashSet<Commande>();
        }

        [Key]
        [Column("MagasinID")]
        public int MagasinId { get; set; }
        [Column("AdresseID")]
        public int? AdresseId { get; set; }

        [ForeignKey("AdresseId")]
        [InverseProperty("Magasins")]
        public virtual Adresse? Adresse { get; set; }
        [InverseProperty("Magasin")]
        public virtual ICollection<Commande> Commandes { get; set; }
    }
}
