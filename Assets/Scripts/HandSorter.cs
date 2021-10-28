using UnityEngine;
using UnityEngine.UI;

public class HandSorter : MonoBehaviour {
    [SerializeField] private Hand hand;
    [SerializeField] private Button sort123Button;
    [SerializeField] private Button sort777Button;
    [SerializeField] private Button autoSortButton;

    private CardSorter _cardSorter = new CardSorter();
    
    private void Awake() {
        sort123Button.onClick.AddListener(Sort123);
        sort777Button.onClick.AddListener(Sort777);
        autoSortButton.onClick.AddListener(SortAuto);
    }

    private void Sort123() {
        _cardSorter.Sort123(hand.Cards);
        hand.UpdateSlots();
    }
    
    private void Sort777() {
        _cardSorter.Sort777(hand.Cards);
        hand.UpdateSlots();
    }
    
    private void SortAuto() {
        _cardSorter.SortAuto(hand.Cards);
        hand.UpdateSlots();
    }

    private void Update() {
        var interactable = hand.Interactable;
        sort123Button.interactable = interactable;
        sort777Button.interactable = interactable;
        autoSortButton.interactable = interactable;
    }
}
