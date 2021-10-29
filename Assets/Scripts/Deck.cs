using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour {
    [SerializeField] private DeckConfiguration configuration;
    [SerializeField] private CardBehavior cardBehaviorRes;
    [SerializeField] private float spacing = 0.1f;

    private List<CardBehavior> _cards = new List<CardBehavior>();

    private void Awake() {
        Create();
        Shuffle();
    }

    private void Create() {
        var cards = configuration.Cards;
        for (int i = 0; i < cards.Length; i++) {
            var cardBehavior = Instantiate(cardBehaviorRes, transform);
            cardBehavior.AssignData(cards[i]);
            PositionCardBehaviour(cardBehavior, i);
            _cards.Add(cardBehavior);
        }
    }
    
    private void Shuffle() {
        for (int i = _cards.Count - 1; i > 0; i--) {
            var rand = Random.Range(0, i + 1);
            (_cards[i], _cards[rand]) = (_cards[rand], _cards[i]);
            PositionCardBehaviour(_cards[i], i);
            PositionCardBehaviour(_cards[rand], rand);
        }
    }

    private void PositionCardBehaviour(CardBehavior cardBehavior, int orderIndex) {
        var localPosition = cardBehavior.transform.localPosition;
        localPosition.z = -(orderIndex * spacing);
        cardBehavior.transform.localPosition = localPosition;
    }

    public CardBehavior Draw() {
        var lastIndex = _cards.Count - 1;
        var card = _cards[lastIndex];
        _cards.RemoveAt(lastIndex);
        card.transform.SetParent(null);
        return card;
    }
}
