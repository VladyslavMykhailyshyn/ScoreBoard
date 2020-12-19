using System.Collections.Generic;
using System.Linq;
using ScoreBoard.Storage;

namespace ScoreBoard
{
    /// <summary>
    /// Provides all functionality for a scoreboard
    /// </summary>
    public class ScoreBoard
    {
        private readonly IGamesStorage _gamesStorage;

        /// 
        /// <summary>
        /// ScoreBoard controller. Creates ScoreBoard
        /// Storage injection allows creating any other storage (SQL, document, another in-memory store)
        /// that implements the IGamesStorage interface and uses it
        /// </summary>
        /// <param name="storage">Storage to store the scores for this scoreboard</param>
        public ScoreBoard(IGamesStorage storage)
        {
            _gamesStorage = storage;
        }

        /// <summary>
        /// Starts a new game. The score is 0-0 by default, but the game can be added with another score (if needed)
        /// </summary>
        /// <param name="homeTeam">Home team name</param>
        /// <param name="awayTeam">Away team name</param>
        /// <param name="homeScore">Home team init score (zero by default)</param>
        /// <param name="awayScore">Away team init score (zero by default)</param>
        public void StartGame(string homeTeam, string awayTeam, int homeScore = 0, int awayScore = 0)
        {
            _gamesStorage.AddGame(homeTeam,awayTeam);
            if (homeScore != 0 || awayScore != 0)
            {
                _gamesStorage.UpdateScore(homeTeam, awayTeam, homeScore, awayScore);
            }
        }

        /// <summary>
        /// Updates score for teams
        /// </summary>
        /// <param name="homeTeam">Home team name</param>
        /// <param name="awayTeam">Away team name</param>
        /// <param name="homeScore">Home team new score</param>
        /// <param name="awayScore">Away team new score</param>
        public void UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            _gamesStorage.UpdateScore(homeTeam, awayTeam, homeScore, awayScore);
        }

        /// <summary>
        /// Finishes existing game. Removes it from the scoreboard.
        /// </summary>
        /// <param name="homeTeam">Home team name</param>
        /// <param name="awayTeam">Away team name</param>
        public void FinishGame(string homeTeam, string awayTeam)
        {
            _gamesStorage.DeleteGame(homeTeam, awayTeam);
        }

        /// <summary>
        /// Exports games summary as a set of strings in
        /// '{homeTeamName} {homeTeamScore} - {awayTeamName} {awayTeamScore}' format
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> ExportGamesSummary()
        {
            return _gamesStorage.GetCurrentScores().Select(s =>
                $"{s.Key.homeTeam} {s.Value.homeScore} - {s.Key.awayTeam} {s.Value.awayScore}");
        }

        /// <summary>
        /// Returns games summary in a format convenient for further work
        /// </summary>
        /// <returns></returns>
        public Dictionary<(string homeTeam, string awayTeam), (int homeScore, int awayScore)> GetGamesSummary()
        {
            return _gamesStorage.GetCurrentScores();
        }
    }
}
