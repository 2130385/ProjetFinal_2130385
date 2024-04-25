using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2130385.Models
{
    [Table("Courriel", Schema = "Clients")]
    [Index("Courriel1", Name = "UX_Courriel_Email", IsUnique = true)]
    public partial class Courriel
    {
        [Key]
        [Column("CourrielID")]
        public int CourrielId { get; set; }
        [Column("ClientID")]
        public int? ClientId { get; set; }
        [Column("courriel")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Courriel1 { get; set; }

        [ForeignKey("ClientId")]
        [InverseProperty("Courriels")]
        public virtual Client? Client { get; set; }
    }
}
