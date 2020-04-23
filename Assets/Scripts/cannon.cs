using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{
    private float curTime = 0.0f;
    private float shootTime = 1.0f;
    public GameObject ballPrefab;
    public GameObject buildingPrefab;
    private GameObject[] buildings;
    private Vector3 pos;
    private float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        pos= transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fire()
    {
        pos = transform.position;
        Instantiate(ballPrefab, pos, Quaternion.identity);
    }
}
