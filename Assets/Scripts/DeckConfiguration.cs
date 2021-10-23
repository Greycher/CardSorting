using UnityEngine;

[CreateAssetMenu(menuName = "Deck Configuration")]
public class DeckConfiguration : ScriptableObject {
    [SerializeField] private Card[] cards;

    public Card[] Cards => cards;
}
