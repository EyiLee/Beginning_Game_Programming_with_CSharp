using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleCards;

namespace Lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            Card[] card = new Card[5];

            // suffle the deck
            deck.Shuffle();

            // get the first card and print it
            card[0] = deck.TakeTopCard();
            card[0].FlipOver();
            Console.WriteLine("{0} {1}", card[0].Suit, card[0].Rank);
            Console.WriteLine();

            // get the second card and print both
            card[1] = deck.TakeTopCard();
            card[1].FlipOver();
            Console.WriteLine("{0} {1}", card[0].Suit, card[0].Rank);
            Console.WriteLine("{0} {1}", card[1].Suit, card[1].Rank);

            Console.ReadLine();
        }
    }
}
