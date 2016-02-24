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
using AutoMapper;
using Pawze.API.Models;

namespace Pawze.API.Controllers
{
    [Authorize]
    public class BoxItemsController : ApiController
    {
        private PawzeDataContext db = new PawzeDataContext();

        // GET: api/BoxItems
        public IEnumerable<BoxItemsModel> GetBoxItems()
        {
            return Mapper.Map<IEnumerable<BoxItemsModel>>(
                db.BoxItems.Where(b => b.Box.PawzeUser.UserName == User.Identity.Name)
                );
        }

        // GET: api/BoxItems/5
        [ResponseType(typeof(BoxItemsModel))]
        public IHttpActionResult GetBoxItem(int id)
        {
            BoxItem dbBoxItem = db.BoxItems.FirstOrDefault(b => b.Box.PawzeUser.UserName == User.Identity.Name && b.BoxItemId == id);
            if (dbBoxItem == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<BoxItemsModel>(dbBoxItem));
        }

        // PUT: api/BoxItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBoxItem(int id, BoxItemsModel boxItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BoxItem dbBoxItem = db.BoxItems.FirstOrDefault(b => b.Box.PawzeUser.UserName == User.Identity.Name && b.BoxItemId == id);

            if (id != boxItem.BoxItemId)
            {
                return BadRequest();
            }

            dbBoxItem.Update(boxItem);

            db.Entry(dbBoxItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoxItemExists(id))
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

        // POST: api/BoxItems
        [ResponseType(typeof(BoxItemsModel))]
        public IHttpActionResult PostBoxItem(BoxItemsModel boxItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbBoxItem = new BoxItem();

            dbBoxItem.Update(boxItem);

            db.BoxItems.Add(dbBoxItem);
            db.SaveChanges();

            boxItem.BoxItemId = dbBoxItem.BoxItemId;

            return CreatedAtRoute("DefaultApi", new { id = boxItem.BoxItemId }, boxItem);
        }

        // DELETE: api/BoxItems/5
        [ResponseType(typeof(BoxItemsModel))]
        public IHttpActionResult DeleteBoxItem(int id)
        {
            BoxItem dbBoxItem = db.BoxItems.FirstOrDefault(b => b.Box.PawzeUser.UserName == User.Identity.Name && b.BoxItemId == id);
            if (dbBoxItem == null)
            {
                return NotFound();
            }

            db.BoxItems.Remove(dbBoxItem);
            db.SaveChanges();

            return Ok(Mapper.Map<BoxItemsModel>(dbBoxItem));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BoxItemExists(int id)
        {
            return db.BoxItems.Count(e => e.BoxItemId == id) > 0;
        }
    }
}