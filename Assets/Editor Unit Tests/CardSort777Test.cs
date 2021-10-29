using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CardSort777Test
{
    private CardSort777 _cardSort777 = new CardSort777();
    
    [Test]
    public void TestCase1() {
        var testCase = GetCase1();
        var result = _cardSort777.Sort(testCase.cards);
        Assert.True(DoesCardGroupsMatches(result, testCase.result));
    }
    
    [Test]
    public void TestCase2() {
        var testCase = GetCase2();
        var result = _cardSort777.Sort(testCase.cards);
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
        cardGroup.cardNumber = 1;
        _cardSort777.TryAddToGroup(cardGroup, cards[0]);
        _cardSort777.TryAddToGroup(cardGroup, cards[4]);
        _cardSort777.TryAddToGroup(cardGroup, cards[8]);
        results.Add(cardGroup);
        
        cardGroup = new CardGroup();
        cardGroup.cardNumber = 4;
        _cardSort777.TryAddToGroup(cardGroup, cards[7]);
        _cardSort777.TryAddToGroup(cardGroup, cards[10]);
        _cardSort777.TryAddToGroup(cardGroup, cards[3]);
        _cardSort777.TryAddToGroup(cardGroup, cards[6]);
        results.Add(cardGroup);
        
        //Remaining cards
        cardGroup = new CardGroup();
        cardGroup.cardNumber = 0;
        cardGroup.cards.Add(cards[1]);
        cardGroup.cards.Add(cards[2]);
        cardGroup.cards.Add(cards[5]);
        cardGroup.cards.Add(cards[9]);
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
            cardNumber = 13,
        });
        cards[1].AssignData(new CardData() {
            suit = Suit.Maca,
            cardNumber = 8,
        });
        cards[2].AssignData(new CardData() {
            suit = Suit.Karo,
            cardNumber = 4,
        });
        cards[3].AssignData(new CardData() {
            suit = Suit.Sinek,
            cardNumber = 13,
        });
        cards[4].AssignData(new CardData() {
            suit = Suit.Maca,
            cardNumber = 9,
        });
        cards[5].AssignData(new CardData() {
            suit = Suit.Karo,
            cardNumber = 11,
        });
        cards[6].AssignData(new CardData() {
            suit = Suit.Sinek,
            cardNumber = 8,
        });
        cards[7].AssignData(new CardData() {
            suit = Suit.Maca,
            cardNumber = 13,
        });
        cards[8].AssignData(new CardData() {
            suit = Suit.Karo,
            cardNumber = 6,
        });
        cards[9].AssignData(new CardData() {
            suit = Suit.Karo,
            cardNumber = 13,
        });
        cards[10].AssignData(new CardData() {
            suit = Suit.Karo,
            cardNumber = 8,
        });
        
        var results = new List<CardGroup>();
        
        var cardGroup = new CardGroup();
        cardGroup.cardNumber = 13;
        _cardSort777.TryAddToGroup(cardGroup, cards[0]);
        _cardSort777.TryAddToGroup(cardGroup, cards[3]);
        _cardSort777.TryAddToGroup(cardGroup, cards[7]);
        _cardSort777.TryAddToGroup(cardGroup, cards[9]);
        results.Add(cardGroup);
        
        cardGroup = new CardGroup();
        cardGroup.cardNumber = 8;
        _cardSort777.TryAddToGroup(cardGroup, cards[1]);
        _cardSort777.TryAddToGroup(cardGroup, cards[6]);
        _cardSort777.TryAddToGroup(cardGroup, cards[10]);
        results.Add(cardGroup);
        
        //Remaining cards
        cardGroup = new CardGroup();
        cardGroup.cardNumber = 0;
        cardGroup.cards.Add(cards[2]);
        cardGroup.cards.Add(cards[4]);
        cardGroup.cards.Add(cards[5]);
        cardGroup.cards.Add(cards[8]);
        results.Add(cardGroup);

        return new CardSortCases(cards, results);
    }
}
