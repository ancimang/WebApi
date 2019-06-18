using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppTheatre.Models
{
    public class VrstaPredstave
    {
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Naziv { get; set; }

        public string Opis { get; set; }

        //public ICollection<Predstava> ListaPredstava { get; set; }
    }
}