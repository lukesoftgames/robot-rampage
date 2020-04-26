using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float distance;
    public float speed;
    void Start()
    {
     GameEvents.current.onRobotOutside += RevealPlayer;
    }
    
    void RevealPlayer() {
      LeanTween.moveX(gameObject, transform.position.x + 5f, 3f).setOnComplete(GameEvents.current.PlayerOutside);

    }

}
