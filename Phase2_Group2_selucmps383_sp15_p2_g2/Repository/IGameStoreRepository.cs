using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public interface IGameStoreRepository
    {
        // for api/game controllers
        IQueryable<Game> GetAllGames();
        Game GetGame(int gameId);
        void AddGame(Game game);
        void UpdateGame(Game gamInDb);
        bool GameExists(int gameId);
        void RemoveGame(Game game);
        Game GetGame(string gameName);

        IQueryable<Genre> GetGenresByGame(int gameId);

        IQueryable<Game> GetGamesByGenre(int genreId);

        //for api/genre controllers
        IQueryable<Genre> GetAllGenres();
        Genre GetGenre(int genreId);    // if necessary use bool to check whether a genre belongs to any game
        bool GenreExists(int genreId);
        Genre GetGenre(string genreName);
        void UpdateGenre(Genre genreInDb);
        void AddGenre(Genre genre);

        // for  api/user controllers
        IQueryable<User> GetAllUsers();
        User GetUserById(int userId);
        void AddUser(User user);
        void UpdateUser(User checkUserInDb);
        bool UserExists(int userId);
        void RemoveUser(User user);

        // for api/sale controllers
        IQueryable<Sale> GetAllSales();
        Sale GetSale(int saleId);
        void AddSale(Sale sale);
        void RemoveSale(Sale sale);
        void UpdateSale(Sale saleInDb);
        bool SaleExists(int saleId);

        //for api/tag controllers
        IQueryable<Tag> GetAllTags();
        Tag GetTag(int tagId);
        void AddTag(Tag tag);
        void RemoveTag(Tag tag);
        void UpdateTag(Tag tagInDb);
        bool TagExists(int tagId);

        //for api/cart controllers
        IQueryable<Cart> GetAllCarts();
        Cart GetCart(int cartId);
        void AddCart(Cart cart);
        void RemoveCart(Cart cart);
        void UpdateCart(Cart cartInDb);
        bool CartExists(int cartId);

        bool IsAuthorizedUser(string emailAddress, string password);

        bool SaveAll();
    }
}
