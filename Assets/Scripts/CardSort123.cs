using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSort123 {
    private List<Card> _cards = new List<Card>();
    private List<CardGroup> _cardGroups = new List<CardGroup>();
    private Stack<CardGroup> _cardGroupStack = new Stack<CardGroup>();
    
    public List<CardGroup> Sort(Card[] cards) {
        Reset();
        _cards.AddRange(cards);
        var cardCount = cards.Length;
        QuickSort(_cards, 0, cardCount - 1);
        var remainingCardGroup = GetNewGroup();
        for (int i = 0; i < cardCount;) {
            //There needs to be at least 3 consecutive card
            if (i >= cardCount - 2) {
                while (i < cardCount) {
                    remainingCardGroup.cards.Add(_cards[i++]);
                }
                break;
            }
            var j = i;
            Card thisCard;
            Card nextCard = _cards[j];
            do {
                thisCard = nextCard;
                j++;
                if (j < cardCount) {
                    nextCard = _cards[j];
                }
            } while (j < cardCount && thisCard.Data.suit == nextCard.Data.suit && nextCard.Data.cardNumber == thisCard.Data.cardNumber + 1);
            var streak = j - i;
            if (streak >= 3) {
                var cardGroup = GetNewGroup();
                for (int k = i; k < j; k++) {
                    cardGroup.cards.Add(_cards[k]);
                }
                _cardGroups.Add(cardGroup);
            }
            else {
                for (int k = i; k < j; k++) {
                    remainingCardGroup.cards.Add(_cards[k]);
                }
            }
            i = j;
        }
        
        _cardGroups.Add(remainingCardGroup);
        return _cardGroups;
    }
    
    private void Reset() {
        for (int i = 0; i < _cardGroups.Count; i++) {
            var cardGroup = _cardGroups[i];
            PushGroupToStack(cardGroup);
        }
        _cardGroups.Clear();
        _cards.Clear();
    }

    private void PushGroupToStack(CardGroup cardGroup) {
        cardGroup.Reset();
        _cardGroupStack.Push(cardGroup);
    }
   
    void QuickSort(List<Card> cards, int low, int high) { 
        if (low < high) { 
            int pi = Partition(cards, low, high); 
            QuickSort(cards, low, pi - 1); 
            QuickSort(cards, pi + 1, high); 
        } 
    } 
   
    private int Partition(List<Card> cards, int low, int high) {
        var pivot = cards[high].Data;
        int i = (low - 1);
        for (int j = low; j <= high - 1; j++) { 
         
            if (cards[j].Data < pivot) { 
                i++;
                Swap(cards, i, j); 
            } 
        } 
        Swap(cards, i + 1, high); 
        return (i + 1); 
    } 
   
    private void Swap(List<Card> cards, int i, int j) { 
        var t = cards[i];
        cards[i] = cards[j];
        cards[j] = t;
    } 
    
    private CardGroup GetNewGroup() {
        CardGroup cardGroup;
        if (_cardGroupStack.Count > 0) {
            cardGroup = _cardGroupStack.Pop();
        }
        else {
            cardGroup = new CardGroup();
        }
      
        return cardGroup;
    }
}
