using UnityEngine;

public class GridNode : MonoBehaviour {

  public bool IsOpen;
  public bool IsDestination;
  public GridNode[] Neighbors;

  public ActorSolverMovement Solver { get; set; }

}