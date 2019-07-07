using System.Collections.Generic;
using System.Linq;
using WarGame.Model;

namespace WarGame.Core
{
	/// <summary>
	/// An implementation of the <see cref="IStatCollector"/> contract
	/// </summary>
	public class WarStatCollector : IStatCollector
	{
		private int _numberOfBattles = 0;
		private Dictionary<Card, int> _battlesCardHasWon = new Dictionary<Card, int>();
		private Dictionary<Card, int> _battlesCardHasLost = new Dictionary<Card, int>();
		private int _warCount = 0;
		private Dictionary<int, int> _warsWon = new Dictionary<int, int> { { 1, 0 }, { 2, 0 } };

		/// <summary>
		/// Get the game stats
		/// </summary>
		/// <returns></returns>
		public GameStats GetStats()
		{
			KeyValuePair<Card, int> biggestWinner = _battlesCardHasWon.OrderByDescending(x => x.Value).First();
			KeyValuePair<Card, int> biggestLoser = _battlesCardHasLost.OrderByDescending(x => x.Value).First();

			return new GameStats
			{
				NumberOfBattles = _numberOfBattles,
				BiggestLoser = biggestLoser.Key.ToString(),
				BiggestLoserCount = biggestLoser.Value,
				BiggestWinner = biggestWinner.Key.ToString(),
				BiggestWinnerCount = biggestWinner.Value,
				NumberOfWars = _warCount,
				PlayerOneWarWins = _warsWon[1],
				PlayerTwoWarWins = _warsWon[2],
			};
		}

		/// <summary>
		/// Record stats for a battle
		/// </summary>
		/// <param name="winningPlayer">The player that won the battle</param>
		/// <param name="cardWon">The card that won the battle</param>
		/// <param name="cardLost">The card that lost the battle</param>
		public void RecordBattle(int winningPlayer, Card cardWon, Card cardLost)
		{
			_numberOfBattles++;
			if(_battlesCardHasWon.ContainsKey(cardWon))
			{
				_battlesCardHasWon[cardWon]++;
			}
			else
			{
				_battlesCardHasWon.Add(cardWon, 1);
			}


			if (_battlesCardHasLost.ContainsKey(cardLost))
			{
				_battlesCardHasLost[cardLost]++;
			}
			else
			{
				_battlesCardHasLost.Add(cardLost, 1);
			}
		}

		/// <summary>
		/// Record stats for a war
		/// </summary>
		/// <param name="winningPlayer">The player that won the war</param>
		public void RecordWar(int player)
		{
			_warCount++;
			_warsWon[player]++;
		}

		/// <summary>
		/// Reset the game stats
		/// </summary>
		public void Reset()
		{
			_numberOfBattles = 0;
			_battlesCardHasWon = new Dictionary<Card, int>();
			_battlesCardHasLost = new Dictionary<Card, int>();
			_warCount = 0;
			_warsWon = new Dictionary<int, int> { { 1, 0 }, { 2, 0 } };
		}
	}
}
