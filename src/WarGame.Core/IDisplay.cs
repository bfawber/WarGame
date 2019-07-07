using System.Collections.Generic;
using WarGame.Model;

namespace WarGame.Core
{
	/// <summary>
	/// A contract for displaying things to the user
	/// </summary>
	public interface IDisplay
	{
		/// <summary>
		/// Displays that a battle was won.
		/// </summary>
		/// <param name="player">The player who won the battle</param>
		/// <param name="cardWon">The winning card</param>
		/// <param name="cardLost">The losing card</param>
		void DisplayBattleWon(int player, Card cardWon, Card cardLost);

		/// <summary>
		/// Display that a war has been won
		/// </summary>
		/// <param name="player">The player that won the war</param>
		/// <param name="cardsAtStake">The cards that were at stake during the war</param>
		void DisplayWarWon(int player, IEnumerable<Card> cardsAtStake);

		/// <summary>
		/// Display that a game has been won
		/// </summary>
		/// <param name="player">The player that won the game</param>
		void DisplayGameWon(int player);

		/// <summary>
		/// Display the game stats
		/// </summary>
		/// <param name="statCollector">An implementation of a <see cref="IStatCollector"/> for collecting the game stats</param>
		void DisplayGameStats(IStatCollector statCollector);

		/// <summary>
		/// Display a message in the game
		/// </summary>
		/// <param name="message">The message to display</param>
		void DisplayMessage(string message);
	}
}
