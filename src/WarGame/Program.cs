using WarGame.Core;
using WarGame.Core.HashSetDeck;

namespace WarGame
{
	class Program
	{
		static void Main(string[] args)
		{
			GameManager gameManager = new GameManager(new Deck(), new ConsoleDisplay(), new WarStatCollector());
			gameManager.StartSimulation();
		}		
	}
}
