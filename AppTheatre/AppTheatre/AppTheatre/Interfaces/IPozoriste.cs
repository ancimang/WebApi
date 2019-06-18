using AppTheatre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTheatre.Interfaces
{
    public interface IPozoriste
    {
        IEnumerable<Pozoriste> GetAll();
        Pozoriste GetById(int id);
        void Delete(Pozoriste pozoriste);
        void Update(Pozoriste pozoriste);
        void Add(Pozoriste pozoriste);


        IEnumerable<Pozoriste> PostPretraga(int x, int y);
        IEnumerable<Pozoriste> GetNaziv(string naziv);
        IEnumerable<Pozoriste> GetGrad(string grad);
        IEnumerable<Pozoriste> GetAdress(string adress);
        IEnumerable<Pozoriste> GetNajstarijeNajmladje();

    }
}
