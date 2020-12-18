using ScoreBoard.Storage;

namespace ScoreBoard
{
    public class ScoreBoard
    {
        private readonly IGamesStorage _gamesStorage;

        //It allows creating any other storage (SQL, document, another in-memory store) that implement the IGamesStorage interface and use it
        public ScoreBoard(IGamesStorage storage)
        {
            _gamesStorage = storage;
        }
    }
}
