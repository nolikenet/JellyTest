using DG.Tweening;
using UnityEngine;

public class ActorMovement : MonoBehaviour {

  private bool _rotating;

  private void ResetRotation() {
    transform.rotation = Quaternion.Euler(Vector3.zero);
    _rotating = false;
  }

  public void Rotate(Vector3 angleAxis) {
    if (_rotating) return;
    _rotating = true;
    transform.DOLocalRotateQuaternion(transform.localRotation * Quaternion.Euler(angleAxis), 0.5f)
      .OnComplete(
        ResetRotation
      );
  }

}