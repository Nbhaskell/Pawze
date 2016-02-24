using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Pawze.API.Domain;
using Pawze.API.Infrastructure;

namespace Pawze.API.Controllers
{
    [Authorize]
    public class PawzeConfigurationsController : ApiController
    {
        private PawzeDataContext db = new PawzeDataContext();

        // GET: api/Configurations
        public IQueryable<PawzeConfiguration> GetConfigurations()
        {
            return db.PawzeConfigurations;
        }

        // GET: api/Configurations/5
        [ResponseType(typeof(PawzeConfiguration))]
        public IHttpActionResult GetConfiguration(int id)
        {
            PawzeConfiguration configuration = db.PawzeConfigurations.Find(id);
            if (configuration == null)
            {
                return NotFound();
            }

            return Ok(configuration);
        }

        // PUT: api/Configurations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConfiguration(int id, PawzeConfiguration configuration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != configuration.PawzeConfigurationId)
            {
                return BadRequest();
            }

            db.Entry(configuration).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigurationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Configurations
        [ResponseType(typeof(PawzeConfiguration))]
        public IHttpActionResult PostConfiguration(PawzeConfiguration configuration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PawzeConfigurations.Add(configuration);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = configuration.PawzeConfigurationId }, configuration);
        }

        // DELETE: api/Configurations/5
        [ResponseType(typeof(PawzeConfiguration))]
        public IHttpActionResult DeleteConfiguration(int id)
        {
            PawzeConfiguration configuration = db.PawzeConfigurations.Find(id);
            if (configuration == null)
            {
                return NotFound();
            }

            db.PawzeConfigurations.Remove(configuration);
            db.SaveChanges();

            return Ok(configuration);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConfigurationExists(int id)
        {
            return db.PawzeConfigurations.Count(e => e.PawzeConfigurationId == id) > 0;
        }
    }
}