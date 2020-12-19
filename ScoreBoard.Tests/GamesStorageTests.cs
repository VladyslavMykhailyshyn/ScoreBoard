using System;
using System.Collections.Generic;
using System.Linq;
using ScoreBoard.Storage.Impl;
using Xunit;

namespace ScoreBoard.Tests
{
    public class GamesStorageTests
    {
        [Theory]
        [InlineData("Ukraine", "Poland", 0, 0)]
        public void AddGameTest(string team1, string team2, int homeScore, int awayScore)
        {
            //Arrange

            //Act
            var storage = new GamesStorage();
            storage.AddGame(team1, team2);
            var resultScores = storage.GetCurrentScores();

            //Assert
            Assert.NotEmpty(resultScores);
            Assert.Single(resultScores);
            Assert.Equal((homeScore, awayScore), resultScores[(team1, team2)]);
        }

        [Theory]
        [InlineData("Ukraine", "Poland")]
        public void AddExistingGameTest(string team1, string team2)
        {
            //Arrange

            //Act
            var storage = new GamesStorage();
            storage.AddGame(team1, team2);

            //Assert
            Assert.Throws<ArgumentException>(() => storage.AddGame(team1, team2));
        }

        [Theory]
        [InlineData("Ukraine", "Poland", 0, 1)]
        public void UpdateNotExistingGameTest(string team1, string team2, int homeScore, int awayScore)
        {
            //Arrange

            //Act
            var storage = new GamesStorage();
            storage.AddGame(team1, team2);

            //Assert
            Assert.Throws<KeyNotFoundException>(() => storage.UpdateScore(team2, team2, homeScore, awayScore));
        }

        [Theory]
        [InlineData("Ukraine", "Poland")]
        public void DeleteNotExistingGameTest(string team1, string team2)
        {
            //Arrange

            //Act
            var storage = new GamesStorage();

            //Assert
            Assert.Throws<KeyNotFoundException>(() => storage.DeleteGame(team1, team2));
        }

        [Theory]
        [InlineData("Ukraine", "Poland", 1, 0)]
        [InlineData("Poland", "Ukraine", 0, 1)]
        public void UpdateScoreTest(string team1, string team2, int homeScore, int awayScore)
        {
            //Arrange

            //Act
            var storage = new GamesStorage();
            storage.AddGame(team1, team2);
            storage.UpdateScore(team1, team2, homeScore, awayScore);
            var resultScores = storage.GetCurrentScores();

            //Assert
            Assert.NotEmpty(resultScores);
            Assert.Single(resultScores);
            Assert.Equal((homeScore, awayScore), resultScores[(team1, team2)]);
        }

        [Fact]
        //Used data from exercise to show that everything works as expected. 
        public void GetCurrentScoresTest()
        {
            //Arrange
            const string team1 = "Mexico";
            const string team2 = "Canada";
            const string team3 = "Spain";
            const string team4 = "Brazil";
            const string team5 = "Germany";
            const string team6 = "France";
            const string team7 = "Uruguay";
            const string team8 = "Italy";
            const string team9 = "Argentina";
            const string team10 = "Australia";
            const int score1 = 0;
            const int score2 = 5;
            const int score3 = 10;
            const int score4 = 2;
            const int score5 = 2;
            const int score6 = 2;
            const int score7 = 6;
            const int score8 = 6;
            const int score9 = 3;
            const int score10 = 1;

            //Act
            var storage = new GamesStorage();
            storage.AddGame(team1, team2);
            storage.AddGame(team3, team4);
            storage.AddGame(team5, team6);
            storage.AddGame(team7, team8);
            storage.AddGame(team9, team10);
            storage.UpdateScore(team1, team2, score1, score2);
            storage.UpdateScore(team3, team4, score3, score4);
            storage.UpdateScore(team5, team6, score5, score6);
            storage.UpdateScore(team7, team8, score7, score8);
            storage.UpdateScore(team9, team10, score9, score10);
            var resultScores = storage.GetCurrentScores();

            //Assert
            Assert.NotEmpty(resultScores);
            Assert.Equal(5, resultScores.Count);
            Assert.Equal((score7, score8), resultScores[resultScores.Keys.ToList()[0]]);
            Assert.Equal((score3, score4), resultScores[resultScores.Keys.ToList()[1]]);
            Assert.Equal((score1, score2), resultScores[resultScores.Keys.ToList()[2]]);
            Assert.Equal((score9, score10), resultScores[resultScores.Keys.ToList()[3]]);
            Assert.Equal((score5, score6), resultScores[resultScores.Keys.ToList()[4]]);
        }

        [Theory]
        [InlineData("Ukraine", "Poland")]
        public void DeleteGameTest(string team1, string team2)
        {
            //Arrange

            //Act
            var storage = new GamesStorage();
            storage.AddGame(team1, team2);
            var scoresBeforeDelete = storage.GetCurrentScores();
            storage.DeleteGame(team1, team2);
            var scoresAfterDelete = storage.GetCurrentScores();

            //Assert
            Assert.NotEmpty(scoresBeforeDelete);
            Assert.Single(scoresBeforeDelete);
            Assert.Empty(scoresAfterDelete);
        }
    }
}