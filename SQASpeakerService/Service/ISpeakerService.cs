using System.Collections.Generic;
using SQASpeakerService.Models;

namespace SQASpeakerService.Service
{
    public interface ISpeakerService
    {
        IEnumerable<Speaker> FetchSpeakerDetails(IEnumerable<string> speakerIds);
    }
}
