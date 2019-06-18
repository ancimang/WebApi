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
    public class PozoristaController : ApiController
    {
        IPozoriste _repository { get; set; }

        public PozoristaController(IPozoriste repository)
        {
            _repository = repository;
        }
        public IEnumerable<Pozoriste> Get()
        {
            return _repository.GetAll();
        }
        public IHttpActionResult Get(int id)
        {
            var pozoriste = _repository.GetById(id);
            if (pozoriste == null)
            {
                return NotFound();
            }
            return Ok(pozoriste);
        }
        public IHttpActionResult Post(Pozoriste pozoriste)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.Add(pozoriste);

            return CreatedAtRoute("DefaultApi", new { id = pozoriste.Id }, pozoriste);
        }
        public IHttpActionResult Put(int id, Pozoriste pozoriste)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pozoriste.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(pozoriste);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(pozoriste);
        }
        public IHttpActionResult Delete(int id)
        {
            var pozoriste = _repository.GetById(id);
            if (pozoriste == null)
            {
                return NotFound();
            }

            _repository.Delete(pozoriste);

            return StatusCode(HttpStatusCode.NoContent);
        }
        public IEnumerable<Pozoriste> PostPretraga(int x, int y)
        {
            return _repository.PostPretraga(x, y);
        }
        public IEnumerable<Pozoriste> GetNaziv(string naziv)
        {
            return _repository.GetNaziv(naziv);
        }
        [Route("api/pozoriste/grad/")]
        public IEnumerable<Pozoriste> GetGrad(string grad)
        {
            return _repository.GetGrad(grad);
        }
        [Route("api/pozoriste/najstarije")]
        public IEnumerable<Pozoriste> GetNajstarijeNajmladje()
        {
            return _repository.GetNajstarijeNajmladje();
        }
        public IEnumerable<Pozoriste> GetAdress(string adress)
        {
            return _repository.GetAdress(adress);
        }
    }
}
