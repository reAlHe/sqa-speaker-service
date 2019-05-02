using System.Collections.Generic;
using SQASpeakerService.Models;

namespace SQASpeakerService.Repository
{
    public interface ISpeakersRepository
    {
        IEnumerable<Speaker> GetSpeakersById(IEnumerable<string> speakerIds);
    }
}
