using System;
using UnityEngine;

[Serializable]
public class CardData{
    [SerializeField] private Suit suit;
    [SerializeField] private int cardNumber;
    [SerializeField] private int points;
    [SerializeField] private Texture texture;

    public Texture Texture => texture;
    public int Number => cardNumber;

    public static bool operator<(CardData a, CardData b) {

        return (a.suit < b.suit) || (a.suit == b.suit && a.cardNumber < b.cardNumber);
    }
    
    public static bool operator>(CardData a, CardData b)
    {
        return (a.suit > b.suit) || (a.suit == b.suit && a.cardNumber > b.cardNumber);
    }
    
    public static bool operator==(CardData a, CardData b) {

        return a.suit == b.suit && a.cardNumber == b.cardNumber;
    }
    
    public static bool operator!=(CardData a, CardData b)
    {
        return a.suit != b.suit || a.cardNumber != b.cardNumber;
    }
}
