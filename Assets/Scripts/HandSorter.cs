using UnityEngine;
using UnityEngine.UI;

public class HandSorter : MonoBehaviour {
    [SerializeField] private Hand hand;
    [SerializeField] private Button sort123Button;
    [SerializeField] private Button sort777Button;
    [SerializeField] private Button autoSortButton;

    private CardSort777 _cardSort777 = new CardSort777();
    private CardSort123 _cardSort123 = new CardSort123();
    private CardSortAuto _cardSortAuto = new CardSortAuto();
    
    
    private void Awake() {
        sort123Button.onClick.AddListener(Sort123);
        sort777Button.onClick.AddListener(Sort777);
        autoSortButton.onClick.AddListener(SortAuto);
    }

    private void Sort123() {
        var cardGroups = _cardSort123.Sort(hand.Cards);
        if (cardGroups.Count > 1) {
            hand.UpdateSlots(cardGroups);
        }
    }
    
    private void Sort777() {
        var cardGroups = _cardSort777.Sort(hand.Cards);
        //There is at least remaining cards group
        //If the count is below 1 then there is no actual group
        if (cardGroups.Count > 1) {
            hand.UpdateSlots(cardGroups);
        }
    }
    
    private void SortAuto() {
        var cardGroups = _cardSortAuto.MinimizeRemainingCardPoints(_cardSort123.Sort(hand.Cards), _cardSort777.Sort(hand.Cards));
        if (cardGroups.Count > 1) {
            hand.UpdateSlots(cardGroups);
        }
    }

    private void Update() {
        var interactable = hand.Interactable;
        sort123Button.interactable = interactable;
        sort777Button.interactable = interactable;
        autoSortButton.interactable = interactable;
    }
}
