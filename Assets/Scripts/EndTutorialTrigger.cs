using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndTutorialTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private bool readyToEnd = false;
    void Start() {
      GameEvents.current.onTransitionOut += SwitchLevel;
    }
    void SwitchLevel() {
      if (readyToEnd) {
        SceneManager.LoadScene("LevelUpdate");
      }
    }
    void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.tag == "Player") {
        readyToEnd = true;
        GameEvents.current.FinishTutorial();
      }
    }
}
