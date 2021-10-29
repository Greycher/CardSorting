using System;
using UnityEngine;

public class CardBehavior : MonoBehaviour {
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private int foregroundMatIndex;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private string flipAnimStr;

    public bool AtTarget => _inPosition && _inRotation;
    public Card Card => _card;

    private Card _card;
    private State _state = State.Free;
    private Pose _targetPose;
    private bool _inPosition;
    private bool _inRotation;

    private void Awake() {
        _card = new Card(this);
    }

    private void Update() {
        if (_state == State.Moving) {
            ProgressPosition();
            ProgressRotation();
            if (AtTarget) {
                _state = State.Free;
            }
        }
    }

    private void ProgressPosition() {
        if (!_inPosition) {
            transform.position = Vector3.MoveTowards(transform.position, _targetPose.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _targetPose.position) <= Vector3.kEpsilon) {
                _inPosition = true;
            }
        }
    }
    
    private void ProgressRotation() {
        if (!_inRotation) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetPose.rotation, rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, _targetPose.rotation) <= Quaternion.kEpsilon) {
                _inRotation = true;
            }
        }
    }
    
    public void AssignData(CardData cardData) {
        _card.AssignData(cardData);
        UpdateTexture(cardData.texture);
    }

    private void UpdateTexture(Texture texture) {
        var mat = renderer.materials[foregroundMatIndex];
        mat.mainTexture = texture;
    }

    public void Flip() {
        animator.SetTrigger(flipAnimStr);
    }

    public void MoveToPose(Pose pose) {
        _inPosition = false;
        _inRotation = false;
        _targetPose = pose;
        
        if (_state == State.Free) {
            _state = State.Moving;
        }
    }

    public void MarkBusy() {
        _state = State.Busy;
    }
    
    public void UnMarkBusy() {
        _state = AtTarget ? State.Free : State.Moving;
    }

    public static bool IsColliderBelongs(Collider collider) {
        var parent = collider.transform.parent;
        if (parent != null) {
            return parent.GetComponent<CardBehavior>() != null;
        }

        return false;
    }

    public static bool TryGetFromCollider(Collider collider, out CardBehavior card) {
        var parent = collider.transform.parent;
        if (parent != null) {
            card = parent.GetComponent<CardBehavior>();
            if (card != null) {
                return true;
            }
        }
        else {
            card = null;
        }
        
        return false;
    }

    private enum State {
        Free,
        Moving,
        Busy
    }
}
