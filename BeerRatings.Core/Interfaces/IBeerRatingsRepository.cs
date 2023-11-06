using BeerRatings.Core.Domians;
using BeerRatings.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerRatings.Core.Interfaces
{
    public interface IBeerRatingsRepository
    {
        Task<List<UserRating>> GetBeerRatingsById(int beerId);
        Task<bool> SaveBeerRating(UserRating beerRating);

    }
}
