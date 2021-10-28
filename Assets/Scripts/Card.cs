using System;
using UnityEngine;

public class Card : MonoBehaviour {
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private int foregroundMatIndex;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private string flipAnimStr;

    public Action<Card> onTargetPoseReached;

    public Hand BelongedHand {
        get => _belongedHand;
        set => _belongedHand = value;
    }
    public bool AtTarget => _inPosition && _inRotation;

    private CardData _cardData;
    private State _state = State.Free;
    private Pose _targetPose;
    private Hand _belongedHand;
    private bool _inPosition;
    private bool _inRotation;

    private void Update() {
        if (_state == State.Moving) {
            ProgressPosition();
            ProgressRotation();
            if (AtTarget) {
                _state = State.Free;
                onTargetPoseReached?.Invoke(this);
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
        _cardData = cardData;
        UpdateTexture(_cardData.Texture);
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
            return parent.GetComponent<Card>() != null;
        }

        return false;
    }

    public static bool TryGetFromCollider(Collider collider, out Card card) {
        var parent = collider.transform.parent;
        if (parent != null) {
            card = parent.GetComponent<Card>();
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
