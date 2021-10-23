using UnityEngine;

public class Deck : MonoBehaviour {
    [SerializeField] private DeckConfiguration configuration;
    [SerializeField] private CardBehaviour cardRes;

    private void Awake() {
        Create();
    }

    private void Create() {
        var cards = configuration.Cards;
        for (int i = 0; i < cards.Length; i++) {
            var cardBehaviour = Instantiate(cardRes);
            cardBehaviour.AssignCard(cards[i]);
        }
    }
}
