using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BackManager : MonoBehaviour
{
    public Button play;

    public Button quit;
    string levelToPlay;
    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(() => {
            GameEvents.current.LevelChange();
            levelToPlay = "play";
            Debug.Log("Play game");
            // Open scene
        });
        quit.onClick.AddListener(() => {
            Application.Quit();
        });
        GameEvents.current.onTransitionOut += ChangeScene;
    }

    void ChangeScene()
    {
        if (levelToPlay == "play")
        {
            SceneManager.LoadScene("LevelUpdate");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
