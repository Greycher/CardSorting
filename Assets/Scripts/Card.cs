using System;
using UnityEngine;

[Serializable]
public class Card {
    [SerializeField] private Suit suit;
    [SerializeField] private int points;
    [SerializeField] private Texture texture;

    public Texture Texture => texture;
}
