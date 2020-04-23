using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button play;
    public Button tutorial;

    public Button quit;
    string levelToPlay;
    void Start()
    {
        play.onClick.AddListener(() => {
          GameEvents.current.LevelChange();
          levelToPlay = "play";
          Debug.Log("Play game");
          Crossfader.current.TransitionOut(ChangeScene);
        });
        tutorial.onClick.AddListener(() => {
          GameEvents.current.LevelChange();
          levelToPlay = "tutorial";
        });
        quit.onClick.AddListener(() => {
          Application.Quit();
        });
        GameEvents.current.onTransitionOut += ChangeScene;
    }
    void ChangeScene() {
      if (levelToPlay == "play") {
        SceneManager.LoadScene("LevelUpdate");
      } else if (levelToPlay == "tutorial") {
        SceneManager.LoadScene("tutorial");
      }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
