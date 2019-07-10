using WarGame.Core;
using WarGame.Core.HashSetDeck;

namespace WarGame
{
	class Program
	{
		/// <summary>
		/// Plays the card game war.
		/// </summary>
		/// <param name="interactive">Wait for input between each battle</param>
		/// <param name="games">The number of games to play</param>
		/// <param name="compoundStats">Compound the stats for all games, rather than reset each game.</param>
		static void Main(bool interactive = false, int games = 1, bool compoundStats = false)
		{
			//Command line parsing done with the System.CommandLine.DragonFruit package. This is pretty sweet, save for future reference.

			GameManager gameManager = new GameManager(new Deck(), new ConsoleDisplay(), new WarStatCollector(), new Model.WarGameOptions
			{
				Interactive = interactive,
				NumberOfGames = games,
				CompoundStats = compoundStats,
			}
			);
			gameManager.StartSimulation();
		}		
	}
}
