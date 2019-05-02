using System.Collections.Generic;
using SQASpeakerService.Models;
using SQASpeakerService.Repository;

namespace SQASpeakerService.Service
{
    /// <summary>
    /// This class is responsible for retrieving conference details like speakers.
    /// </summary>
    public class SpeakerService : ISpeakerService
    {
        private readonly ISpeakersRepository _speakersRepository;

        public SpeakerService(ISpeakersRepository speakersRepository)
        {
            _speakersRepository = speakersRepository;
        }

        /// <summary>
        /// Fetches all speaker details for given ids.
        /// </summary>
        /// <param name="speakerIds">The speaker ids</param>
        /// <returns>A list containing all speaker details</returns>
        public IEnumerable<Speaker> FetchSpeakerDetails(IEnumerable<string> speakerIds)
        {
            return _speakersRepository.GetSpeakersById(speakerIds);
        }
    }
}
