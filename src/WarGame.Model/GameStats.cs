using System;
using System.Collections.Generic;
using System.Text;

namespace WarGame.Model
{
	public class GameStats
	{
		public int NumberOfBattles { get; set; }

		public string BiggestWinner { get; set; }

		public int BiggestWinnerCount { get; set; }

		public string BiggestLoser { get; set; }

		public int BiggestLoserCount { get; set; }

		public int NumberOfWars { get; set; }

		public int PlayerOneWarWins { get; set; }

		public int PlayerTwoWarWins { get; set; }

		public override string ToString()
		{
			return $"Number of Battles: {NumberOfBattles}\nNumber of Wars; {NumberOfWars}\nPlayer 1 War Wins: {PlayerOneWarWins}\nPlayer 2 War Wins: {PlayerTwoWarWins}\n" +
				$"Biggest Winner: {BiggestWinner} with {BiggestWinnerCount} wins!\n" +
				$"Biggest Loser: {BiggestLoser} with {BiggestLoserCount} losses...\n";
		}
	}
}
