using UnityEngine;

public interface IMovable {

  bool TryMove(Vector3 direction);
  void ResetRotation();
  void ResetScale();

}