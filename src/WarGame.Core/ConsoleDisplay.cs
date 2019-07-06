using System;
using WarGame.Model;

namespace WarGame.Core
{
	public class ConsoleDisplay : IDisplay
	{
		public ConsoleDisplay()
		{
		}

		public void DisplayBattleWon(int player, Card cardWon, Card cardLost)
		{
			Console.WriteLine($"Player {player} won the {cardLost} with the {cardWon}.");
		}

		public void DisplayGameStats(IStatCollector statCollector)
		{
			Console.WriteLine(statCollector.GetStats());
		}

		public void DisplayGameWon(int player)
		{
			Console.WriteLine($"Player {player} Won!");
		}

		public void DisplayMessage(string message)
		{
			Console.WriteLine(message);
		}
	}
}
