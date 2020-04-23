using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameEvents current;
    void Awake()
    {
        current = this;
    }
    void Start() {
      GameEvents.current.TransitionIn();
    }

    public event Action onBuildingCollapse;
    public void BuildingCollapse() {
      if (onBuildingCollapse!=null) {
       onBuildingCollapse(); 
      }
    }
    public event Action onTutorialStart;

    public void TutorialStart() {
       if (onTutorialStart!=null) {
       onTutorialStart(); 
      }
    }
    public event Action onMaskRevealed;
    public void MaskRevealed() {
       if (onMaskRevealed!=null) {
       onMaskRevealed(); 
      }
    }
    public event Action onIntroDialogueDone;
    public void IntroDialogueDone() {
       if (onIntroDialogueDone!=null) {
       onIntroDialogueDone(); 
      }
    }
    public event Action onMaskClosed;

    public void MaskClosed() {
      if (onMaskClosed!=null) {
        onMaskClosed();
      }
    }

    public event Action onRobotOutside;

    public void RobotOutside() {
      if (onRobotOutside!=null) {
        onRobotOutside();
      }
    }
    public event Action onPlayerOutside;

    public void PlayerOutside() {
      if (onPlayerOutside!=null) {
        onPlayerOutside();
      }
    }

    public event Action onMovementTutorialDone;

    public void MovementTutorialDone() {
      if (onMovementTutorialDone!=null) {
        onMovementTutorialDone();
      }
    }

    public event Action onLevelChange;

    public void LevelChange() {
      if (onLevelChange!=null) {
        onLevelChange();
      }
    }

    public event Action onTransitionOut;

    public void TransitionOut() {
      if (onTransitionOut!=null) {
        onTransitionOut();
      }
    }
    
     public event Action onTransitionIn;

    public void TransitionIn() {
      if (onTransitionIn!=null) {
        onTransitionIn();
      }
    }
    public event Action onFinishTutorial;
    public void FinishTutorial() {
      if (onFinishTutorial!=null) {
        onFinishTutorial();
      }
    }

}
