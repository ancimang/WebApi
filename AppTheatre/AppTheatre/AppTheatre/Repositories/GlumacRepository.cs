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
    public class GlumacRepository:IDisposable,IGlumac
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Glumac glumac)
        {
            db.Glumci.Add(glumac);
            db.SaveChanges();

        }

        public void Delete(Glumac glumac)
        {
            db.Glumci.Remove(glumac);
            db.SaveChanges();
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(db != null)
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

        public IEnumerable<Glumac> GetAll()
        {
            return db.Glumci.OrderBy(x => x.GodinaRodjenja);

        }

        public Glumac GetById(int id)
        {
            return db.Glumci.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Glumac> GetByIme(string ime)
        {

            return db.Glumci.Where(x => x.Ime.ToLower().Equals(ime.ToLower()));
        }

        public IEnumerable<Glumac> GetByPrezime(string prezime)
        {
            return db.Glumci.Where(x => x.Prezime.ToLower().Equals(prezime.ToLower()));
        }

        public void Update(Glumac glumac)
        {
            db.Entry(glumac).State = EntityState.Modified;
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