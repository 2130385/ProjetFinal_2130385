using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2130385.Models
{
    [Keyless]
    public partial class VwCommande
    {
        [Column("CommandeID")]
        public int CommandeId { get; set; }
        [StringLength(102)]
        public string? NomClient { get; set; }
        [Column("courriel")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Courriel { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? NumSerieDrone { get; set; }
        [Column("MagasinID")]
        public int MagasinId { get; set; }
        [StringLength(417)]
        [Unicode(false)]
        public string? AdresseMagasin { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateCommande { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Montant { get; set; }
    }
}
