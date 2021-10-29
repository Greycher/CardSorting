using System.Collections.Generic;

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
   
   #region Auto Sort

   public void SortAuto(Card[] cards) {
   }

   #endregion

}
