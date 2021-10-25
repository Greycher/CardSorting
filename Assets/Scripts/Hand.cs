using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Hand : MonoBehaviour {
    [SerializeField] private Deck deck;
    [SerializeField] private int slotCount;
    [SerializeField] private float arcAngle = 20f;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float heightOffset = 0.01f;
    [SerializeField] private float drawDelaySec = 0.5f;

    private int _filledSlotCount;

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
            var card = deck.Draw();
            var slotNumber = _filledSlotCount;
            AssignCardToSlot(card, slotNumber);
        }
    }

    private void AssignCardToSlot(Card card, int slotNumber) {
        var cardTr = card.transform;
        cardTr.SetParent(transform);
        var angle = GetSlotAngle(slotNumber);
        var pos = transform.TransformPoint(GetSlotLocalPos(slotNumber, angle));
        var rot = transform.rotation * Quaternion.AngleAxis(angle, Vector3.back);
        card.MoveToPose(new Pose(pos, rot));
        card.onTargetPoseReached -= OnCardDrawn;
        card.onTargetPoseReached += OnCardDrawn;
        card.Flip();
    }

    private void OnCardDrawn(Card card) {
        // card.Flip();
        card.onTargetPoseReached -= OnCardDrawn;
    }

    private float GetSlotAngle(int slotNumber) {
        var paddingAngle = arcAngle / slotCount;
        var unitCenter = slotCount / 2f + 0.5f;
        var unitDisplacement = slotNumber - unitCenter;
        return unitDisplacement * paddingAngle;
    }

    private Vector3 GetSlotLocalPos(int slotNumber, float angle) {
        var dir = Quaternion.AngleAxis(angle, Vector3.back) * Vector2.up + Vector3.back * slotNumber * heightOffset;
        return dir * radius;
    }

    private void OnDrawGizmos() {
        for (int i = 0; i < slotCount; i++) {
            var slotNumber = i + 1;
            var pos = transform.TransformPoint(GetSlotLocalPos(slotNumber, GetSlotAngle(slotNumber)));
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
