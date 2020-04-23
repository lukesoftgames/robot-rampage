using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAttachable : MonoBehaviour
{
    public Vector2 dir;
    private Vector2 move;
    private float speed = 10.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.transform.name);
        GetComponent<IAttachable>().rb.gravityScale = 1;
        if (collision.gameObject.tag == "RobotPiece")
        {
            Debug.Log("IN HERE");
            gameObject.tag = "RobotPiece";
            GetComponent<IAttachable>().attach(collision.transform);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.transform.parent == null)
            {
                collision.transform.GetComponent<HelicopterController>().damage(34);
            }
            else
            {
                collision.transform.parent.GetComponent<TankController>().damage(34);
            }
            Destroy(transform.gameObject);
        }
        Destroy(this);
    }

    private void Start()
    {
        GetComponent<IAttachable>().rb.gravityScale = 0;
        move = dir.normalized * speed;
    }

    private void Update()
    {
        transform.position += (Vector3)move * Time.deltaTime;
    }
}