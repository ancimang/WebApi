using AppTheatre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTheatre.Interfaces
{
    public interface IGlumac
    {
        Glumac GetById(int id);
        IEnumerable<Glumac>  GetAll();
        void Add(Glumac glumac);
        void Update(Glumac glumac);
        void Delete(Glumac glumac);

        IEnumerable<Glumac> GetByIme(string ime);
        IEnumerable<Glumac> GetByPrezime(string prezime);

    }
}
