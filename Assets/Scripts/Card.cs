using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {
    public CardBehavior Behavior => _behavior;
    public CardData Data => _cardData;
    public Hand BelongedHand {
        get => _belongedHand;
        set => _belongedHand = value;
    }
    
    private CardBehavior _behavior;
    private CardData _cardData;
    private Hand _belongedHand;

    public Card(CardBehavior behavior) {
        _behavior = behavior;
    }

    public void AssignData(CardData cardData) {
        _cardData = cardData;
    }
}
