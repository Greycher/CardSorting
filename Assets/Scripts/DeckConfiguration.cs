using UnityEngine;

[CreateAssetMenu(menuName = "Deck Configuration")]
public class DeckConfiguration : ScriptableObject {
    [SerializeField] private CardData[] cards;

    public CardData[] Cards => cards;
}
