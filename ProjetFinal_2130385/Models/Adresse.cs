using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2130385.Models
{
    [Table("Adresse")]
    public partial class Adresse
    {
        public Adresse()
        {
            Clients = new HashSet<Client>();
            Magasins = new HashSet<Magasin>();
        }

        [Key]
        [Column("AdresseID")]
        public int AdresseId { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? NoPorte { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? NoApp { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Rue { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Ville { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? CodePostal { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Province { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Pays { get; set; }

        [InverseProperty("Adresse")]
        public virtual ICollection<Client> Clients { get; set; }
        [InverseProperty("Adresse")]
        public virtual ICollection<Magasin> Magasins { get; set; }
    }
}
