using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class GameStoreRepository : IGameStoreRepository
    {
        private GameStoreContext _db = new GameStoreContext(); //Named the List _db so the transfer will go smoothly when database is up.


        /// <summary>
        /// Lists all games.
        /// </summary>
        /// <returns>Returns all games found.</returns>
        public IQueryable<Game> GetAll()
        {
            return _db.Games;
        }

        /// <summary>
        /// Finds user by their id.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>Returns the user with the specified Id.</returns>
        public Game Get(int gameId)
        {
            return _db.Games.FirstOrDefault(g => g.GameId == gameId);
        }

        public Game Add(Game game)
        {
            if(game == null)
            {
                throw new ArgumentNullException("Game");
            }
            _db.Games.Add(game);
            _db.SaveChanges();
            return game;
        }

        public void Remove(int id)
        {
            Game removeMe = _db.Games.Find(id);
            _db.Games.Remove(removeMe);
            _db.SaveChanges();
        }

        public bool Update(Game game, int id)
        {
            if(game==null)
            {
                throw new ArgumentNullException("Game");
            }
            if(game.GameId != id)
            {
                return false;
            }
            _db.Entry(game).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
  
            return true;
        }

        public bool GameExists(int id)
        {
            return _db.Games.Count(e => e.GameId == id) > 0;
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            } 
           
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        

    }
}