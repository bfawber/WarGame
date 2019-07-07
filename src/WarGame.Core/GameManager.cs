using System;
using System.Collections.Generic;
using WarGame.Model;

namespace WarGame.Core
{
	/// <summary>
	/// A manager to run the card game
	/// </summary>
	public class GameManager
	{
		private readonly IDisplay _display;
		private readonly IStatCollector _statCollector;
		private readonly IDeck _baseDeck;

		private readonly WarGameOptions _gameOptions;

		/// <summary>
		/// Initializes a new instance of a <see cref="GameManager"/> for playing the card game war.
		/// </summary>
		/// <param name="baseDeck">The base deck to play with</param>
		/// <param name="display">An implementation of the <see cref="IDisplay"/> contract</param>
		/// <param name="statCollector">An implementation of the <see cref="IStatCollector"/> contract</param>
		/// <param name="gameOptions">The options for the game</param>
		public GameManager(IDeck baseDeck, IDisplay display, IStatCollector statCollector, WarGameOptions gameOptions)
		{
			_display = display;
			_statCollector = statCollector;
			_baseDeck = baseDeck;
			_gameOptions = gameOptions;
		}

		/// <summary>
		/// Starts the card game
		/// </summary>
		public void StartSimulation()
		{
			_display.DisplayMessage("----------------------- Welcome to the War Card Game Simulator -----------------------\n");
			for( int i = 0; i < _gameOptions.NumberOfGames; i++)
			{
				var splitDecks = _baseDeck.Split();

				int winner = PlayWar(splitDecks.Item1, splitDecks.Item2);

				_display.DisplayMessage("\n");
				_display.DisplayGameWon(winner);
				_display.DisplayMessage("\n");
				_display.DisplayGameStats(_statCollector);

				if (!_gameOptions.CompoundStats)
				{
					_statCollector.Reset();
				}
			}

			_display.DisplayMessage("Press any key to exit...");
			Console.ReadKey();
		}

		/// <summary>
		/// Plays the card game war
		/// </summary>
		/// <param name="deckOne">Player one's deck</param>
		/// <param name="deckTwo">Player two's deck</param>
		/// <returns>The winner of the game</returns>
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
					War(deckOne, deckTwo, playerOneCard, playerTwoCard);
				}
				else if (playerOneCard.Rank > playerTwoCard.Rank)
				{
					DoBattleOverActions(1, playerOneCard, playerTwoCard, deckOne, deckTwo);
				}
				else
				{
					DoBattleOverActions(2, playerTwoCard, playerOneCard, deckTwo, deckOne);
				}

				if (deckTwo.Count() < 1) return 1;
				if (deckOne.Count() < 1) return 2;

				if (_gameOptions.Interactive)
				{
					_display.DisplayMessage("Press any key for next battle...");
					Console.ReadKey();
				}
			}
		}

		/// <summary>
		/// When two player's cards tie, we have to do the 'War' action. In this action, you put (up to)
		/// three cards at stake and then flip another card for battle. Whomever's card is higher gets all the
		/// cards. 
		/// 
		/// This can repeat multiple times in a row.
		/// </summary>
		/// <param name="deckOne">Player one's deck</param>
		/// <param name="deckTwo">Player two's deck</param>
		/// <param name="playerOneCard">The card that started the war for player one</param>
		/// <param name="playerTwoCard">The card that started the war for player two</param>
		private void War(IDeck deckOne, IDeck deckTwo, Card playerOneCard, Card playerTwoCard)
		{
			_display.DisplayMessage($"War between Player 1's {playerOneCard} and Player 2's {playerTwoCard}!");
			List<Card> cardsUpForStake = new List<Card> { playerOneCard, playerTwoCard };

			while (true)
			{
				// If there's only one card left in the deck, we need to make sure to use the card played to start the war
				playerOneCard = GetCardForWarAndPutCardsUpForStake(deckOne, cardsUpForStake) ?? playerOneCard;
				playerTwoCard = GetCardForWarAndPutCardsUpForStake(deckTwo, cardsUpForStake) ?? playerTwoCard;

				if (playerOneCard.Rank > playerTwoCard.Rank)
				{
					DoWarOverActions(1, playerOneCard, playerTwoCard, deckOne, deckTwo, cardsUpForStake);
					return;
				}
				else if (playerOneCard.Rank < playerTwoCard.Rank)
				{
					DoWarOverActions(2, playerTwoCard, playerOneCard, deckTwo, deckOne, cardsUpForStake);
					return;
				}
			}
		}

		/// <summary>
		/// Get the next card that will be battling for the War action AND put the cards up for stake.
		/// </summary>
		/// <param name="deck">The deck to perform the action for</param>
		/// <param name="cardsUpForStake">The list tracking the cards up for stake. 
		/// THIS LIST GETS MODIFIED. If you make this public, consider changing it to return the list as well.
		/// </param>
		/// <returns>The card that will do battle for the deck.</returns>
		private Card GetCardForWarAndPutCardsUpForStake(IDeck deck, List<Card> cardsUpForStake)
		{
			Card next = null;
			Card cardToBattle = null;

			for (int i = 0; i < 3 && i < deck.Count(); i++)
			{
				// We have to make sure that we don't run out of cards when putting cards up for stake. If we do, just use the last available card.
				next = deck.GetNext();
				if (next == null) break;
				cardsUpForStake.Add(next);
				cardToBattle = next;
			}

			return cardToBattle;
		}

		/// <summary>
		/// Perform the actions necessary when a battle is over
		/// </summary>
		/// <param name="winningPlayer">The player that won the battle</param>
		/// <param name="winningCard">The card that won the battle</param>
		/// <param name="losingCard">The card that lost the battle</param>
		/// <param name="winningDeck">The deck that won the battle</param>
		/// <param name="losingDeck">The deck that lost the battle</param>
		private void DoBattleOverActions(int winningPlayer, Card winningCard, Card losingCard, IDeck winningDeck, IDeck losingDeck)
		{
			_display.DisplayBattleWon(1, winningCard, losingCard);
			_statCollector.RecordBattle(1, winningCard, losingCard);
			winningDeck.Add(winningCard);
			winningDeck.Add(losingCard);
			losingDeck.Remove(losingCard);
		}

		/// <summary>
		/// Perform the actions necessary when a war is over
		/// </summary>
		/// <param name="winningPlayer">The player that won the battle</param>
		/// <param name="winningCard">The card that won the battle</param>
		/// <param name="losingCard">The card that lost the battle</param>
		/// <param name="winningDeck">The deck that won the battle</param>
		/// <param name="losingDeck">The deck that lost the battle</param>
		/// <param name="cardsAtStake">The cards that were at stake</param>
		private void DoWarOverActions(int winningPlayer, Card winningCard, Card losingCard, IDeck winningDeck, IDeck losingDeck, IEnumerable<Card> cardsAtStake)
		{
			_display.DisplayBattleWon(1, winningCard, losingCard);
			_display.DisplayWarWon(1, cardsAtStake);
			_statCollector.RecordWar(1);
			_statCollector.RecordBattle(1, winningCard, losingCard);
			foreach (var card in cardsAtStake)
			{
				winningDeck.Add(card);
				losingDeck.Remove(card);
			}
		}
	}
}
