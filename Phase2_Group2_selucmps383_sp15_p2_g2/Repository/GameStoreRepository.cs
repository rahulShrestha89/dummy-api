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

        /*
         * Users Methods
         */
        #region
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
        /// Removes a user from the db.
        /// </summary>
        /// <param name="user"></param>
        public void RemoveUser(User user)
        {
            _context.Users.Remove(user);
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Count(u => u.UserId == userId) > 0;
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

        //Want to check this before adding intellisense comments.
        public void UpdateUser(User checkUserInDb)
        {
            _context.Entry(checkUserInDb).State = EntityState.Modified;
        }
        #endregion 

        /*
         * Game Methods
         */
        #region
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
        /// Finds game by id.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>Returns the game with the specified Id.</returns>
        public Game GetGame(int gameId)
        {
            return _context.Games.Where(g => g.GameId == gameId).FirstOrDefault();
        }

        public void UpdateGame(Game gameInDb)
        {
            _context.Entry(gameInDb).State = EntityState.Modified;
        }

        /// <summary>
        /// Adds a game to the db.
        /// </summary>
        /// <param name="game"></param>
        public void AddGame(Game game)
        {
            _context.Games.Add(game);
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

        public bool GameExists(int gameId)
        {
            return _context.Games.Count(e => e.GameId == gameId) > 0;
        }

        /// <summary>
        /// Removes a game from the db.
        /// </summary>
        /// <param name="game"></param>
        public void RemoveGame(Game game)
        {
            _context.Games.Remove(game);
        }

        public Game GetGame(string gameName)
        {
            return _context.Games.Where(g => g.GameName.Equals(gameName)).FirstOrDefault();
        }
        #endregion
        
       

        /*
         * Sale Methods
         */
        #region

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
        public Sale GetSale(int saleId)
        {
            return _context.Sales.Where(s => s.SaleId == saleId).FirstOrDefault();
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

        public void UpdateSale(Sale saleInDb)
        {
            _context.Entry(saleInDb).State = EntityState.Modified;
        }

        public bool SaleExists(int saleId)
        {
            return _context.Sales.Count(s => s.SaleId == saleId) > 0;
        }
        #endregion

        /*
         * Genre Methods
         */
        #region
        /// <summary>
        /// Get all genres
        /// </summary>
        /// <returns></returns>
        public IQueryable<Genre> GetAllGenres()
        {
            return _context.Genres
                .AsQueryable();
        }

        public void AddGenre(Genre genre)
        {
            _context.Genres.Add(genre);
        }

        public Genre GetGenre(string genreName)
        {
            return _context.Genres.Where(g => g.GenreName.Equals(genreName)).FirstOrDefault();
        }

        public bool GenreExists(int genreId)
        {
            return _context.Games.Count(e => e.GameId == genreId) > 0;
        }

        /// <summary>
        /// Get Genre by GameId
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public IQueryable<Genre> GetGenresByGame(int gameId)
        {
            return _context.Genres
                    .Where(u => u.Games.Any(e => e.GameId == gameId));
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

        public void UpdateGenre(Genre genreInDb)
        {
            _context.Entry(genreInDb).State = EntityState.Modified;
        }
        #endregion

        /*
         * Tag Methods
         */
        #region
        public IQueryable<Tag> GetAllTags()
        {
            return _context.Tags
                .AsQueryable();
        }

        public void AddTag(Tag tag)
        {
            _context.Tags.Add(tag);
        }

        public bool TagExists(int tagId)
        {
            return _context.Tags.Count(s => s.TagId == tagId) > 0;
        }

        public Tag GetTag(int tagId)
        {
            return _context.Tags.Where(s => s.TagId == tagId).FirstOrDefault();
        }

        public void RemoveTag(Tag tag)
        {
            _context.Tags.Remove(tag);
        }

        public void UpdateTag(Tag tagInDb)
        {
            _context.Entry(tagInDb).State = EntityState.Modified;
        }
        #endregion

        /*
         * Cart Methods
         */ 
        #region
        public IQueryable<Cart> GetAllCarts()
        {
            return _context.Carts
                .AsQueryable();
        }

        public void AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public bool CartExists(int cartId)
        {
            return _context.Carts.Count(s => s.CartId == cartId) > 0;
        }

        public Cart GetCart(int cartId)
        {
            return _context.Carts.Where(s => s.CartId == cartId).FirstOrDefault();
        }

        public void RemoveCart(Cart cart)
        {
            _context.Carts.Remove(cart);
        }

        public void UpdateCart(Cart cartInDb)
        {
            _context.Entry(cartInDb).State = EntityState.Modified;
        }
        #endregion

        /*
         * Misc. Methods
         */
        #region
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
        #endregion
    }
}