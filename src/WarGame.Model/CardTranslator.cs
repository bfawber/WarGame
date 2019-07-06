using System;

namespace WarGame.Model
{
	public static class CardTranslator
	{
		public static string Translate(int rank)
		{
			if (rank < 11) return rank.ToString();

			switch (rank)
			{
				case 11:
					return "J";
				case 12:
					return "Q";
				case 13:
					return "K";
				case 14:
					return "A";
				default:
					throw new Exception("Rank not recognized!");
			}
		}
	}
}
