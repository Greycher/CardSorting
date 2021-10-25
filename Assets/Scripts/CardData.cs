using System;
using UnityEngine;

[Serializable]
public class CardData {
    [SerializeField] private Suit suit;
    [SerializeField] private int cardNumber;
    [SerializeField] private int points;
    [SerializeField] private Texture texture;

    public Texture Texture => texture;
}
