using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSort777{
   private List<CardGroup> _cardGroups = new List<CardGroup>();
   private Stack<CardGroup> _cardGroupStack = new Stack<CardGroup>();

   public List<CardGroup> Sort(Card[] cards) {
      Reset();
      var remainingCardGroup = GetNewGroup();
      for (int i = 0; i < cards.Length; i++) {
         var card = cards[i];
         var cardNumber = card.Data.cardNumber;
         CardGroup cardGroup;
         if (_cardGroups.Count > 0) {
            var index = BinarySearchCardGroup(card.Data.cardNumber, 0, _cardGroups.Count - 1);
            if (index < 0) {
               cardGroup = GetNewGroup();
               _cardGroups.Insert(~index, cardGroup);
            }
            else {
               cardGroup = _cardGroups[index];
            }
         }
         else {
            cardGroup = GetNewGroup();
            _cardGroups.Add(cardGroup);
         }

         cardGroup.cardNumber = cardNumber;
         if (!TryAddToGroup(cardGroup, card)) {
            remainingCardGroup.cards.Add(card);
         }
      }
      
      for (int i = 0; i < _cardGroups.Count ; i++) {
         var cardGroup = _cardGroups[i];
         if (cardGroup.cards.Count < 3) {
            remainingCardGroup.cards.AddRange(cardGroup.cards);
            PushGroupToStack(cardGroup);
            var lastIndex = _cardGroups.Count - 1;
            if (i != lastIndex) {
               _cardGroups[i] = _cardGroups[lastIndex];
            }
            _cardGroups.RemoveAt(lastIndex);
            i--;
         }
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
   }

   private void PushGroupToStack(CardGroup cardGroup) {
      cardGroup.Reset();
      _cardGroupStack.Push(cardGroup);
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
   
   public bool TryAddToGroup(CardGroup cardGroup, Card card) {
      int suitIndex = (int)card.Data.suit;
      if (cardGroup.suitExistence[suitIndex]) {
         return false;
      }

      cardGroup.suitExistence[suitIndex] = true;
      cardGroup.cards.Add(card);
      return true;
   }
   
   private int BinarySearchCardGroup(int cardNumber, int low, int high) {
      while (low <= high) {
         int index1 = low + (high - low >> 1);
         int num3 = _cardGroups[index1].cardNumber.CompareTo(cardNumber);
         if (num3 == 0)
            return index1;
         if (num3 < 0)
            low = index1 + 1;
         else
            high = index1 - 1;
      }
      return ~low;
   }
}
