using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppTheatre.Models
{
    public class Glumac
    {
        public int Id { get; set; }

        [Required]
        public string Ime { get; set;}

        [Required]
        public string Prezime { get; set; }

        public int? GodinaRodjenja { get; set; }


        //public virtual ICollection<Veza> Veze { get; set; }




    }
}