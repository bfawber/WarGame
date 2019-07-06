using System;
using System.Collections.Generic;
using WarGame.Model;

namespace WarGame.Core
{
	public class GameManager
	{
		private readonly IDisplay _display;
		private readonly IStatCollector _statCollector;
		private readonly IDeck _baseDeck;

		private readonly WarGameOptions _gameOptions;

		public GameManager(IDeck baseDeck, IDisplay display, IStatCollector statCollector, WarGameOptions gameOptions)
		{
			_display = display;
			_statCollector = statCollector;
			_baseDeck = baseDeck;
			_gameOptions = gameOptions;
		}

		public void StartSimulation()
		{
			_display.DisplayMessage("----------------------- Welcome to the War Card Game Simulator -----------------------\n");

			var splitDecks = _baseDeck.Split();

			int winner = PlayWar(splitDecks.Item1, splitDecks.Item2);

			_display.DisplayMessage("\n");
			_display.DisplayGameWon(winner);
			_display.DisplayMessage("\n");
			_display.DisplayGameStats(_statCollector);

			_display.DisplayMessage("Press any key to exit...");
			Console.ReadKey();
		}

		private int PlayWar(IDeck deckOne, IDeck deckTwo)
		{
			deckOne.Shuffle();
			deckTwo.Shuffle();

			while (true)
			{
				Card playerOneCard = deckOne.GetNext();
				Card playerTwoCard = deckTwo.GetNext();

				if (playerOneCard.Rank == playerTwoCard.Rank)
				{
					var warResult = War(deckOne, deckTwo, playerOneCard, playerTwoCard);
					switch (warResult.Item1)
					{
						case 1:
							foreach (var card in warResult.Item2)
							{
								deckOne.Add(card);
								deckTwo.Remove(card);
							}
							if (deckTwo.Count() < 1) return 1;
							break;
						case 2:
							foreach (var card in warResult.Item2)
							{
								deckTwo.Add(card);
								deckOne.Remove(card);
							}
							if (deckOne.Count() < 1) return 2;
							break;
					}
				}
				else if (playerOneCard.Rank > playerTwoCard.Rank)
				{
					_display.DisplayBattleWon(1, playerOneCard, playerTwoCard);
					_statCollector.RecordBattleWon(1, playerOneCard, playerTwoCard);
					deckOne.Add(playerOneCard);
					deckOne.Add(playerTwoCard);
					deckTwo.Remove(playerTwoCard);
					if (deckTwo.Count() < 1) return 1;
				}
				else
				{
					_display.DisplayBattleWon(2, playerTwoCard, playerOneCard);
					_statCollector.RecordBattleWon(2, playerTwoCard, playerOneCard);
					deckTwo.Add(playerTwoCard);
					deckTwo.Add(playerOneCard);
					deckOne.Remove(playerOneCard);
					if (deckOne.Count() < 1) return 2;
				}

				if (_gameOptions.Interactive)
				{
					_display.DisplayMessage("Press any key for next battle...");
					Console.ReadKey();
				}
			}
		}

		private Tuple<int, List<Card>> War(IDeck deckOne, IDeck deckTwo, Card playerOneCard, Card playerTwoCard)
		{
			_display.DisplayMessage($"War between Player 1's {playerOneCard} and Player 2's {playerTwoCard}!");
			List<Card> cardsUpForStake = new List<Card> { playerOneCard, playerTwoCard };
			Card next = null;

			while (true)
			{
				for (int i = 0; i < 3 && i < deckOne.Count(); i++)
				{
					next = deckOne.GetNext();
					if (next == null) break;
					cardsUpForStake.Add(next);
					playerOneCard = next;
				}

				for (int i = 0; i < 3 && i < deckTwo.Count(); i++)
				{
					next = deckTwo.GetNext();
					if (next == null) break;
					cardsUpForStake.Add(next);
					playerTwoCard = next;
				}

				if (playerOneCard.Rank > playerTwoCard.Rank)
				{
					_display.DisplayBattleWon(1, playerOneCard, playerTwoCard);
					_display.DisplayWarWon(1, cardsUpForStake);
					_statCollector.RecordWar(1);
					_statCollector.RecordBattleWon(1, playerOneCard, playerTwoCard);
					return new Tuple<int, List<Card>>(1, cardsUpForStake);
				}
				else if (playerOneCard.Rank < playerTwoCard.Rank)
				{
					_display.DisplayBattleWon(2, playerTwoCard, playerOneCard);
					_display.DisplayWarWon(2, cardsUpForStake);
					_statCollector.RecordWar(2);
					_statCollector.RecordBattleWon(2, playerTwoCard, playerOneCard);
					return new Tuple<int, List<Card>>(2, cardsUpForStake);
				}
			}
		}
	}
}
