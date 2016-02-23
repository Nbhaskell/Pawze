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
    public class InventoriesController : ApiController
    {
        private PawzeDataContext db = new PawzeDataContext();

        // GET: api/Inventories
        public IEnumerable<InventoriesModel> GetInventories()
        {
            return Mapper.Map<IEnumerable<InventoriesModel>>(
                db.Inventories
                );
        }

        // GET: api/Inventories/5
        [ResponseType(typeof(InventoriesModel))]
        public IHttpActionResult GetInventory(int id)
        {
            Inventory dbInventory = db.Inventories.Find(id);
            if (dbInventory == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<InventoriesModel>(dbInventory));
        }

        // PUT: api/Inventories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInventory(int id, InventoriesModel inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventory.InventoryId)
            {
                return BadRequest();
            }

            Inventory dbInventory = db.Inventories.Find(id);
            dbInventory.Update(inventory);

            db.Entry(dbInventory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

        // POST: api/Inventories
        [ResponseType(typeof(InventoriesModel))]
        public IHttpActionResult PostInventory(InventoriesModel inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbInventory = new Inventory();
            db.Inventories.Add(dbInventory);

            db.SaveChanges();

            inventory.InventoryId = dbInventory.InventoryId;

            return CreatedAtRoute("DefaultApi", new { id = inventory.InventoryId }, inventory);
        }

        // DELETE: api/Inventories/5
        [ResponseType(typeof(InventoriesModel))]
        public IHttpActionResult DeleteInventory(int id)
        {
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return NotFound();
            }

            db.Inventories.Remove(inventory);
            db.SaveChanges();

            return Ok(Mapper.Map<InventoriesModel>(inventory));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InventoryExists(int id)
        {
            return db.Inventories.Count(e => e.InventoryId == id) > 0;
        }
    }
}