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

  private void Move(GridNode location, Vector3 direction) {
    transform.DOMove(new Vector3(location.transform.position.x, transform.position.y, location.transform.position.z),
      0.3f);
  }

}