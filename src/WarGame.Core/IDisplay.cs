using System.Collections.Generic;
using WarGame.Model;

namespace WarGame.Core
{
	public interface IDisplay
	{
		void DisplayBattleWon(int player, Card cardWon, Card cardLost);

		void DisplayWarWon(int player, IEnumerable<Card> cardsAtStake);

		void DisplayGameWon(int player);

		void DisplayGameStats(IStatCollector statCollector);

		void DisplayMessage(string message);
	}
}
