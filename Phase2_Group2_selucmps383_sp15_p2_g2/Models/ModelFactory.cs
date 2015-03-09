using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private IGameStoreRepository _repository;

        public ModelFactory(HttpRequestMessage request, IGameStoreRepository repository)
        {
            _UrlHelper = new UrlHelper(request);
            _repository = repository; 
        }

        public ModelFactory()
        {
            // TODO: Complete member initialization
        }

        public UserModel Create(User user)
        {
            return new UserModel()
            {
                Url = _UrlHelper.Link("Users", new { email = user.EmailAddress }),
                UserId = user.UserId,
                Email = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                CustomerCart = user.CustomerCart,
                Sales = user.Sales.Select(s => Create(s))
            };
        }

        public SaleModel Create(Sale sale)
        {
            return new SaleModel()
            {
                SaleDate = sale.SaleDate,
                SaleId = sale.SaleId,
                TotalAmount = sale.TotalAmount
            };
           
        }

        public GameModel Create(Game game)
        {
            return new GameModel()
            {
                GameId = game.GameId,
                GamePrice = game.GamePrice,
                GameName = game.GameName,
                ReleaseDate = game.ReleaseDate,
                InventoryCount = game.InventoryCount
            };
        }

        public UserBaseModel CreateUserSummary(User user)
        {
            return new UserBaseModel()
            {
                Url = _UrlHelper.Link("Users", new { email = user.EmailAddress }),
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.EmailAddress,
                Role = user.Role
            };

        }
    }
}