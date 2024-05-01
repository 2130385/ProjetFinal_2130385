using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2130385.Models
{
    [Keyless]
    public partial class VwDrone
    {
        [Column("DroneID")]
        public int DroneId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? NumSerie { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? NomModele { get; set; }
        public int? Vitesse { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Prix { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateSortie { get; set; }
    }
}
