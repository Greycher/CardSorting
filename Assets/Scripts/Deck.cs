using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    [SerializeField] private DeckConfiguration configuration;
    [SerializeField] private Card cardRes;
    [SerializeField] private float spacing = 0.1f;

    private List<Card> _cards = new List<Card>();
    private void Awake() {
        Create();
        Shuffle();
    }

    private void Create() {
        var cards = configuration.Cards;
        for (int i = 0; i < cards.Length; i++) {
            var card = Instantiate(cardRes, transform);
            card.AssignData(cards[i]);
            PositionCard(card, i);
            _cards.Add(card);
        }
    }

    private void PositionCard(Card card, int orderIndex) {
        var localPosition = card.transform.localPosition;
        localPosition.z = -(orderIndex * spacing);
        card.transform.localPosition = localPosition;
    }

    private void Shuffle() {
        for (int i = _cards.Count - 1; i > 0; i--) {
            var rand = Random.Range(0, i + 1);
            (_cards[i], _cards[rand]) = (_cards[rand], _cards[i]);
            PositionCard(_cards[i], i);
            PositionCard(_cards[rand], rand);
        }
    }
}
