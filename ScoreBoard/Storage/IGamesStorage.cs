using System.Collections.Generic;

namespace ScoreBoard.Storage
{
    public interface IGamesStorage
    {
        public void AddGame(string homeTeam, string awayTeam);
        public void UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore);
        public void DeleteGame(string homeTeam, string awayTeam);
        public Dictionary<(string homeTeam, string awayTeam), (int homeScore, int awayScore)> GetCurrentScores();
    }
}
