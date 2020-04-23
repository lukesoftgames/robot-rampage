using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRobot : MonoBehaviour
{
    // Start is called before the first frame update
    public float distance;
    public float speed;
    public GameObject block;
    private Vector3 pos;
    void Start()
    {
      GameEvents.current.onMaskClosed += RevealRobot;
      GameEvents.current.onMovementTutorialDone += RobotMoveToBuilding;
    }

    void RobotMoveToBuilding(){
      // once I get the code for the robot movement, this will start the game from here
      LeanTween.moveX(gameObject, transform.position.x + distance*2, 10f);
    }

    void RevealRobot() {
      LeanTween.moveX(gameObject, transform.position.x + distance, 5f).setOnComplete(RobotDone);

    }
    void RobotDone() {
      CameraShake.current.StopShake();
        pos = new Vector3(-0.35f, 2.0f, 0);
        var blk1 = Instantiate(block, pos, Quaternion.identity);
        blk1.tag = "block";

        pos = new Vector3(-1, 0.89f, 0);
        var blk2 = Instantiate(block, pos, Quaternion.identity);
        blk2.tag = "block";

        pos = new Vector3(0.25f, 0.89f, 0);
        var blk3 = Instantiate(block, pos, Quaternion.identity);
        blk3.tag = "block";
        GameEvents.current.RobotOutside();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
