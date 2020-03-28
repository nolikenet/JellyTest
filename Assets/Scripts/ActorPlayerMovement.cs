using DG.Tweening;
using UnityEngine;

public class ActorPlayerMovement : MonoBehaviour, IMovable {

  private bool _isMoving;
  private NodeScope _node;

  private void Awake() {
    _node = GetComponent<NodeScope>();
    transform.position = new Vector3(_node.StartingNode.transform.position.x, transform.position.y,
      _node.StartingNode.transform.position.z);
  }

  private void ResetRotation() {
    transform.rotation = Quaternion.Euler(Vector3.zero);
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
    var sq = DOTween.Sequence();
    var move = transform.DOMove(
      new Vector3(destination.transform.position.x, transform.position.y, destination.transform.position.z), 0.3f);
    var rotate = transform
      .DOLocalRotateQuaternion(transform.localRotation * Quaternion.Euler(GetRotationAxis(direction) * 90.0f), 0.3f)
      .OnComplete(
        ResetRotation
      );
    var shakeScale = transform.DOShakeScale(0.3f, 0.3f, 10, 1.0f);
    sq.Append(move).Join(rotate).Append(shakeScale).OnComplete(() => {
      _node.CurrentNode = destination;
      _isMoving = false;
      
    }).Play();
  }

}