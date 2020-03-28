using UnityEngine;

public class GridNode : MonoBehaviour {

    public bool IsOpen;
    public bool IsDestination;
    public GridNode[] Neighbors;
    
    private void Awake() {
        
    }

}
