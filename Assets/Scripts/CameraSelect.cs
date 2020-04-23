using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject staticCam;
    void Start()
    {
        GameEvents.current.onMovementTutorialDone += TargetPlayer;
    }
    void TargetPlayer() {
      staticCam.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
