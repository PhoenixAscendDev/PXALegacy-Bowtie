using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Extensions;

namespace JB2.Bowtie.GameObjects
{
    public static class CardHelper
    {

        public static PlayingCard[]  GeneratePlayingCards(bool isAceHigh, bool includeJokers)
        {
            List<PlayingCard> list = new List<PlayingCard>();

            var faces = System.Enum.GetValues(typeof(Enum.PlayingCardFaceType)).Cast<Enum.PlayingCardFaceType>();
            var suits = System.Enum.GetValues(typeof(Enum.PlayingCardSuitType)).Cast<Enum.PlayingCardSuitType>();

            foreach (Enum.PlayingCardSuitType suit in suits)
            {
                if (suit != Enum.PlayingCardSuitType.NoSuit)
                {

                    foreach (Enum.PlayingCardFaceType face in faces)
                    {
                        if (face != Enum.PlayingCardFaceType.Joker)
                        {
                            list.Add(new PlayingCard(face, suit, isAceHigh));
                        }
                    }
                }
            }

            if(includeJokers)
            {
                list.Add(new PlayingCard(Enum.PlayingCardFaceType.Joker, Enum.PlayingCardSuitType.NoSuit,isAceHigh));
                list.Add(new PlayingCard(Enum.PlayingCardFaceType.Joker, Enum.PlayingCardSuitType.NoSuit, isAceHigh));
            }

            return list.ToArray();
        }

        public static PlayingCardDeck GeneratePlayingCardDeck(bool isAceHigh, bool includeJokers, int numOfSets )
        {
            List<PlayingCard> list = new List<PlayingCard>();
            for(int i=0; i <= numOfSets; i++)
            {
                list.AddRange(GeneratePlayingCards(isAceHigh, includeJokers));
            }

            list.Shuffle();

            return new PlayingCardDeck(list.ToArray());

        }
    }
}
