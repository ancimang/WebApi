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
    public class VrstePredstaveRepository : IDisposable, IVrstaPredstave
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(VrstaPredstave vrstaPredstave)
        {
            db.Vrste.Add(vrstaPredstave);
            db.SaveChanges();

        }

        public void Delete(VrstaPredstave vrstaPredstave)
        {
             db.Vrste.Remove(vrstaPredstave);
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

        public IEnumerable<VrstaPredstave> GetAll()
        {
            return db.Vrste;
        }

        public VrstaPredstave GetById(int id)
        {
            return db.Vrste.FirstOrDefault(x => x.Id == id);

        }

        public IEnumerable<VrstaPredstave> GetByNaziv(string naziv)
        {
            return db.Vrste.Where(x => x.Naziv.ToLower().Equals(naziv.ToLower()));
        }


        public void Update(VrstaPredstave vrstaPredstave)
        {
            db.Entry(vrstaPredstave).State = EntityState.Modified;
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