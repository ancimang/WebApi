using AppTheatre.Interfaces;
using AppTheatre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppTheatre.Controllers
{
    public class GlumciController : ApiController
    {
        IGlumac _repository { get; set; }

        public GlumciController (IGlumac repository)
        {
            _repository = repository;
        }
        public IHttpActionResult Get(int id)
        {
            var glumac = _repository.GetById(id);
            if (glumac == null)
            {
                return NotFound();
            }
            return Ok(glumac);

        }
       
        public IEnumerable<Glumac> Get()
        {
            return _repository.GetAll();
        }
        public IHttpActionResult Delete(int id)
        {
            var glumac = _repository.GetById(id);
            if (glumac == null)
            {
                return BadRequest();
            }
            _repository.Delete(glumac);

            return StatusCode(HttpStatusCode.NoContent);

        }
        public IHttpActionResult Put (int id, Glumac glumac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (glumac.Id == id)
            {
                return BadRequest();
            }
            try
            {
                _repository.Update(glumac);
            }
            catch
            {
                return BadRequest();

            }
            return Ok(glumac);
        }
        public IHttpActionResult Post(Glumac glumac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.Add(glumac);
            return CreatedAtRoute("DefaultApi", new { id = glumac.Id }, glumac);
        }

        public IEnumerable<Glumac> GetByIme(string ime)
        {
            return _repository.GetByIme(ime);
        }
        public IEnumerable<Glumac> GetByPrezime(string prezime)
        {
            return _repository.GetByPrezime(prezime);
        }
    }
}
