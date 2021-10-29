using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CardSort123Test
{
    private CardSort123 _cardSort123 = new CardSort123();

    [Test]
    public void TestCase1() {
        var testCase = GetCase1();
        var result = _cardSort123.Sort(testCase.cards);
        Assert.True(DoesCardGroupsMatches(result, testCase.result));
    }
    
    [Test]
    public void TestCase2() {
        var testCase = GetCase2();
        var result = _cardSort123.Sort(testCase.cards);
        Assert.True(DoesCardGroupsMatches(result, testCase.result));
    }

    private bool DoesCardGroupsMatches(List<CardGroup> result, List<CardGroup> givenResult) {
        if (result.Count != givenResult.Count) {
            return false;
        }

        for (int i = 0; i < result.Count; i++) {
            var group = result[i];
            if (!givenResult.Exists(cardGroup => @group == cardGroup)) {
                return false;
            }
        }

        return true;
    }
    
    private CardSortCases GetCase1() {
        var cards = new[] {
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
        };
        
        cards[0].AssignData(new CardData() {
            suit = Suit.Kupa,
            cardNumber = 1,
        });
        cards[1].AssignData(new CardData() {
            suit = Suit.Maca,
            cardNumber = 2,
        });
        cards[2].AssignData(new CardData() {
            suit = Suit.Karo,
            cardNumber = 5,
        });
        cards[3].AssignData(new CardData() {
            suit = Suit.Kupa,
            cardNumber = 4,
        });
        cards[4].AssignData(new CardData() {
            suit = Suit.Maca,
            cardNumber = 1,
        });
        cards[5].AssignData(new CardData() {
            suit = Suit.Karo,
            cardNumber = 3,
        });
        cards[6].AssignData(new CardData() {
            suit = Suit.Sinek,
            cardNumber = 4,
        });
        cards[7].AssignData(new CardData() {
            suit = Suit.Maca,
            cardNumber = 4,
        });
        cards[8].AssignData(new CardData() {
            suit = Suit.Karo,
            cardNumber = 1,
        });
        cards[9].AssignData(new CardData() {
            suit = Suit.Maca,
            cardNumber = 3,
        });
        cards[10].AssignData(new CardData() {
            suit = Suit.Karo,
            cardNumber = 4,
        });

        var results = new List<CardGroup>();
        
        var cardGroup = new CardGroup();
        cardGroup.cards.Add(cards[4]);
        cardGroup.cards.Add(cards[1]);
        cardGroup.cards.Add(cards[9]);
        cardGroup.cards.Add(cards[7]);
        results.Add(cardGroup);
        
        cardGroup = new CardGroup();
        cardGroup.cards.Add(cards[5]);
        cardGroup.cards.Add(cards[10]);
        cardGroup.cards.Add(cards[2]);
        results.Add(cardGroup);
        
        //Remaining cards
        cardGroup = new CardGroup();
        cardGroup.cards.Add(cards[0]);
        cardGroup.cards.Add(cards[3]);
        cardGroup.cards.Add(cards[6]);
        cardGroup.cards.Add(cards[8]);
        results.Add(cardGroup);

        return new CardSortCases(cards, results);
    }
    
    private CardSortCases GetCase2() {
        var cards = new[] {
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
            new Card(null),
        };
        
        cards[0].AssignData(new CardData() {
            suit = Suit.Kupa,
            cardNumber = 2,
        });
        cards[1].AssignData(new CardData() {
            suit = Suit.Maca,
            cardNumber = 2,
        });
        cards[2].AssignData(new CardData() {
            suit = Suit.Karo,
            cardNumber = 5,
        });
        cards[3].AssignData(new CardData() {
            suit = Suit.Kupa,
            cardNumber = 4,
        });
        cards[4].AssignData(new CardData() {
            suit = Suit.Maca,
            cardNumber = 1,
        });
        cards[5].AssignData(new CardData() {
            suit = Suit.Karo,
            cardNumber = 3,
        });
        cards[6].AssignData(new CardData() {
            suit = Suit.Kupa,
            cardNumber = 3,
        });
        cards[7].AssignData(new CardData() {
            suit = Suit.Maca,
            cardNumber = 4,
        });
        cards[8].AssignData(new CardData() {
            suit = Suit.Karo,
            cardNumber = 1,
        });
        cards[9].AssignData(new CardData() {
            suit = Suit.Maca,
            cardNumber = 3,
        });
        cards[10].AssignData(new CardData() {
            suit = Suit.Sinek,
            cardNumber = 5,
        });

        var results = new List<CardGroup>();
        
        var cardGroup = new CardGroup();
        cardGroup.cards.Add(cards[0]);
        cardGroup.cards.Add(cards[6]);
        cardGroup.cards.Add(cards[3]);
        results.Add(cardGroup);
        
        cardGroup = new CardGroup();
        cardGroup.cards.Add(cards[4]);
        cardGroup.cards.Add(cards[1]);
        cardGroup.cards.Add(cards[9]);
        cardGroup.cards.Add(cards[7]);
        results.Add(cardGroup);
        
        //Remaining cards
        cardGroup = new CardGroup();
        cardGroup.cards.Add(cards[2]);
        cardGroup.cards.Add(cards[5]);
        cardGroup.cards.Add(cards[8]);
        cardGroup.cards.Add(cards[10]);
        results.Add(cardGroup);

        return new CardSortCases(cards, results);
    }
}
