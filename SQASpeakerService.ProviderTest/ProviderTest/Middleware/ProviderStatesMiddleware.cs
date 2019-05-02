using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using SQASpeakerService.Models;
using SQASpeakerService.ProviderTest.ProviderTest.Middleware.MiddlewareBase;

namespace SQASpeakerService.ProviderTest.ProviderTest.Middleware
{
    /// <summary>
    /// This class provides all business logic for creating the preconditions for the provider contract tests.
    /// </summary>
    public class ProviderStatesMiddleware : BaseProviderStateMiddleware
    {
        private readonly IMongoDatabase _database;
        
        private const string DbName = "speakers";

        private const string CollectionName = "speakers";

        public ProviderStatesMiddleware(RequestDelegate next) : base(next)
        {
            _database = new MongoClient().GetDatabase(DbName);
            ProviderStates = new Dictionary<string, Action> {
                {
                    "There are speakers details available for speakers with id 1, 2 and 3",
                    PrepareSpeakersDatabase
                }
            };
            ConsumerName = "loop-energyprofile";
            ResetMethod = ResetEnvironment;
        }

        /// <summary>
        /// Sets up the speakers database mock and specifies its behavior.
        /// </summary>
        private void PrepareSpeakersDatabase()
        {
            var speaker1 = new Speaker("1", "Alexander", "Henze", "MaibornWolff");
            var speaker2 = new Speaker("2", "Maik", "Nogens", "MaibornWolff");
            var speaker3 = new Speaker("3", "Joachim", "Basler", "MaibornWolff");

            var collection = _database.GetCollection<Speaker>(CollectionName);
            
            collection.InsertMany(new [] {speaker1, speaker2, speaker3});
        }

        /// <summary>
        /// Resets all environmental systems like service mocks and databases.
        /// </summary>
        private void ResetEnvironment()
        {
            _database.DropCollection(CollectionName);
        }
    }
}
