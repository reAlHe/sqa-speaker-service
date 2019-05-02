using System.Collections.Generic;
using Nancy;
using SQASpeakerService.Service;

namespace SQASpeakerService.Api
{
    public sealed class SpeakerModule : NancyModule
    {
        private readonly ISpeakerService _speakerService;
        
        public SpeakerModule(ISpeakerService speakerService) : base("/speakers")
        {
            _speakerService = speakerService;
            
            Get("",parameters =>
            {
                var ids = SplitIds(Request.Query["id"].Value as string);
                var speakerDetails = _speakerService.FetchSpeakerDetails(ids);
                return Response.AsJson(speakerDetails).WithStatusCode(HttpStatusCode.OK);
            });
        }

        private static IEnumerable<string> SplitIds(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return new List<string>();
            }
            return ids.Split(",");
        }
    }
}
