using System;
using System.Collections.Generic;
using System.Linq;

namespace ScoreBoard.Storage.Impl
{
    /// <summary>
    /// Scores storage for scoreboard
    /// </summary>
    public class GamesStorage: IGamesStorage
    {
        private const int DefaultScore = 0;
        private readonly Dictionary<(string homeTeam, string awayTeam), (int homeScore, int awayScore)> 
            _gamesStore = new Dictionary<(string, string), (int, int)>();

        /// <summary>
        /// Adds game to the scoreboard with a default score (0 - 0)
        /// </summary>
        /// <param name="homeTeam"></param>
        /// <param name="awayTeam"></param>
        public void AddGame(string homeTeam, string awayTeam)
        {
            if (_gamesStore.Keys.Any(k =>
                k.homeTeam == homeTeam ||
                k.awayTeam == homeTeam ||
                k.homeTeam == awayTeam ||
                k.awayTeam == awayTeam))
            {
                throw new ArgumentException("One or both of the provided teams are already playing the game");
            }
            _gamesStore.Add((homeTeam, awayTeam), (DefaultScore, DefaultScore));
        }

        /// <summary>
        /// Updates score for existing game
        /// </summary>
        /// <param name="homeTeam">Home team name</param>
        /// <param name="awayTeam">Away team name</param>
        /// <param name="homeScore">Home team init score (zero by default)</param>
        /// <param name="awayScore">Away team init score (zero by default)</param>
        public void UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            if (!_gamesStore.ContainsKey((homeTeam, awayTeam)))
            {
                throw new KeyNotFoundException($"No games between {homeTeam} and {awayTeam}");
            }
            _gamesStore[(homeTeam, awayTeam)] = (homeScore, awayScore);
        }

        /// <summary>
        /// Removes game for provided teams from the board
        /// </summary>
        /// <param name="homeTeam">Home team name</param>
        /// <param name="awayTeam">Away team name</param>
        public void DeleteGame(string homeTeam, string awayTeam)
        {
            if (!_gamesStore.ContainsKey((homeTeam, awayTeam)))
            {
                throw new KeyNotFoundException($"No games between {homeTeam} and {awayTeam}");
            }
            _gamesStore.Remove((homeTeam, awayTeam));
        }

        /// <summary>
        /// Returns scores for current games by total score ordered by most recently
        /// </summary>
        /// <returns>Scores dictionary with keys = team names and values = scores</returns>
        public Dictionary<(string homeTeam, string awayTeam), (int homeScore, int awayScore)> GetCurrentScores()
        {
            return _gamesStore.Reverse().OrderByDescending(gs => gs.Value.awayScore + gs.Value.homeScore)
                .ToDictionary(s => s.Key, s => s.Value);
        }
    }
}
