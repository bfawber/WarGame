using System;
using System.Collections.Generic;
using WarGame.Model;

namespace WarGame.Core
{
	/// <summary>
	/// An implementation of the <see cref="IDisplay"/> using the System Console
	/// </summary>
	public class ConsoleDisplay : IDisplay
	{
		/// <summary>
		/// Displays that a battle was won.
		/// </summary>
		/// <param name="player">The player who won the battle</param>
		/// <param name="cardWon">The winning card</param>
		/// <param name="cardLost">The losing card</param>
		public void DisplayBattleWon(int player, Card cardWon, Card cardLost)
		{
			Console.WriteLine($"Player {player} won the {cardLost} with the {cardWon}.");
		}

		/// <summary>
		/// Display the game stats
		/// </summary>
		/// <param name="statCollector">An implementation of a <see cref="IStatCollector"/> for collecting the game stats</param>
		public void DisplayGameStats(IStatCollector statCollector)
		{
			Console.WriteLine(statCollector.GetStats());
		}

		/// <summary>
		/// Display that a game has been won
		/// </summary>
		/// <param name="player">The player that won the game</param>
		public void DisplayGameWon(int player)
		{
			Console.WriteLine($"Player {player} Won!");
		}

		/// <summary>
		/// Display a message in the game
		/// </summary>
		/// <param name="message">The message to display</param>
		public void DisplayMessage(string message)
		{
			Console.WriteLine(message);
		}

		/// <summary>
		/// Display that a war has been won
		/// </summary>
		/// <param name="player">The player that won the war</param>
		/// <param name="cardsAtStake">The cards that were at stake during the war</param>
		public void DisplayWarWon(int player, IEnumerable<Card> cardsAtStake)
		{
			Console.WriteLine($"Player {player} won the war and got all these cards at stake {string.Join(",", cardsAtStake)}");
		}
	}
}
