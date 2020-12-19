using Moq;
using System.Collections.Generic;
using ScoreBoard.Storage;
using Xunit;

namespace ScoreBoard.Tests
{
    public class ScoreBoardTests
    {
        [Theory]
        [InlineData("Ukraine", "Poland")]
        public void StartGameWithDefaultScoreTest(string team1, string team2)
        {
            //Arrange
            var mockStorage = new Mock<IGamesStorage>();
            mockStorage.Setup(p => p.AddGame(team1, team2))
                .Verifiable();

            //Act
            var board = new ScoreBoard(mockStorage.Object);
            board.StartGame(team1, team2);

            //Assert
            mockStorage.Verify(p => p.AddGame(team1,team2), Times.Once);
        }

        [Theory]
        [InlineData("Ukraine", "Poland", 1, 1)]
        public void UpdateGameTest(string team1, string team2, int team1Score, int team2Score)
        {
            //Arrange
            var mockStorage = new Mock<IGamesStorage>();
            mockStorage.Setup(p => p.UpdateScore(team1, team2, team1Score, team2Score))
                .Verifiable();

            //Act
            var board = new ScoreBoard(mockStorage.Object);
            board.UpdateScore(team1,team2, team1Score, team2Score);

            //Assert
            mockStorage.Verify(p => p.UpdateScore(team1, team2, team1Score, team2Score), Times.Once);
        }

        [Theory]
        [InlineData("Ukraine", "Poland", 1, 1)]
        public void StartGameWithCustomScoreTest(string team1, string team2, int team1Score, int team2Score)
        {
            //Arrange
            var mockStorage = new Mock<IGamesStorage>();
            mockStorage.Setup(p => p.AddGame(team1, team2))
                .Verifiable();
            mockStorage.Setup(p => p.UpdateScore(team1, team2, team1Score, team2Score))
                .Verifiable();

            //Act
            var board = new ScoreBoard(mockStorage.Object);
            board.StartGame(team1, team2, team1Score, team2Score);

            //Assert
            mockStorage.Verify(p => p.AddGame(team1, team2), Times.Once);
            mockStorage.Verify(p => p.UpdateScore(team1, team2, team1Score, team2Score), Times.Once);
        }

        [Theory]
        [InlineData("Ukraine", "Poland")]
        public void FinishGameTest(string team1, string team2)
        {
            //Arrange
            var mockStorage = new Mock<IGamesStorage>();
            mockStorage.Setup(p => p.DeleteGame(team1, team2))
                .Verifiable();

            //Act
            var board = new ScoreBoard(mockStorage.Object);
            board.FinishGame(team1, team2);

            //Assert
            mockStorage.Verify(p => p.DeleteGame(team1, team2), Times.Once);
        }

        [Theory]
        [InlineData("Ukraine", "Poland", 1, 1)]
        public void GetGamesSummaryTest(string team1, string team2, int team1Score, int team2Score)
        {
            //Arrange
            var mockStorage = new Mock<IGamesStorage>();
            var summaryMock = new Dictionary<(string homeTeam, string awayTeam), (int homeScore, int awayScore)>
            {
                {(team1, team2),(team1Score, team2Score)}
            };
            mockStorage.Setup(p => p.GetCurrentScores())
                .Returns(summaryMock)
                .Verifiable();

            //Act
            var board = new ScoreBoard(mockStorage.Object);
            var summary = board.GetGamesSummary();

            //Assert
            mockStorage.Verify(p => p.GetCurrentScores(), Times.Once);
            Assert.Equal(summaryMock, summary);
        }

        [Theory]
        [InlineData("Ukraine", "Poland", 1, 1)]
        public void ExportGamesSummaryTest(string team1, string team2, int team1Score, int team2Score)
        {
            //Arrange
            var mockStorage = new Mock<IGamesStorage>();
            var summaryMock = new Dictionary<(string homeTeam, string awayTeam), (int homeScore, int awayScore)>
            {
                {(team1, team2),(team1Score, team2Score)}
            };
            var exportExpected = new List<string>
            {
                $"{team1} {team1Score} - {team2} {team2Score}"
            };
            mockStorage.Setup(p => p.GetCurrentScores())
                .Returns(summaryMock)
                .Verifiable();

            //Act
            var board = new ScoreBoard(mockStorage.Object);
            var exportResult = board.ExportGamesSummary();

            //Assert
            mockStorage.Verify(p => p.GetCurrentScores(), Times.Once);
            Assert.Equal(exportExpected, exportResult);
        }
    }
}
