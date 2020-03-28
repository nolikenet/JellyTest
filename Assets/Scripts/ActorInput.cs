using UnityEngine;

public class ActorInput : MonoBehaviour {

  private ActorMovement _movement;

  private void Awake() {
    _movement = GetComponent<ActorMovement>();
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.W)) {
      _movement.Rotate(Vector3.right * 90.0f);
    } else if (Input.GetKeyDown(KeyCode.A)) {
      _movement.Rotate(Vector3.forward * 90.0f);
    } else if (Input.GetKeyDown(KeyCode.D)) {
      _movement.Rotate(Vector3.forward * (-90.0f));
    } else if (Input.GetKeyDown(KeyCode.S)) {
      _movement.Rotate(Vector3.right * (-90.0f));
    }
  }

}