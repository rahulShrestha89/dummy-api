using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Helpers;
using Phase2_Group2_selucmps383_sp15_p2_g2.Models;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Repository 
{
    public class GameStoreRepository : IGameStoreRepository
    {
        private GameStoreContext _context;

        public GameStoreRepository(GameStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// return all users
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetAllUsers()
        {
            return _context.Users;
        }

        /// <summary>
        /// return user with a specified user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserById(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId).FirstOrDefault();
        }

        /// <summary>
        /// Lists all games.
        /// </summary>
        /// <returns>Returns all games found.</returns>
        public IQueryable<Game> GetAllGames()
        {
            return  _context.Games
                .AsQueryable();
        }

        /// <summary>
        /// Finds user by their id.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>Returns the game with the specified Id.</returns>
        public Game GetGame(int gameId)
        {
            return _context.Games.Find(gameId);
        }

        /// <summary>
        /// Find game by genre
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns></returns>
        public IQueryable<Game> GetGamesByGenre(int genreId)
        {
            return _context.Games
                    .Where(u => u.Genres.Any(e => e.GenreId == genreId));
        }

        public bool GameExists(int id)
        {
            return _context.Games.Count(e => e.GameId == id) > 0;
        }
        
        /// <summary>
        /// Get all genres
        /// </summary>
        /// <returns></returns>
        public IQueryable<Genre> GetAllGenres()
        {
            return _context.Genres
                .AsQueryable();
        }

        /// <summary>
        /// Get genre by Id
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns></returns>
        public Genre GetGenre(int genreId)
        {
            return _context.Genres.Find(genreId);
        }

        /// <summary>
        /// Get Genre by GameId
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public IQueryable<Genre> GetGenresByGame(int gameId)
        { 
            return _context.Genres 
                    .Where(u => u.Games.Any(e => e.GameId== gameId));
        }

        /// <summary>
        /// Lists all sales.
        /// </summary>
        /// <returns>Returns a queryable of sales.</returns>
        public IQueryable<Sale> GetAllSales()
        {
            return _context.Sales.AsQueryable();
        }

        /// <summary>
        /// Finds a Sale in the db.
        /// </summary>
        /// <param name="saleId"></param>
        /// <returns>Returns the found sale.</returns>
        public Sale GetSaleById(int saleId)
        {
            return _context.Sales.Find(saleId);
        }

        /// <summary>
        /// Adds a sale to the database.
        /// </summary>
        /// <param name="sale"></param>
        public void AddSale(Sale sale)
        {
            _context.Sales.Add(sale);
        }

        /// <summary>
        /// Removes a sale from the db.
        /// </summary>
        /// <param name="sale"></param>
        public void RemoveSale(Sale sale)
        {
            _context.Sales.Remove(sale);
        }


        public bool GenreExists(int genreId)
        {
            return _context.Games.Count(e => e.GameId == genreId) > 0;
        }

        public bool IsAuthorizedUser(string emailAddress, string password)
        {
            var user = _context.Users.Where(u => u.EmailAddress == emailAddress).FirstOrDefault();

            if (user != null)
            {
                if (Crypto.VerifyHashedPassword(user.Password, password))
                {
                    return true;
                }   
            }
            return false;
        }
        
        /// <summary>
        /// Add a new user to the db.
        /// </summary>
        /// <param name="user"></param>
       public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public void UpdateUser(int id, User checkUserInDb)
       {
           _context.Entry(checkUserInDb).State = EntityState.Modified;
       }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            } 
           
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

    }
}