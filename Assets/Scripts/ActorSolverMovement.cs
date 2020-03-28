using DG.Tweening;
using UnityEngine;

public class ActorSolverMovement : MonoBehaviour, IMovable {

  private NodeScope _node;
  private void Awake() {
    _node = GetComponent<NodeScope>();
    transform.position = Vector3.up + _node.StartingNode.transform.position;
    _node.CurrentNode.Solver = this;
  }

  public bool TryMove(Vector3 direction) {
    foreach (var node in _node.CurrentNode.Neighbors) {
      var vecDir = node.transform.position - _node.CurrentNode.transform.position;
      if (vecDir.normalized != direction.normalized) continue;
      if (!node.IsOpen) return false;
      Move(node, direction);
      node.Solver = this;
      _node.CurrentNode.Solver = null;
      _node.CurrentNode = node;
      return true;
    }

    return false;
  }

  public void Punch() {
    transform.DOShakeScale(0.4f, 0.3f, 10, 1.0f);
  }

  private void Move(GridNode location, Vector3 direction) {
    var sq = DOTween.Sequence();
    var move= transform.DOMove(new Vector3(location.transform.position.x, transform.position.y, location.transform.position.z),
      0.3f);
    var shakeScale = transform.DOShakeScale(0.3f, 0.3f, 10, 1.0f);
    sq.Append(move).Append(shakeScale).Play();
  }

}