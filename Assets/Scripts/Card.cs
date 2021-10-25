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

    private CardData _cardData;
    private Pose _targetPose;
    private bool _animating;
    private bool _inPosition;
    private bool _inRotation;

    private void Update() {
        if (_animating) {
            ProgressPosition();
            ProgressRotation();
            if (_inPosition && _inRotation) {
                _animating = false;
                onTargetPoseReached(this);
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
        _animating = true;
        _inPosition = false;
        _inRotation = false;
        _targetPose = pose;
    }
}
