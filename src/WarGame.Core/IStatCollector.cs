using System;
using System.Collections.Generic;
using System.Text;
using WarGame.Model;

namespace WarGame.Core
{
	public interface IStatCollector
	{
		void RecordBattleWon(int player, Card cardWon, Card cardLost);

		void RecordWar(int player);

		GameStats GetStats();
	}
}
