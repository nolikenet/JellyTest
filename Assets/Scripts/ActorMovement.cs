using DG.Tweening;
using UnityEngine;

public class ActorMovement : MonoBehaviour {

  private bool _isMoving;
  private bool _rotating;

  public GridPiece _currentGridPiece;

  private void ResetRotation() {
    transform.rotation = Quaternion.Euler(Vector3.zero);
    _rotating = false;
  }

  public void TryMove(Vector3 direction) {
    if (_isMoving) return;
    foreach (var neighbor in _currentGridPiece.Neighbors) {
      var vecDir = neighbor.transform.position - _currentGridPiece.transform.position;
      if (vecDir.normalized == direction.normalized) {
        if (!neighbor.IsOpen) return;
            Move(neighbor, direction);
      }
    }
  }

  private Vector3 GetRotationAxis(Vector3 direction) {
    var rotationAxis = Vector3.zero;
    if (direction == Vector3.forward) rotationAxis = Vector3.right;
    else if (direction == Vector3.back) rotationAxis = Vector3.left;
    else if (direction == Vector3.right) rotationAxis = Vector3.back;
    else if (direction == Vector3.left) rotationAxis = Vector3.forward;
    return rotationAxis;
  }

  private void Move(GridPiece destination, Vector3 direction) {
    _isMoving = true;
    var sq = DOTween.Sequence();
    var move = transform.DOMove(new Vector3(destination.transform.position.x, transform.position.y, destination.transform.position.z), 0.3f);
    var rotate = transform.DOLocalRotateQuaternion(transform.localRotation * Quaternion.Euler(GetRotationAxis(direction) * 90.0f), 0.3f)
      .OnComplete(
        ResetRotation
      );
    var punchScale = transform.DOPunchScale(direction.normalized, 0.3f, 10, 1.0f);
    sq.Append(move).Join(rotate).Append(punchScale).OnComplete(() => {
      _currentGridPiece = destination;
      _isMoving = false;
    }).Play();
  }

}