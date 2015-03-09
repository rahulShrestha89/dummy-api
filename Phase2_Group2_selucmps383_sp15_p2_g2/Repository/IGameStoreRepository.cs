using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public interface IGameStoreRepository
    {
        IQueryable<Game> GetAllGames();
        Game GetGame(int gameId);
        bool GameExists(int gameId);

        IQueryable<Genre> GetGenresByGame(int gameId);

        IQueryable<Game> GetGamesByGenre(int genreId);

        IQueryable<Genre> GetAllGenres();
        Genre GetGenre(int genreId);    // if necessary use bool to check whether a genre belongs to amy game
        bool GenreExists(int genreId);

        // for  api/user controllers
        IQueryable<User> GetAllUsers();
        User GetUserById(int userId);
        void AddUser(User user);
        void UpdateUser(User checkUserInDb);

        // for api/sale controllers
        IQueryable<Sale> GetAllSales();
        Sale GetSaleById(int saleId);
        void AddSale(Sale sale);
        void RemoveSale(Sale sale);

        bool IsAuthorizedUser(string emailAddress, string password);

        bool SaveAll();
    }
}
