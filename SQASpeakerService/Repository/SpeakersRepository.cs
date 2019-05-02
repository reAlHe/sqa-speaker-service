using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SQASpeakerService.Models;
using Enumerable = System.Linq.Enumerable;

namespace SQASpeakerService.Repository
{
    public class SpeakersRepository : ISpeakersRepository
    {
        private readonly IMongoCollection<Speaker> _collection;

        private const string DatabaseName = "speakers";
        
        private const string CollectionName = "speakers";

        public SpeakersRepository()
        {
            var client = new MongoClient();
            var database = client.GetDatabase(DatabaseName);
            _collection = database.GetCollection<Speaker>(CollectionName);
        }
        
        public IEnumerable<Speaker> GetSpeakersById(IEnumerable<int> speakerIds)
        {
            return _collection.AsQueryable().Where(speaker => Enumerable.Contains(speakerIds, speaker.Id)).ToList();
        }
    }
}
