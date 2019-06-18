using AppTheatre.Interfaces;
using AppTheatre.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace AppTheatre.Repositories
{
    public class PozoristeRepository : IDisposable, IPozoriste
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Pozoriste pozoriste)
        {
           db.Pozorista.Add(pozoriste);
           db.SaveChanges();
        }

        public void Delete(Pozoriste pozoriste)
        {
            db.Pozorista.Remove(pozoriste);
            db.SaveChanges();

        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Pozoriste> GetAdress(string adress)
        {
            return db.Pozorista.Where(x => x.Adresa.ToLower().Equals(adress.ToLower()));

        }

        public IEnumerable<Pozoriste> GetAll()
        {
            return db.Pozorista.OrderBy(x=>x.Naziv);
        }

        public Pozoriste GetById(int id)
        {
            var pozoriste = db.Pozorista.FirstOrDefault(x => x.Id == id);

            return pozoriste;

        }

        public IEnumerable<Pozoriste> GetGrad(string grad)
        {
            return db.Pozorista.Where(x => x.Grad.ToLower().Equals(grad.ToLower()));
        }

        public IEnumerable<Pozoriste> GetNajstarijeNajmladje()
        {
            return (db.Pozorista.OrderBy(x => x.GodinaOsnivanja).Take(1)).
                Union(db.Pozorista.OrderByDescending(x => x.GodinaOsnivanja).Take(1));


        }

        public IEnumerable<Pozoriste> GetNaziv(string naziv)
        {
            return db.Pozorista.Where(x => x.Naziv.ToLower().Equals(naziv.ToLower()));

        }

        public IEnumerable<Pozoriste> PostPretraga(int x, int y)
        {
            return db.Pozorista.Where(p => p.GodinaOsnivanja > x && p.GodinaOsnivanja < y);
        }

        public void Update(Pozoriste pozoriste)
        {
            db.Entry(pozoriste).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

        }
    }
}