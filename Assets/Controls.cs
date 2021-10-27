using UnityEngine;
using UnityEngine.EventSystems;

public class Controls : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {
    [SerializeField] private Camera camera;
    [SerializeField] private Hand hand;
    [SerializeField] private float rayMaxDistance;

    private const int NonPointerID = int.MinValue;

    private bool Pressed => _pointerID != NonPointerID;

    private int _pointerID = NonPointerID;
    
    private void Update() {
        if (!Pressed) {
            if (TryGetPointedCard(out Card card, out Vector3 point)) {
                if (card.IsAtHand) {
                    hand.Highlight(point);
                }
            }
            else {
                if (hand.Highlighting) {
                    hand.RemoveHighLight();
                }
            }
        }
    }
    
    private bool TryGetPointedCard(out Card card, out Vector3 point) {
        var mousePos = Input.mousePosition;
        var ray = camera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayMaxDistance)) {
            point = hitInfo.point;
            return Card.TryGetFromCollider(hitInfo.collider, out card);
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
            if (TryGetPointedCard(out Card card, out Vector3 point)) {
                if (card.IsAtHand) {
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