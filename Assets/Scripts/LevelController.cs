using UnityEngine;

public class LevelController : MonoBehaviour {

  public static LevelController Instance;
  public GridNode[] Destinations;

  private void Awake() {
    Instance = this;
  }

  public bool TryEndLevel() {
    var solvers = 0;
    foreach (var dest in Destinations) {
      if (dest.Solver != null) {
        solvers++;
      }
    }
    return Destinations.Length == solvers;
  }

}