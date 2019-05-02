using System.Linq;
using FluentAssertions;
using Moq;
using SQASpeakerService.Models;
using SQASpeakerService.Repository;
using SQASpeakerService.Service;
using Xunit;

namespace SQASpeakerService.Test.Service
{
    public class SpeakerServiceTest
    {
        private readonly ISpeakerService _speakerService;

        private readonly Mock<ISpeakersRepository> _speakersRepositoryMock;

        public SpeakerServiceTest()
        {
            _speakersRepositoryMock = new Mock<ISpeakersRepository>();
            _speakerService = new SpeakerService(_speakersRepositoryMock.Object);
        }
        
        [Fact]
        public void FetchSpeakerDetailsReturnsCorrectSpeakerDetails()
        {
            var speakerIds = new[] {"1", "2", "3"};
            var speakerDetails1 = new Speaker("1", "Alexander", "Alexander", "MaibornWolff");
            var speakerDetails2 = new Speaker("2", "Maik", "Nogens", "MaibornWolff");
            var speakerDetails3 = new Speaker("3", "Joachim", "Basler", "MaibornWolff");
            var speakers = new[] {speakerDetails1, speakerDetails2, speakerDetails3};

            _speakersRepositoryMock.Setup(mock => mock.GetSpeakersById(speakerIds)).Returns(speakers);

            var result = _speakerService.FetchSpeakerDetails(speakerIds);

            result.Count().Should().Be(3);
            result.Should().Contain(speakerDetails1);
            result.Should().Contain(speakerDetails2);
            result.Should().Contain(speakerDetails3);
        }
    }
}
