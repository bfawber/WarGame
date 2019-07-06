using System;
using WarGame.Model;

namespace WarGame.Core
{
	/// <summary>
	/// Actions all decks should be able to do.
	/// </summary>
	/// <typeparam name="T">The card type</typeparam>
	public interface IDeck
	{
		/// <summary>
		/// Shuffles the cards in the deck into a "random" order.
		/// </summary>
		/// <returns>The shuffled deck</returns>
		void Shuffle();

		/// <summary>
		/// Splits the deck into two equally sized decks
		/// </summary>
		/// <returns>Two decks of equal size</returns>
		Tuple<IDeck, IDeck> Split();

		/// <summary>
		/// Gets the next card in the deck
		/// </summary>
		/// <returns>The next card in the deck</returns>
		Card GetNext();

		/// <summary>
		/// Adds a card to the deck
		/// </summary>
		/// <param name="Card">The card to add</param>
		void Add(Card card);

		/// <summary>
		/// Removes a card from the deck
		/// </summary>
		/// <param name="Card">The card to remove</param>
		void Remove(Card card);

		int Count();
	}
}
