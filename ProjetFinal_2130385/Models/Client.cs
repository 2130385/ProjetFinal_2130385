using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2130385.Models
{
    [Table("Client", Schema = "Clients")]
    public partial class Client
    {
        public Client()
        {
            Commandes = new HashSet<Commande>();
            Courriels = new HashSet<Courriel>();
        }

        [Key]
        [Column("ClientID")]
        public int ClientId { get; set; }
        [Column("AdresseID")]
        public int? AdresseId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Prenom { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? NomFamille { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? NoTel1 { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? NoTel2 { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateNaissance { get; set; }

        [ForeignKey("AdresseId")]
        [InverseProperty("Clients")]
        public virtual Adresse? Adresse { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<Commande> Commandes { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<Courriel> Courriels { get; set; }
    }
}
