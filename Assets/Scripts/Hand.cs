using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Hand : MonoBehaviour {
    [SerializeField] private Deck deck;
    [SerializeField] private int slotCount;
    [SerializeField] private float arcAngle = 20f;
    [SerializeField] private float radius = 1f;
    [FormerlySerializedAs("heightOffset")] [SerializeField] private float zOffset = 0.01f;
    [SerializeField] private float drawDelaySec = 0.5f;
    [SerializeField] private float highlightDistance = 0.2f;

    public bool Highlighting => _highlightedCard != null;

    private bool Interactable => _cards[slotCount - 1] != null;

    private Card[] _cards;
    private Card _highlightedCard;
    private int _filledSlotCount;

    private void Awake() {
        _cards = new Card[slotCount];
    }

    private void Start() {
        StartCoroutine(Fill());
    }

    IEnumerator Fill() {
        for (int i = 0; i < slotCount; i++) {
            DrawACard();
            yield return new WaitForSeconds(drawDelaySec);
        }
    }

    private void DrawACard() {
        if (_filledSlotCount < slotCount) {
            _filledSlotCount++;
            var slotIndex = _filledSlotCount - 1;
            var card = deck.Draw();
            AssignCardToSlot(card, slotIndex);
            card.Flip();
        }
    }

    private void AssignCardToSlot(Card card, int slotIndex) {
        var cardTr = card.transform;
        cardTr.SetParent(transform);
        var angle = SlotToAngle(slotIndex);
        var zOffset = Vector3.back * slotIndex * this.zOffset;
        var pos = transform.TransformPoint(AngleToLocalPos(angle) + zOffset);
        var rot = transform.rotation * AngleToLocalRot(angle);
        card.MoveToPose(new Pose(pos, rot));
        _cards[slotIndex] = card;
        card.IsAtHand = true;
    }
    
    private float SlotToAngle(int slotIndex) {
        var paddingAngle = arcAngle / slotCount;
        var unitCenter = slotCount / 2f + 0.5f;
        var slotNumber = slotIndex + 1;
        var unitDisplacement = slotNumber - unitCenter;
        return unitDisplacement * paddingAngle;
    }
    
    private int AngleToClosestSlotIndex(float angle) {
        var paddingAngle = arcAngle / slotCount;
        var unitCenter = slotCount / 2f + 0.5f;
        var unitDisplacement = angle / paddingAngle;
        var slotNumber = Mathf.RoundToInt(unitDisplacement + unitCenter);
        var slotIndex = slotNumber - 1;
        return Mathf.Clamp(slotIndex, 0, slotCount - 1);
    }

    private Vector3 AngleToLocalPos(float angle) {
        var dir = Quaternion.AngleAxis(angle, Vector3.back) * Vector3.up;
        return dir * radius;
    }
    
    private Quaternion AngleToLocalRot(float angle) {
        return Quaternion.AngleAxis(angle, Vector3.back);
    }

    public void Highlight(Vector3 pointedPoint) {
        if (!Interactable) {
            return;
        }
        
        var angle = PointedPositionToAngle(pointedPoint);
        var slotIndex = AngleToClosestSlotIndex(angle);
        var card = _cards[slotIndex];
        if (_highlightedCard != null) {
            if (card == _highlightedCard) {
                return;
            }
            
            RemoveHighLight();
        }

        _highlightedCard = card;
        _highlightedCard.MarkBusy();
        _highlightedCard.transform.localPosition += highlightDistance * Vector3.back;
    }
    
    public void DragHighlightedCard(Vector3 pointedPoint) {
        if (!Highlighting) {
            return;
        }

        var halfArcAngle = arcAngle * 0.5f;
        var angle = Mathf.Clamp(PointedPositionToAngle(pointedPoint), -halfArcAngle, halfArcAngle);
        var tr = _highlightedCard.transform;
        var slotIndex = AngleToClosestSlotIndex(angle);
        var zOffset = Vector3.back * slotIndex * this.zOffset;
        var highlightOffset = highlightDistance * Vector3.back;
        tr.localPosition = AngleToLocalPos(angle) + highlightOffset + zOffset;
        tr.localRotation = AngleToLocalRot(angle);
        if (_cards[slotIndex] != _highlightedCard) {
            MoveCard(FindSlotIndex(_highlightedCard), slotIndex);
        }
    }

    private void MoveCard(int from, int to) {
        Debug.Log($"Card moved from {from} to {to}.");
        if (from > to) {
            Card prevCard = _cards[from];
            for (int i = to; i <= from; i++) {
                var temp = _cards[i];
                AssignCardToSlot(prevCard, i);
                prevCard = temp;
            }
        }
        else {
            Card nextCard = _cards[from];
            for (int i = to; i >= from; i--) {
                var temp = _cards[i];
                AssignCardToSlot(nextCard, i);
                nextCard = temp;
            }
        }
    }

    public void RemoveHighLight() {
        if (_highlightedCard != null) {
            _highlightedCard.UnMarkBusy();
            _highlightedCard.transform.localPosition -= highlightDistance * Vector3.back;
            _highlightedCard = null;
        }
    }
    
    private float PointedPositionToAngle(Vector3 pointedPoint) {
        var up = ((Vector2) transform.InverseTransformPoint(pointedPoint)).normalized;
        var angle = Vector2.SignedAngle(up, Vector2.up);
        return angle;
    }

    private int FindSlotIndex(Card card) {
        for (int i = 0; i < _cards.Length; i++) {
            if (card == _cards[i]) {
                return i;
            }
        }

        return -1;
    }

    private void OnDrawGizmos() {
        for (int i = 0; i < slotCount; i++) {
            var slotIndex = i;
            var pos = transform.TransformPoint(AngleToLocalPos(SlotToAngle(slotIndex)));
            Gizmos.DrawWireSphere(pos, 0.2f);
            
            var style = GUI.skin.label;
            var oldFontSize = style.fontSize;
            style.fontSize = 18;
            var oldTextColor = style.normal.textColor;
            style.normal.textColor = Color.red;
            Handles.Label(pos, i.ToString());
            style.fontSize = oldFontSize;
            style.normal.textColor = oldTextColor;
        }
    }
}
