using System;
using WarGame.Core;
using WarGame.Core.HashSetDeck;
using WarGame.Model;
using Xunit;

namespace WarGame.Tests
{
	public class UnitTest1
	{
		[Fact]
		public void InitializeBaseDeck_PopulatesAllCardsInAStandardDeck_WhenInitialized()
		{
			IDeck deck = new Deck();

			Assert.Equal(52, deck.Count());
		}

		[Fact]
		public void Split_SplitsCardsEvenly_BetweenBothDecks()
		{
			IDeck deck = new Deck();

			var splitDecks = deck.Split();

			Assert.Equal(splitDecks.Item1.Count(), splitDecks.Item2.Count());
		}
	}
}
