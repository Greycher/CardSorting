
using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSorter {
   #region 1-2-3 Sort

   public void Sort123(Card[] cards) {
      QuickSort(cards, 0, cards.Length - 1);
   }
   
   void QuickSort(Card[] cards, int low, int high) { 
      if (low < high) { 
         int pi = Partition(cards, low, high); 
         QuickSort(cards, low, pi - 1); 
         QuickSort(cards, pi + 1, high); 
      } 
   } 
   
   private int Partition(Card[] cards, int low, int high) {
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
   
   private void Swap(Card[] cards, int i, int j) { 
      var t = cards[i];
      cards[i] = cards[j];
      cards[j] = t;
   } 

   #endregion
   
   #region 7-7-7 Sort

   private List<CardGroup> _cardGroups = new List<CardGroup>();
   private CardGroupComparer _cardGroupComparer = new CardGroupComparer();

   public void Sort777(Card[] cards) {
      _cardGroups.Clear();
      for (int i = 0; i < cards.Length; i++) {
         var card = cards[i];
         var cardGroup = new CardGroup(card.Data.Number);
         if (_cardGroups.Count > 0) {
            Debug.Log($"Binary searching for card number {cardGroup.cardNumber}.");
            var index = _cardGroups.BinarySearch(cardGroup, _cardGroupComparer);
            Debug.Log($"Fount index is {index}.");
            if (index < 0) {
               index = ~index;
               Debug.Log($"Index is lower than zero, bitwise complement of the number is {index}.");
               cardGroup.AddIndex(i);
               _cardGroups.Insert(index, cardGroup);
            }
            else {
               cardGroup = _cardGroups[index];
               cardGroup.AddIndex(i);
               _cardGroups[index] = cardGroup;
            }
         }
         else {
            cardGroup.AddIndex(i);
            _cardGroups.Add(cardGroup);
         }

         PrintCardGroups();
      }
   }

   private void PrintCardGroups() {
      Debug.Log("There is ");
      for (int i = 0; i < _cardGroups.Count; i++) {
         var cardGroup = _cardGroups[i];
         Debug.Log($"{cardGroup.Count} time {cardGroup.cardNumber},");
      }
   }

   private int BinarySearch(int cardNumber, int low, int high) {
      if (low >= high) {
         int mid = low + (high - low) / 2;
         var midCardNumber = _cardGroups[mid].cardNumber;
         if (midCardNumber == cardNumber) {
            return mid;
         }

         if (midCardNumber > cardNumber) {
            return BinarySearch(cardNumber, low, mid - 1);
         }
 
         return BinarySearch(cardNumber, mid + 1, high);
      }
      
      return -1;
   }

   private struct CardGroup {
      public int cardNumber;
      private int firstIndex;
      private int secondIndex;
      private int thirdIndex;
      private int fourthIndex;

      public int Count => _count;
      
      private int _count;

      public CardGroup(int cardNumber) {
         this.cardNumber = cardNumber;
         firstIndex = -1;
         secondIndex = -1;
         thirdIndex = -1;
         fourthIndex = -1;
         _count = 0;
      }

      public void AddIndex(int i) {
         switch (++_count) {
            case 1:
               firstIndex = i;
               break;
            
            case 2:
               secondIndex = i;
               break;
            
            case 3:
               thirdIndex = i;
               break;
            
            case 4:
               fourthIndex = i;
               break;
         }
      }
   }
   
   private class CardGroupComparer : IComparer<CardGroup> {
      public int Compare(CardGroup x, CardGroup y) {
         return x.cardNumber.CompareTo(y.cardNumber);
      }
   }
   
   #endregion

   #region Auto Sort

   public void SortAuto(Card[] cards) {
      QuickSort(cards, 0, cards.Length - 1);
   }

   #endregion

}
