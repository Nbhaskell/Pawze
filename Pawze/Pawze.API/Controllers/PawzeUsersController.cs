﻿using System;
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
    public class PawzeUsersController : ApiController
    {
        private PawzeDataContext db = new PawzeDataContext();

        // GET: api/PawzeUsers
        public IEnumerable<PawzeUsersModel> GetPawzeUsers()
        {
            return Mapper.Map<IEnumerable<PawzeUsersModel>>(
                db.PawzeUsers
            );
        }

        // GET: api/PawzeUsers/5
        [ResponseType(typeof(PawzeUsersModel))]
        public IHttpActionResult GetPawzeUser(string id)
        {
            PawzeUser dbPawzeUser = db.PawzeUsers.Find(id);

            if (dbPawzeUser == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PawzeUsersModel>(dbPawzeUser));
        }

        // PUT: api/PawzeUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPawzeUser(string id, PawzeUsersModel pawzeUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pawzeUser.PawzeUserId)
            {
                return BadRequest();
            }

            PawzeUser dbPawzeUser = db.PawzeUsers.Find(id);
            dbPawzeUser.Update(pawzeUser);

            db.Entry(dbPawzeUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PawzeUserExists(id))
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

        // POST: api/PawzeUsers
        [ResponseType(typeof(PawzeUsersModel))]
        public IHttpActionResult PostPawzeUser(PawzeUsersModel pawzeUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbPawzeUser = new PawzeUser();
            db.PawzeUsers.Add(dbPawzeUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PawzeUserExists(pawzeUser.PawzeUserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            pawzeUser.PawzeUserId = dbPawzeUser.PawzeUserId;

            return CreatedAtRoute("DefaultApi", new { id = pawzeUser.PawzeUserId }, pawzeUser);
        }

        // DELETE: api/PawzeUsers/5
        [ResponseType(typeof(PawzeUsersModel))]
        public IHttpActionResult DeletePawzeUser(string id)
        {
            PawzeUser pawzeUser = db.PawzeUsers.Find(id);
            if (pawzeUser == null)
            {
                return NotFound();
            }

            db.PawzeUsers.Remove(pawzeUser);
            db.SaveChanges();

            return Ok(Mapper.Map<PawzeUsersModel>(pawzeUser));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PawzeUserExists(string id)
        {
            return db.PawzeUsers.Count(e => e.PawzeUserId == id) > 0;
        }
    }
}