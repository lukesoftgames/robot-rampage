using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    private float speed = 7.0f;
    private float angle;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        angle = Random.Range(0, Mathf.PI / 6);
        dir.x = Mathf.Cos(angle);
        dir.y = Mathf.Sin(angle);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
}
