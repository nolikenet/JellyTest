using DG.Tweening;
using UnityEngine;

public class ActorSolverMovement : MonoBehaviour, IMovable {

  private NodeScope _node;
  private Renderer _renderer;
  private Material _initialMat;
  private Material _destinationMat;

  private DG.Tweening.Sequence _moveSq;
  private DG.Tweening.Tween _punch; 

  private void Start() {
    _node = GetComponent<NodeScope>();
    transform.position = Vector3.up + _node.StartingNode.transform.position;
    _node.CurrentNode.Solver = this;
    _renderer = GetComponent<Renderer>();
    _initialMat = _renderer.material;
    _destinationMat = Resources.Load<Material>("Materials/Destination");
  }

  public bool TryMove(Vector3 direction) {
    foreach (var node in _node.CurrentNode.Neighbors) {
      var vecDir = node.transform.position - _node.CurrentNode.transform.position;
      if (vecDir.normalized != direction.normalized) continue;
      if (!node.IsOpen) return false;
      if (node.Solver != null) return false;
      Move(node, direction);
      node.Solver = this;
      _node.CurrentNode.Solver = null;
      _node.CurrentNode = node;
      _renderer.material = _node.CurrentNode.IsDestination ? _destinationMat : _initialMat;
      
      if (LevelController.Instance.TryEndLevel()) {
        Debug.Log("<color=green>Level Ended.</color>");
      }
      return true;
    }

    return false;
  }

  public void Punch() {
    ResetRotation();
    ResetScale();
    _punch?.Kill();
    _punch = transform.DOShakeScale(0.4f, 0.3f, 10, 1.0f);
  }
  
  public void ResetRotation() {
    transform.rotation = Quaternion.Euler(Vector3.zero);
  }

  public void ResetScale() {
    transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
  }

  private void Move(GridNode location, Vector3 direction) {
    _moveSq?.Kill();
    ResetRotation();
    ResetScale();
    _moveSq = DOTween.Sequence();
    var move = transform.DOMove(
      new Vector3(location.transform.position.x, transform.position.y, location.transform.position.z),
      0.3f);
    var shakeScale = transform.DOShakeScale(0.5f, 0.3f, 10, 1.0f);
    _moveSq.Join(move).Join(shakeScale).Play();
  }

}