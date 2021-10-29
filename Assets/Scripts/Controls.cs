using UnityEngine;
using UnityEngine.EventSystems;

public class Controls : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {
    [SerializeField] private Camera camera;
    [SerializeField] private float rayMaxDistance;

    private const int NonPointerID = int.MinValue;

    private bool Pressed => _pointerID != NonPointerID;

    private Hand _lastHighlightedHand;
    private int _pointerID = NonPointerID;

    private void Update() {
        if (!Pressed) {
            if (TryGetPointedCard(out CardBehavior cardBehavior, out Vector3 point)) {
                var hand = cardBehavior.Card.BelongedHand;
                if (hand != null) {
                    _lastHighlightedHand = hand;
                    hand.Highlight(point);
                }
            }
            else {
                if (_lastHighlightedHand != null) {
                    _lastHighlightedHand.RemoveHighLight();
                    _lastHighlightedHand = null;
                }
            }
        }
    }
    
    private bool TryGetPointedCard(out CardBehavior card, out Vector3 point) {
        var mousePos = Input.mousePosition;
        var ray = camera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayMaxDistance)) {
            point = hitInfo.point;
            return CardBehavior.TryGetFromCollider(hitInfo.collider, out card);
        }

        card = null;
        point = Vector3.zero;
        return false;
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (_pointerID == NonPointerID) {
            _pointerID = eventData.pointerId;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (eventData.pointerId == _pointerID) {
            if (TryGetPointedCard(out CardBehavior cardBehavior, out Vector3 point)) {
                var hand = cardBehavior.Card.BelongedHand;
                if (hand != null) {
                    _lastHighlightedHand = hand;
                    hand.DragHighlightedCard(point);
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (eventData.pointerId == _pointerID) {
            _pointerID = NonPointerID;
        }
    }
}
