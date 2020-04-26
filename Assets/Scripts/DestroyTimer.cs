using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    private float destroyTime = 0.5f;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > destroyTime)
        {
            Destroy(transform.gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public void SetDestroyTime(float d)
    {
        destroyTime = d;
    }
}
