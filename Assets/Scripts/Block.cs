using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Transform parent;
    private bool held;
    private Vector3 mousePosition;
    private float moveSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (held)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

            //transform.position = parent.position
        }
        else
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    public void SetHold(bool holding)
    {
        held = holding;
    }

    public bool GetHold()
    {
        return held;
    }

}
