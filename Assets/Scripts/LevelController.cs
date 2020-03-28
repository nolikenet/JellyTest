using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

  public static LevelController Instance;
  public GridNode[] Destinations;

  private void Awake() {
    Instance = this;
  }

  private void RestartLevel() {
    var activeScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(activeScene.buildIndex);
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.R)) {
      RestartLevel();
    }
  }

  private IEnumerator LoadNextLevelCor() {
    yield return new WaitForSeconds(3.0f);
    var activeScene = SceneManager.GetActiveScene();
    if (activeScene.buildIndex != 2) {
      var next = activeScene.buildIndex + 1;
      SceneManager.LoadScene(next);
    } else {
      SceneManager.LoadScene(0);
    }
  }

  public void TryEndLevel() {
    var solvers = 0;
    foreach (var dest in Destinations) {
      if (dest.Solver != null) {
        solvers++;
      }
    }

    if (Destinations.Length == solvers) {
      StartCoroutine(LoadNextLevelCor());
    }
  }

}