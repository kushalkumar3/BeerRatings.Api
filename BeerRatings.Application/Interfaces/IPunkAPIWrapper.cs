using BeerRatings.Core.Entities;
using System.Threading.Tasks;

namespace BeerRatings.Application.Interfaces
{
    public interface IPunkAPIWrapper
    {
        Task<bool> IsValidBeerId(int beerID);
        Task<Beer> GetBeerByName(string beerName);
    }
}
