using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    private Vector3 pos;
    public Transform cannon;
    private GameObject[] buildings;
    private float speed = 3.0f;
    private float curTime = 0.0f;
    private float shootTime = 1.0f;
    public bool won;
    public bool tutorial;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      
        
        if (!won)
        {

            pos = transform.position;

            buildings = GameObject.FindGameObjectsWithTag("building");

            float distance = Vector3.Distance(transform.position, buildings[0].transform.position);

            if (distance > 15.0f)
            {
                transform.position = pos + Vector3.right * speed * Time.deltaTime;
            }
            else
            {
                if (curTime > shootTime)
                {

                    cannon.GetComponent<cannon>().fire();

                    curTime = 0.0f;
                }
                else
                {
                    curTime += Time.deltaTime;
                }
            }
        }
        
    }
}
