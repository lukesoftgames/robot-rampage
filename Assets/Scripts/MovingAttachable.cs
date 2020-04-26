using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAttachable : MonoBehaviour
{
    public Vector2 dir;
    private Vector2 move;
    private float init_speed = 20.0f;
    private float speed = 20.0f;
    private bool shot;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.transform.name);
        GetComponent<IAttachable>().rb.gravityScale = 1;
        if (collision.gameObject.tag == "RobotPiece")
        {
            gameObject.tag = "RobotPiece";
            GetComponent<IAttachable>().attach(collision.transform);
            GetComponent<Block>().SetHold(false);
            gameObject.layer = LayerMask.NameToLayer("Robot");
        }
        Debug.Log(!GetComponent<Block>().GetHold());
        if (!GetComponent<Block>().GetHold())
        {
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
            else if (collision.gameObject.tag != "RobotPiece")
            {
                gameObject.AddComponent<DestroyTimer>();
                gameObject.GetComponent<DestroyTimer>().SetDestroyTime(5.0f);
            }
            Destroy(this);
        }
    }

    private void Start()
    {
        GetComponent<IAttachable>().rb.gravityScale = 0;
        move = dir.normalized * speed;
    }

    private void Update()
    {
        if (shot)
        {
            move = dir.normalized * speed;
            //Debug.Log(move);
            transform.position += (Vector3)move * Time.deltaTime;
        }
    }

    public void SetShot(bool inp_shot)
    {
        shot = inp_shot;
    }

    public void SetSpeed(float speed_factor)
    {
        speed = init_speed * speed_factor;
    }
}