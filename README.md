# ScoreBoard

This library represents a scoreboard. It can track scores for any amount of games. 
Provides an interface that allows to add/update/finish a game or get scores for all current games.

Library stores all scores in the in-memory collection. If you want to use another storage - implement the IGamesStorage interface and use your implementation instead of the default.

The library has two options for getting the summary data- STANDARD(returns Dictionary that allows you easily process the data) and EXPORT(returns set of strings that allows you easily display the data)

I also added a possibility to get a game score by one team name only. It looks like a frequent use case for fans with a favorite team.

All functionality is completely covered by unit tests.

Enjoy it.