using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleCards;

namespace Lab13
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();

            deck.Shuffle();

            List<Card> cards = new List<Card>();

            // take 5 cards from deck
            for (int i = 0; i < 5; i++)
            {
                cards.Add(deck.TakeTopCard());
            }

            // print cards on hand
            foreach (Card card in cards)
            {
                card.Print();
            }

            Console.ReadLine();
        }
    }
}
