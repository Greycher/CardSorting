using System;
using UnityEngine;

[Serializable]
public class CardData{
    public Suit suit;
    public int cardNumber;
    public int points;
    public Texture texture;

    public int ID => (int) suit * 100 + cardNumber;

    public static bool operator<(CardData a, CardData b) {

        return a.ID < b.ID;
    }
    
    public static bool operator>(CardData a, CardData b) {
        return a.ID > b.ID;
    }
    
    public static bool operator==(CardData a, CardData b) {

        return a.ID == b.ID;
    }
    
    public static bool operator!=(CardData a, CardData b) {
        return a.ID != b.ID;
    }
}
