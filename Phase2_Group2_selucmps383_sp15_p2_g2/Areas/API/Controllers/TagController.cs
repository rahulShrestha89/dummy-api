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
    public class TagController : BaseApiController
    {
        IGameStoreRepository _repo;
        ModelFactory _modelFactory;

        public GameStoreContext db = new GameStoreContext(); 

        public TagController()
        {

        }

        public TagController(IGameStoreRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }
       

        // GET api/Tag
        [System.Web.Http.ActionName("GetAllTags")]
        public IQueryable<Tag> GetAllTags()
        {
            return _repo.GetAllTags();
        }

        // GET api/Tag/5
        [System.Web.Http.ActionName("GetTag")]
        [ResponseType(typeof(Tag))]
        public IHttpActionResult GetTag(int tagId)
        {
            Tag tag = _repo.GetTag(tagId);
            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        // PUT api/Tag/5
        [RoleAuthentication("StoreAdmin")]
        [ResponseType(typeof(Tag))]
        [System.Web.Http.ActionName("PutTag")]
        public IHttpActionResult PutTag(int tagId, Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (tagId != tag.TagId)
            {
                return BadRequest();
            }

            var tagInDb = _repo.GetTag(tagId);
            var gamesToCheck = tag.Games;

            if(tag.TagName != null)
            {
                tagInDb.TagName = tag.TagName;
            }

            if(gamesToCheck != null)
            {
                foreach (var g in gamesToCheck)
                {
                    if(!tagInDb.Games.Contains(g))
                    {
                        if(!_repo.GameExists(g.GameId))
                        {
                            _repo.AddGame(g);
                        }
                        tagInDb.Games.Add(g);
                    }
                }
            }


            _repo.UpdateTag(tagInDb);

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
                if (!_repo.TagExists(tagId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST api/Tag
        [System.Web.Http.HttpPost]
        [RoleAuthentication("StoreAdmin")]
        [ValidateAntiForgeryToken]
        [System.Web.Http.ActionName("PostTag")]
        [ResponseType(typeof(Tag))]
        public IHttpActionResult PostTag(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.AddTag(tag);
            _repo.SaveAll();

            return CreatedAtRoute("DefaultApi", new { id = tag.TagId }, tag);
        }

        // DELETE api/Tag/5
        [RoleAuthentication("StoreAdmin")]
        [ResponseType(typeof(Tag))]
        [System.Web.Http.ActionName("DeleteTag")]
        public IHttpActionResult DeleteTag(int tagId)
        {
            Tag tag = _repo.GetTag(tagId);
            if (tag == null)
            {
                return NotFound();
            }

            var gamesWithTag = tag.Games;

            foreach(var g in gamesWithTag)
            {
                g.Tags.Remove(tag);
            }

            _repo.RemoveTag(tag);
            _repo.SaveAll();

            return Ok(tag);
        }
    }
}