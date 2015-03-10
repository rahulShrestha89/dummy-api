using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Phase2_Group2_selucmps383_sp15_p2_g2.Models;
using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;
using Phase2_Group2_selucmps383_sp15_p2_g2.Controllers;
using Phase2_Group2_selucmps383_sp15_p2_g2.Authentication;
using System.Web.Helpers;
using Phase2_Group2_selucmps383_sp15_p2_g2.Enums;
using System.Data.Entity.Infrastructure;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Areas.API.Controllers
{
    public class GenreController : ApiController
    {
        IGameStoreRepository _repo;
        ModelFactory _modelFactory;

        public GameStoreContext db = new GameStoreContext(); 

        public GenreController()
        {

        }

        public GenreController(IGameStoreRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }

        // GET api/Genre
        [System.Web.Http.ActionName("GetAllGenres")]
        public IQueryable<Genre> GetAllGenres()
        {
            return _repo.GetAllGenres();
        }

        // GET api/Genre/5
        [System.Web.Http.ActionName("GetGenre")]
        [ResponseType(typeof(Genre))]
        public IHttpActionResult GetGenre(int genreId)
        {
            Genre genre = _repo.GetGenre(genreId);
            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        // PUT api/Genre/5
        [System.Web.Http.ActionName("PutGenre")]
        [RoleAuthentication("StoreAdmin")]
        [ResponseType(typeof(Genre))]
        public IHttpActionResult PutGenre(int genreId, Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (genreId != genre.GenreId)
            {
                return BadRequest();
            }

            var genreInDb = _repo.GetGenre(genreId);

            if(genre.GenreName != null)
            {
                genreInDb.GenreName = genre.GenreName;
            }

            var gamesToCheck = genre.Games;

            if(gamesToCheck != null)
            {
                foreach(var g in gamesToCheck)
                {
                    if(!genreInDb.Games.Contains(g))
                    {
                        if (!_repo.GameExists(g.GameId))
                        {
                            _repo.AddGame(g);
                        }
                        genreInDb.Games.Add(g);
                    }
                }
            }

            _repo.UpdateGenre(genreInDb);

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
            catch (DbUpdateConcurrencyException)
            {
                if (!_repo.GenreExists(genreId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST api/Genre
        [ResponseType(typeof(Genre))]
        public IHttpActionResult PostGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Genres.Add(genre);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = genre.GenreId }, genre);
        }

        // DELETE api/Genre/5
        [ResponseType(typeof(Genre))]
        public IHttpActionResult DeleteGenre(int id)
        {
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }

            db.Genres.Remove(genre);
            db.SaveChanges();

            return Ok(genre);
        }

    }
}