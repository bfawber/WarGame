namespace WarGame.Model
{
	public class Card
	{
		public int Rank { get; set; }

		public SuitKind Suit { get; set; }

		public override string ToString()
		{
			return $"{CardTranslator.Translate(Rank)} of {Suit.ToString()}";
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Card);
		}

		private bool Equals(Card other)
		{
			if (ReferenceEquals(this, other)) return true;
			if (other == null) return false;

			return Rank == other.Rank &&
				Suit == other.Suit;
		}

		public override int GetHashCode()
		{
			var hashCode = 483265535;
			hashCode = hashCode * -1521134295 + Rank.GetHashCode();
			hashCode = hashCode * -1521134295 + Suit.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(Card left, Card right)
		{
			if (ReferenceEquals(left, right)) return true;
			if (ReferenceEquals(right, null)) return false;
			if (ReferenceEquals(left, null)) return false;

			return left.Equals(right);
		}

		public static bool operator !=(Card left, Card right)
		{
			return !(left == right);
		}
	}
}
