using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Phase2_Group2_selucmps383_sp15_p2_g2.Models;
using System.Web.Http.Description;
using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Controllers
{
    public class GameController : ApiController
    {
        public GameStoreContext _db = new GameStoreContext(); 

        public IQueryable<Game> GetAll()
        {
            return _db.Games;
        }

        [ResponseType(typeof(Game))]
        public IHttpActionResult GetGame(int id)
        {
            Game game = _db.Games.Find(id);
            if(game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }
        
        [ResponseType(typeof(Game))]
        public IHttpActionResult PostGame(Game addedGame)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Games.Add(addedGame);
            _db.SaveChanges();
           
            return CreatedAtRoute("DefaultApi", new { id = addedGame.GameId }, addedGame);
        }

        public IHttpActionResult PutGame(Game game, int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != game.GameId)
            {
                return BadRequest();
            }
            _db.Entry(game).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        [ResponseType(typeof(Game))]
        public IHttpActionResult Delete(int id)
        {
            Game game = _db.Games.Find(id);
            if(game != null)
            {
                _db.Games.Remove(game);
                _db.SaveChanges();
                return Ok(game);
            }
            return NotFound();

        }

        public bool GameExists(int id)
        {
            return _db.Games.Count(e => e.GameId == id) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}
