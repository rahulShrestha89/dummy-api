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
using Phase2_Group2_selucmps383_sp15_p2_g2.Authentication;
using System.Web.Mvc;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Areas.API.Controllers
{
    public class GameController : BaseApiController
    {
        IGameStoreRepository _repo;
        ModelFactory _modelFactory;

        public GameStoreContext _db = new GameStoreContext(); 

        public GameController()
        {

        }

        public GameController(IGameStoreRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }

        [System.Web.Http.ActionName("GetAllGames")]
        public IQueryable<GameModel> GetAll()
        {
            return _repo.GetAllGames().Select(g => _modelFactory.Create(g));
        }
       

        [System.Web.Http.ActionName("GetGame")]
        [ResponseType(typeof(GameModel))]
        public IHttpActionResult GetGame(int gameId)
        {
            GameModel game = _modelFactory.Create(_repo.GetGame(gameId));
            if(game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [System.Web.Http.ActionName("GetGamesByGenre")]
        public IQueryable GetGamesByGenre(string genreName)
        {
            var genre = _repo.GetGenre(genreName);
            return genre.Games.AsQueryable();
            
        }

        [System.Web.Http.HttpPost]
        [RoleAuthentication("StoreAdmin")]
        [ValidateAntiForgeryToken]
        [ResponseType(typeof(Game))]
        [System.Web.Http.ActionName("PostGame")]
        [ResponseType(typeof(Game))]
        public IHttpActionResult PostGame(Game game)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.AddGame(game);
            _repo.SaveAll();
           
            return CreatedAtRoute("DefaultApi", new { id = game.GameId }, game);
        }

        [RoleAuthentication("StoreAdmin")]
        [ResponseType(typeof(Game))]
        [System.Web.Http.ActionName("PutGame")]
        public IHttpActionResult PutGame([FromBody]Game game, int gameId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(gameId != game.GameId)
            {
                return BadRequest();
            }
            var gameInDb = _repo.GetGame(gameId);

            if (gameInDb == null)
            {
                return NotFound();
            }

            //Redundant checks for differences between game being put and one in db.
            #region
            if (game.GameName != null)
            {
                gameInDb.GameName = game.GameName;
            }

            if(game.ReleaseDate != null)
            {
                gameInDb.ReleaseDate = game.ReleaseDate;
            }

            if(game.GamePrice != null)
            {
                gameInDb.GamePrice = game.GamePrice;
            }

            if(game.InventoryCount != null)
            {
                gameInDb.InventoryCount = game.InventoryCount;
            }
            #endregion 
            //end Redundant checks.

            //Code to check if items are in the collection.
            #region
            var genresToAdd = game.Genres;
            var tagsToAdd = game.Tags;

           
            if (genresToAdd != null)
            {
                foreach (var g in genresToAdd)
                {
                    if(!gameInDb.Genres.Contains(g)) //check if the item is not in the collection of genres
                    {
                        gameInDb.Genres.Add(g); //if not add it to the collection.
                    }
                }
            }
            
            if(tagsToAdd != null)
            {
                foreach(var t in tagsToAdd)
                {
                    if (!gameInDb.Tags.Contains(t)) //check if the item is not in the collection of tags.
                    {
                        gameInDb.Tags.Add(t); //if not add it to the collection.
                    }
                }
            }
            #endregion
            //end of checks.

            _repo.UpdateGame(gameInDb);

            try
            {
                if(_repo.SaveAll())
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!_repo.GameExists(gameId))
                {
                    return NotFound();
                }
                else
                { 
                    throw; 
                }
             
            }

        }

        [ResponseType(typeof(Game))]
        public IHttpActionResult Delete(int id)
        {
            Game game = _db.Games.Find(id);
            if(game != null)
            {
                _repo.RemoveGame(game);
                _repo.SaveAll();
                return Ok(game);
            }
            return NotFound();

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
