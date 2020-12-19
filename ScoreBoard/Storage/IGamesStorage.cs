using System.Collections.Generic;

namespace ScoreBoard.Storage
{
    /// <summary>
    /// Represents interface for games storage
    /// </summary>
    public interface IGamesStorage
    {        
        /// <summary>
        /// Adds game to the scoreboard with a default score (0 - 0)
        /// </summary>
        /// <param name="homeTeam"></param>
        /// <param name="awayTeam"></param>
        public void AddGame(string homeTeam, string awayTeam);
        /// <summary>
        /// Updates score for existing game
        /// </summary>
        /// <param name="homeTeam">Home team name</param>
        /// <param name="awayTeam">Away team name</param>
        /// <param name="homeScore">Home team init score (zero by default)</param>
        /// <param name="awayScore">Away team init score (zero by default)</param>
        public void UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore);
        /// <summary>
        /// Removes game for provided teams from the board
        /// </summary>
        /// <param name="homeTeam">Home team name</param>
        /// <param name="awayTeam">Away team name</param>
        public void DeleteGame(string homeTeam, string awayTeam);
        /// <summary>
        /// Returns scores for current games by total score ordered by most recently
        /// </summary>
        /// <returns>Scores dictionary with keys = team names and values = scores</returns>
        public Dictionary<(string homeTeam, string awayTeam), (int homeScore, int awayScore)> GetCurrentScores();
        /// <summary>
        /// Returns score for selected team
        /// </summary>
        /// <param name="team">Team name</param>
        /// <returns>Score for the team as key-value pair </returns>
        public KeyValuePair<(string homeTeam, string awayTeam), (int homeScore, int awayScore)> GetTeamScore(
            string team);
    }
}
