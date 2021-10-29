using System.Collections.Generic;

public class CardGroup {
    public int cardNumber;
    public List<Card> cards = new List<Card>();
    public bool[] suitExistence = new bool[4];
    
    public void Reset() {
        cardNumber = 0;
        cards.Clear();
        suitExistence[0] = false;
        suitExistence[1] = false;
        suitExistence[2] = false;
        suitExistence[3] = false;
    }
    
    public int CalculateTotalPoints() {
        var sum = 0;
        for (int i = 0; i < cards.Count; i++) {
            sum += cards[i].Data.points;
        }

        return sum;
    }

    public static bool operator==(CardGroup a, CardGroup b) {
        if (a.cardNumber != b.cardNumber) {
            return false;
        }
         
        var cardCount = a.cards.Count;
        if (cardCount != b.cards.Count) {
            return false;
        }

        for (int i = 0; i < cardCount; i++) {
            if (!b.cards.Exists(card => card.Data == a.cards[i].Data)) {
                return false;
            }
        }

        return true;
    }
    
    public static bool operator!=(CardGroup a, CardGroup b) {
        return !(a == b);
    }
}