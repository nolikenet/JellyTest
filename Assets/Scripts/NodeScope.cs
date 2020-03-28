using UnityEngine;

public class NodeScope : MonoBehaviour {

  public GridNode StartingNode;
  public GridNode CurrentNode { get; set; }

  private void Awake() {
    CurrentNode = StartingNode;
  }

}