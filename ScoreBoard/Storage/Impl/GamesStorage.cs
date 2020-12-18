using System.Collections.Generic;
using System.Linq;

namespace ScoreBoard.Storage.Impl
{
    public class GamesStorage: IGamesStorage
    {
        private readonly Dictionary<(string homeTeam, string awayTeam), (int homeScore, int awayScore)> 
            _gamesStore = new Dictionary<(string, string), (int, int)>();

        public void AddGame(string homeTeam, string awayTeam)
        {
            _gamesStore.Add((homeTeam, awayTeam), (0, 0));
        }

        public void UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            if (_gamesStore.ContainsKey((homeTeam, awayTeam)))
            {
                _gamesStore[(homeTeam, awayTeam)] = (homeScore, awayScore);
            }
        }

        public void DeleteGame(string homeTeam, string awayTeam)
        {
            if (_gamesStore.ContainsKey((homeTeam, awayTeam)))
            {
                _gamesStore.Remove((homeTeam, awayTeam));
            }
        }

        public Dictionary<(string homeTeam, string awayTeam), (int homeScore, int awayScore)> GetCurrentScores()
        {
            return _gamesStore.Reverse().OrderByDescending(gs => gs.Value.awayScore + gs.Value.homeScore)
                .ToDictionary(s => s.Key, s => s.Value);
        }
    }
}
