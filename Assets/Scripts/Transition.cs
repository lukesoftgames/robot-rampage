using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    // Start is called before the first frame update
    public bool startCovered;
    void Start() {
      SetUp();
      GameEvents.current.onFinishTutorial += TransitionOut;
      GameEvents.current.onLevelChange += TransitionOut;
    }
    void SetUp()
    {
      float startX = 0f;
      float startZ = 0f;
      if (startCovered) {
        startX =0f;
        startZ = 180f;
      } else {
        startX = Screen.width*2;
      }
      GetComponent<RectTransform>().anchoredPosition =
          new Vector3(startX, 0f, 0f);
      GetComponent<RectTransform>().rotation =
         Quaternion.Euler(new Vector3(0f, 0f, startZ));
      if (startCovered) {
        TransitionIn();
      }
    }
    void TransitionOut() {
      startCovered = false;
      Debug.Log(Screen.width);
      LeanTween.moveX(GetComponent<RectTransform>(),-100f, 2f).setEase(LeanTweenType.easeInOutCubic)
      .setOnComplete(GameEvents.current.TransitionOut);
    }
    void TransitionIn() {
      Debug.Log(Screen.width);
      LeanTween.moveX(GetComponent<RectTransform>(), -Screen.width*2, 2f).setEase(LeanTweenType.easeInOutCubic)
      .setOnComplete(GameEvents.current.TransitionIn);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
