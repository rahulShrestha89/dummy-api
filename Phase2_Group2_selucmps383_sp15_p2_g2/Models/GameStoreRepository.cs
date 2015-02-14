using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class GameStoreRepository : IGameStoreRepository
    {
        private List<Game> _db = new List<Game>(); //Named the List _db so the transfer will go smoothly when database is up.
        private int _nextId = 1;

        public GameStoreRepository()
        {
           _db.Add(new Game { Name = "Mario"});
           _db.Add(new Game { Name = "Luigi"});
           _db.Add(new Game { Name = "FunGame"});
        }

        /// <summary>
        /// Lists all games.
        /// </summary>
        /// <returns>Returns all games found.</returns>
        public IEnumerable<Game> GetAll()
        {
            return _db;
        }

        /// <summary>
        /// Finds user by their id.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>Returns the user with the specified Id.</returns>
        public Game Get(int gameId)
        {
            return _db.Find(r => r.Id == gameId);
        }

        public Game Add(Game addedGame)
        {
            if(addedGame == null)
            {
                throw new ArgumentNullException("Game");
            }
            addedGame.Id = _nextId++;
            _db.Add(addedGame);
            return addedGame;
        }

        public void Remove(int gameId)
        {
            _db.RemoveAll(r => r.Id == gameId);
        }

        public bool Update(Game updatedGame)
        {
            if(updatedGame == null)
            {
                throw new ArgumentNullException("Game");
            }
            int index = _db.FindIndex(r => r.Id == updatedGame.Id);
            if(index == -1)
            {
                return false;
            }
            _db.RemoveAt(index);
            _db.Add(updatedGame);
            return true;
        }

        public string GetApiKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[16];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

    }
}