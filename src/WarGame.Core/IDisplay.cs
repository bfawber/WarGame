using System;
using System.Collections.Generic;
using System.Text;
using WarGame.Model;

namespace WarGame.Core
{
	public interface IDisplay
	{
		void DisplayBattleWon(int player, Card cardWon, Card cardLost);

		void DisplayGameWon(int player);

		void DisplayGameStats(IStatCollector statCollector);

		void DisplayMessage(string message);
	}
}
