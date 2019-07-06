using System;
using System.Collections.Generic;
using System.Linq;
using WarGame.Model;

namespace WarGame.Core.HashSetDeck
{
	public class Deck : IDeck<Card>
	{
		private HashSet<Card> _cards = new HashSet<Card>();
		private Queue<Card> _cardOrder = new Queue<Card>();

		public Deck()
		{
			InitializeBaseDeck();
		}

		public Deck(HashSet<Card> cards)
		{
			_cards = cards;
		}

		public void Add(Card card)
		{
			// This should be ok because hash sets don't allow dups.
			_cards.Add(card);
			_cardOrder.Enqueue(card);
		}

		public int Count()
		{
			return _cards.Count;
		}

		public Card GetNext()
		{
			if(_cardOrder.Count < 1)
			{
				return null;
			}

			//We don't want to immediately re-enqueue because we might lose.
			return _cardOrder.Dequeue();
		}

		public void Remove(Card card)
		{
			_cards.Remove(card);
		}

		public void Shuffle()
		{
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

		public Tuple<IDeck<Card>, IDeck<Card>> Split()
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

			return new Tuple<IDeck<Card>, IDeck<Card>>(new Deck(deckOne), new Deck(deckTwo));
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
