using UnityEngine;

public class ActorPlayerInput : MonoBehaviour {

  private ActorPlayerMovement _playerMovement;

  private void Awake() {
    _playerMovement = GetComponent<ActorPlayerMovement>();
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.W)) {
      _playerMovement.TryMove(Vector3.forward);
    } else if (Input.GetKeyDown(KeyCode.A)) {
      _playerMovement.TryMove(Vector3.left);
    } else if (Input.GetKeyDown(KeyCode.D)) {
      _playerMovement.TryMove(Vector3.right);
    } else if (Input.GetKeyDown(KeyCode.S)) {
      _playerMovement.TryMove(Vector3.back);
    }
  }

}