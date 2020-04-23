using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequencer : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement playerMovement;
    void Start()
    {
        GameEvents.current.onMaskRevealed += TriggerIntroText;
        GameEvents.current.onMaskClosed += RevealRobot;
        GameEvents.current.onPlayerOutside += TriggerMovementTutorial;
        GameEvents.current.onTransitionIn += BeginTutorial;
    }
    void BeginTutorial() {
      GameEvents.current.TutorialStart();
    }
    void TriggerMovementTutorial() {
      DialogueManager.current.Play("movementTutorial1").OnDone(GameEvents.current.MovementTutorialDone);
      playerMovement.enabled = true;
    }
    void RevealRobot() {
      Debug.Log("shake");
      CameraShake.current.Shake(0.1f);
    }
    void TriggerIntroText() {
      DialogueManager.current.Play("intro1")
      .OnDone(GameEvents.current.IntroDialogueDone);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
