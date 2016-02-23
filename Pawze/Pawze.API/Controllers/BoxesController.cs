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
using Pawze.API.Models;
using AutoMapper;

namespace Pawze.API.Controllers
{
    public class BoxesController : ApiController
    {
        private PawzeDataContext db = new PawzeDataContext();

        // GET: api/Boxes
        public IEnumerable<BoxesModel> GetBoxes()
        {
            return Mapper.Map<IEnumerable<BoxesModel>>( 
                db.Boxes.Where(b => b.PawzeUser.UserName == User.Identity.Name)
            );
        }

        // GET: api/Boxes/5
        [ResponseType(typeof(BoxesModel))]
        public IHttpActionResult GetBox(int id)
        {
            // Box box = db.Boxes.Find(id);
            Box dbBox = db.Boxes.FirstOrDefault(b => b.PawzeUser.UserName == User.Identity.Name && b.BoxId == id);
            if (dbBox == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<BoxesModel>(dbBox));
        }

        // PUT: api/Boxes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBox(int id, BoxesModel box)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Box dbBox = db.Boxes.FirstOrDefault(b => b.PawzeUser.UserName == User.Identity.Name && b.BoxId == id);

            if (id != box.BoxId)
            {
                return BadRequest();
            }

            dbBox.Update(box);

            db.Entry(dbBox).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoxExists(id))
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

        // POST: api/Boxes
        [ResponseType(typeof(BoxesModel))]
        public IHttpActionResult PostBox(BoxesModel box)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbBox = new Box();

            dbBox.PawzeUser = db.PawzeUsers.FirstOrDefault(u => u.UserName == User.Identity.Name);

            dbBox.Update(box);

            db.Boxes.Add(dbBox);
            db.SaveChanges();

            box.BoxId = dbBox.BoxId;

            return CreatedAtRoute("DefaultApi", new { id = box.BoxId }, box);
        }

        // DELETE: api/Boxes/5
        [ResponseType(typeof(BoxesModel))]
        public IHttpActionResult DeleteBox(int id)
        {
            // Box box = db.Boxes.Find(id);
            Box dbBox = db.Boxes.FirstOrDefault(b => b.PawzeUser.UserName == User.Identity.Name && b.BoxId == id);

            if (dbBox == null)
            {
                return NotFound();
            }

            db.Boxes.Remove(dbBox);
            db.SaveChanges();

            return Ok(Mapper.Map<BoxesModel>(dbBox));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BoxExists(int id)
        {
            return db.Boxes.Count(e => e.BoxId == id) > 0;
        }
    }
}