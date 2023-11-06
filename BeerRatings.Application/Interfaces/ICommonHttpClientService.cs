using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerRatings.Application.Interfaces
{
    public interface ICommonHttpClientService
    {
        Task<string> MakeGetRequestAsync(string endPoint);
    }
}
