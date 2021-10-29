using System.Collections.Generic;

public class CardSortAuto {
    private List<int> _numbers = new List<int>();
    private List<int> _permutations = new List<int>();
    private HashSet<int> _usedCardIdCache = new HashSet<int>();
    private List<CardGroup> _cardGroups = new List<CardGroup>();
    private Stack<CardGroup> _cardGroupStack = new Stack<CardGroup>();

    public List<CardGroup> MinimizeRemainingCardPoints(List<CardGroup> sort123, List<CardGroup> sort777) {
        if (sort123.Count < 2) {
            return sort777;
        }
        
        if (sort777.Count < 2) {
            return sort123;
        }
        
        Reset();
        
        var numberCount = sort123.Count + sort777.Count - 2;
        for (int i = _numbers.Count; i < numberCount; i++) {
            _numbers.Add(i);
        }
        
        HeapPermutation(_numbers, numberCount, numberCount);

        var permutationIndex = 0;
        var maxPoints = 0;
        for (int i = 0; i < _permutations.Count; i += numberCount) {
            _usedCardIdCache.Clear();
            var sum = 0;
            for (int j = 0; j < numberCount; j++) {
                var groupIndex = _permutations[i + j];
                CardGroup cardGroup;
                if (groupIndex >= sort123.Count - 1) {
                    cardGroup = sort777[groupIndex - (sort123.Count -1)];
                }
                else {
                    cardGroup = sort123[groupIndex];
                }
                sum += CalculatePoints(cardGroup);
            }

            if (sum > maxPoints) {
                maxPoints = sum;
                permutationIndex = i / numberCount;
            }
        }
        
        _usedCardIdCache.Clear();
        var startIndex = permutationIndex * numberCount;
        for (int i = startIndex; i < startIndex + numberCount; i++) {
            var groupIndex = _permutations[i];
            CardGroup group;
            if (groupIndex >= sort123.Count - 1) {
                group = sort777[groupIndex - (sort123.Count - 1)];
            }
            else {
                group = sort123[groupIndex];
            }

            var newGroup = GetNewGroup();
            var cards = group.cards;
            var currentCardCount = cards.Count;
            var skip = false;
            for (int j = 0; j < cards.Count; j++) {
                var card = cards[j];
                var data = card.Data;
                if (!_usedCardIdCache.Contains(data.ID)) {
                    newGroup.cards.Add(card);
                }
                else if(--currentCardCount < 3) {
                    PushGroupToStack(newGroup);
                    skip = true;
                    break;
                }
            }

            if (!skip) {
                for (int j = 0; j < newGroup.cards.Count; j++) {
                    _usedCardIdCache.Add(newGroup.cards[j].Data.ID);
                }
                _cardGroups.Add(newGroup);
            }
        }

        var remainingCardGroup = GetNewGroup();
        for (int i = 0; i < sort123.Count; i++) {
            var cards = sort123[i].cards;
            for (int j = 0; j < cards.Count; j++) {
                var card = cards[j];
                if (!_usedCardIdCache.Contains(card.Data.ID)) {
                    remainingCardGroup.cards.Add(card);
                }
            }
        }
        
        _cardGroups.Add(remainingCardGroup);
        return _cardGroups;
    }
    
    private void Reset() {
        for (int i = 0; i < _cardGroups.Count; i++) {
            var cardGroup = _cardGroups[i];
            PushGroupToStack(cardGroup);
        }
        _cardGroups.Clear();
        _permutations.Clear();
    }

    private void PushGroupToStack(CardGroup cardGroup) {
        cardGroup.Reset();
        _cardGroupStack.Push(cardGroup);
    }
    
    private CardGroup GetNewGroup() {
        CardGroup cardGroup;
        if (_cardGroupStack.Count > 0) {
            cardGroup = _cardGroupStack.Pop();
        }
        else {
            cardGroup = new CardGroup();
        }
      
        return cardGroup;
    }
    
    private int CalculatePoints(CardGroup group) {
        var cards = group.cards;
        var currentCardCount = cards.Count;
        var sum = 0;
        for (int i = 0; i < cards.Count; i++) {
            var data = cards[i].Data;
            if (!_usedCardIdCache.Contains(data.ID)) {
                _usedCardIdCache.Add(data.ID);
                sum += data.points;
            }
            else if(--currentCardCount < 3) {
                return 0;
            }
        }

        return sum;
    }
    
    private void HeapPermutation(List<int> a, int size, int unchangedSize) {
        if (size == 1) {
            for (int i = 0; i < unchangedSize; i++) {
                _permutations.Add(a[i]);
            }
        }
        
        for (int i = 0; i < size; i++) {
            HeapPermutation(a, size - 1, unchangedSize);
            
            if (size % 2 == 1) {
                int temp = a[0];
                a[0] = a[size - 1];
                a[size - 1] = temp;
            }
            
            else {
                int temp = a[i];
                a[i] = a[size - 1];
                a[size - 1] = temp;
            }
        }
    }
}
