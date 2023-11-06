using BeerRatings.Core.Common;
using BeerRatings.Core.Domians;
using BeerRatings.Core.Entities;
using BeerRatings.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerRatings.Infrastracture.Repositories
{
    public class BeerRatingsRepository : IBeerRatingsRepository
    {
        private readonly string filePath = string.Empty;
        private readonly JsonSerializerSettings settings;
        public BeerRatingsRepository()
        {
            settings = new JsonSerializerSettings();
            settings.ContractResolver = new IgnoreJsonAttributesResolver();
            settings.Formatting = Formatting.Indented;
            filePath = ConfigurationManager.AppSettings["DataFilePath"];
        }
        public Task<List<UserRating>> GetBeerRatingsById(int beerId)
        {
            List<UserRating> userRatings = GetAllUserRatings();
                
            return Task.FromResult(userRatings.Where(c => c.Id == beerId).ToList());
        }

        public Task<bool> SaveBeerRating(UserRating userRating)
        {
            List<UserRating> userRatings = GetAllUserRatings();
            userRatings.Add(userRating);
            string jsonString = JsonConvert.SerializeObject(userRatings, settings);
            File.WriteAllText(filePath, jsonString);
            return Task.FromResult(true);
        }

        private List<UserRating> GetAllUserRatings()
        {
            List<UserRating> userRatings = new List<UserRating>();
            if (File.Exists(filePath))
            {
                string fileJson = File.ReadAllText(filePath);
                if (string.IsNullOrEmpty(fileJson))
                    return userRatings;
                userRatings = JsonConvert.DeserializeObject<List<UserRating>>(fileJson, settings);
            }
            return userRatings;
        }
    }
}
