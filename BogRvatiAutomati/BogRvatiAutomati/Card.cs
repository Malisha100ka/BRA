using BogRvatiAutomati.Properties;

namespace PokerOddsCalculator
{
	public enum Rank
	{
		Ace = 1,
		Two,
		Three,
		Four,
		Five,
		Six,
		Seven,
		Eight,
		Nine,
		Ten,
		Jack,
		Queen,
		King
	}

	public enum Suit
	{
		Spades,
		Clubs,
		Hearts,
		Diamonds
	}

	struct Card
	{
		public Rank Rank { get; private set; }
		public Suit Suit { get; private set; }
		public bool IsKnown { get; set; }

        public int CardID { get; set; }

        public Card(Rank r, Suit s) : this()
        {
            Rank = r;
            Suit = s;
            IsKnown = true;
        }
        public Card(int cardNumber)
        {
            Rank = (Rank)((cardNumber / 4) + 1);
            Suit = (Suit)(cardNumber % 4);
            IsKnown = true;
            CardID = cardNumber;
        }

        public override string ToString()
		{
            return Rank.ToString() + Suit.ToString();
        }
	}
}
