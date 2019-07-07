using WarGame.Model;

namespace WarGame.Core
{
	/// <summary>
	/// A contract for collecting stats
	/// </summary>
	public interface IStatCollector
	{
		/// <summary>
		/// Record stats for a battle
		/// </summary>
		/// <param name="winningPlayer">The player that won the battle</param>
		/// <param name="cardWon">The card that won the battle</param>
		/// <param name="cardLost">The card that lost the battle</param>
		void RecordBattle(int winningPlayer, Card cardWon, Card cardLost);

		/// <summary>
		/// Record stats for a war
		/// </summary>
		/// <param name="winningPlayer">The player that won the war</param>
		void RecordWar(int winningPlayer);

		/// <summary>
		/// Get the game stats
		/// </summary>
		/// <returns></returns>
		GameStats GetStats();

		/// <summary>
		/// Reset the game stats
		/// </summary>
		void Reset();
	}
}
