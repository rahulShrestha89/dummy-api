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

        IQueryable<Genre> GetGenresByGame(int gameId);

        IQueryable<Game> GetGamesByGenre(int genreId);

        IQueryable<Genre> GetAllGenres();
        Genre GetGenre(int genreId);    // if necessary use bool to check whether a genre belongs to any game
        bool GenreExists(int genreId);
        Genre GetGenre(string genreName);

        // for  api/user controllers
        IQueryable<User> GetAllUsers();
        User GetUserById(int userId);
        void AddUser(User user);
        void UpdateUser(User checkUserInDb);
        bool UserExists(int userId);
        void RemoveUser(User user);

        // for api/sale controllers
        IQueryable<Sale> GetAllSales();
        Sale GetSaleById(int saleId);
        void AddSale(Sale sale);
        void RemoveSale(Sale sale);
        void UpdateSale(Sale saleInDb);
        bool SaleExists(int saleId);

        bool IsAuthorizedUser(string emailAddress, string password);

        bool SaveAll();
    }
}
