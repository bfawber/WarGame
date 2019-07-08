using System;
using System.Collections.Generic;
using System.Linq;
using WarGame.Model;

namespace WarGame.Core.HashSetDeck
{
	/// <summary>
	/// A deck of cards
	/// </summary>
	public class Deck : IDeck
	{
		private HashSet<Card> _cards = new HashSet<Card>();
		private Queue<Card> _cardOrder = new Queue<Card>();

		/// <summary>
		/// Initializes a new instance of a <see cref="Deck"/> class with a standard deck of cards.
		/// </summary>
		public Deck()
		{
			InitializeBaseDeck();
		}

		/// <summary>
		/// Initializes a new instance of a <see cref="Deck"/> class with a deck provided.
		/// </summary>
		/// <param name="cards">The deck of cards the deck should have.</param>
		public Deck(HashSet<Card> cards)
		{
			_cards = cards;
		}

		/// <summary>
		/// Adds a card to the deck
		/// </summary>
		/// <param name="Card">The card to add</param>
		public void Add(Card card)
		{
			// This should be ok because hash sets don't allow dups.
			_cards.Add(card);
			_cardOrder.Enqueue(card);
		}

		/// <summary>
		/// Get's the count of cards in the deck.
		/// </summary>
		/// <returns>The number of cards in the deck</returns>
		public int Count()
		{
			return _cards.Count;
		}

		/// <summary>
		/// Gets the next card in the deck
		/// </summary>
		/// <returns>The next card in the deck</returns>
		public Card GetNext()
		{
			if(_cardOrder.Count < 1)
			{
				return null;
			}

			//We don't want to immediately re-enqueue because we might lose.
			return _cardOrder.Dequeue();
		}

		/// <summary>
		/// Removes a card from the deck
		/// </summary>
		/// <param name="Card">The card to remove</param>
		public void Remove(Card card)
		{
			_cards.Remove(card);
		}

		/// <summary>
		/// Shuffles the cards in the deck into a "random" order.
		/// </summary>
		/// <returns>The shuffled deck</returns>
		public void Shuffle()
		{
			_cardOrder.Clear();

			HashSet<Card> deck = new HashSet<Card>(_cards);
			int count = deck.Count;
			Random rand = new Random();
			for (int i = 0; i < count; i++)
			{
				int index = rand.Next(deck.Count - 1);
				var deckArray = deck.ToArray();
				deck.Remove(deckArray[index]);

				_cardOrder.Enqueue(deckArray[index]);
			}
		}

		/// <summary>
		/// Splits the deck into two equally sized decks
		/// </summary>
		/// <returns>Two decks of equal size</returns>
		public Tuple<IDeck, IDeck> Split()
		{
			HashSet<Card> deckOne = new HashSet<Card>(_cards);
			int count = deckOne.Count;

			HashSet<Card> deckTwo = new HashSet<Card>();
			Random rand = new Random();
			for(int i = 0; i < count / 2; i++)
			{
				int index = rand.Next(deckOne.Count - 1);
				var deckOneArray = deckOne.ToArray();
				deckOne.Remove(deckOneArray[index]);
				deckTwo.Add(deckOneArray[index]);
			}

			return new Tuple<IDeck, IDeck>(new Deck(deckOne), new Deck(deckTwo));
		}

		private void InitializeBaseDeck()
		{
			foreach(var suit in Enum.GetValues(typeof(SuitKind)).Cast<SuitKind>())
			{
				var cards = CreateCardsForSuit(suit);
				foreach(var card in cards)
				{
					_cards.Add(card);
				}
			}
		}

		private IEnumerable<Card> CreateCardsForSuit(SuitKind suit)
		{
			List<Card> cards = new List<Card>();
			for(int i = 2; i < 15; i++)
			{
				cards.Add(new Card
				{
					Rank = i,
					Suit = suit,
				});
			}

			return cards;
		}
	}
}
