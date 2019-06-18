using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppTheatre.Models
{
    public class Veza
    {
       
        public int Id { get; set; }


        public int GlumacId { get; set; }
        public Glumac Glumac { get; set; }


        public int PredstavaId { get; set; }
        public Predstava Predstava { get; set; }
    }
}