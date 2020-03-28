using DG.Tweening;
using UnityEngine;

public class ActorPlayerMovement : MonoBehaviour, IMovable {

  private bool _isMoving;
  private NodeScope _node;
  private DG.Tweening.Sequence _moveSq;

  private void Awake() {
    _node = GetComponent<NodeScope>();
    transform.position = new Vector3(_node.StartingNode.transform.position.x, transform.position.y,
      _node.StartingNode.transform.position.z);
  }

  public void ResetRotation() {
    transform.rotation = Quaternion.Euler(Vector3.zero);
  }

  public void ResetScale() {
    transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
  }

  public bool TryMove(Vector3 direction) {
    if (_isMoving) return false;
    foreach (var node in _node.CurrentNode.Neighbors) {
      var vecDir = node.transform.position - _node.CurrentNode.transform.position;
      if (vecDir.normalized != direction.normalized) continue;
      if (!node.IsOpen) return false;
      if (node.Solver != null) {
        var canMove = node.Solver.TryMove(direction);
        if (!canMove) {
          node.Solver.Punch();
          return false;
        }
      }

      Move(node, direction);
      return true;
    }

    return false;
  }

  private Vector3 GetRotationAxis(Vector3 direction) {
    var rotationAxis = Vector3.zero;
    if (direction == Vector3.forward) rotationAxis = Vector3.right;
    else if (direction == Vector3.back) rotationAxis = Vector3.left;
    else if (direction == Vector3.right) rotationAxis = Vector3.back;
    else if (direction == Vector3.left) rotationAxis = Vector3.forward;
    return rotationAxis;
  }

  private void Move(GridNode destination, Vector3 direction) {
    _isMoving = true;
    _moveSq?.Kill();
    ResetRotation();
    ResetScale();
    _moveSq = DOTween.Sequence();
    var move = transform.DOMove(
        new Vector3(destination.transform.position.x, transform.position.y, destination.transform.position.z), 0.2f)
      .OnComplete(() => {
        ;
        _node.CurrentNode = destination;
        _isMoving = false;
      });
    var rotate = transform
      .DOLocalRotateQuaternion(transform.localRotation * Quaternion.Euler(GetRotationAxis(direction) * 90.0f), 0.25f)
      .OnComplete(
        ResetRotation
      );
    var shakeScale = transform.DOShakeScale(0.6f, 0.3f, 10, 1.0f);
    _moveSq.Append(move).Join(rotate).Join(shakeScale).Play();
  }

}