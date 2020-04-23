using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
      GameEvents.current.onTutorialStart += OnTutorialStart;
      GameEvents.current.onIntroDialogueDone += OnIntroTextDone;

    }
    void Start()
    {
      transform.localScale = new Vector3(0,0,0);
    }
    private void OnTutorialStart() {
      Grow();
    }
    private void OnIntroTextDone() {
      Shrink();
    }
    void Grow() {
      LeanTween.scale(gameObject, new Vector3(1.4f,1.4f,1.4f), 0.5f).setEase(LeanTweenType.easeInOutCubic)
      .setOnComplete(GameEvents.current.MaskRevealed);

    }
    void Shrink() {
      LeanTween.scale(gameObject,new Vector3(0f,0f,0f), 0.5f)
      .setOnComplete(GameEvents.current.MaskClosed);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
