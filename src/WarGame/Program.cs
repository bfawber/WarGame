using WarGame.Core;
using WarGame.Core.HashSetDeck;

namespace WarGame
{
	class Program
	{
		/// <summary>
		/// Plays the card game war.
		/// </summary>
		/// <param name="interactive">Wait for input between each key press?</param>
		static void Main(bool interactive = false)
		{
			//Command line parsing done with the System.CommandLine.DragonFruit package. This is pretty sweet, save for future reference.

			GameManager gameManager = new GameManager(new Deck(), new ConsoleDisplay(), new WarStatCollector(), new Model.WarGameOptions
			{
				Interactive = interactive,
			}
			);
			gameManager.StartSimulation();
		}		
	}
}
