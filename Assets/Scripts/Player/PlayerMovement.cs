using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

 
  // speed of horizontal movement
  public float maxSpeed;
  public bool won;
 
  Rigidbody2D rb;

  DistanceJoint2D distanceJoint;
 
 
   // power of jump

  public float jumpAmount;
  // add extra weight when falling for more impact
  public float fallMultiplier = 2.5f;
  // the amount to damp by if a hop
  public float lowJumpMultiplier = 2f;
  bool jump;
  float xaxis;
  float yaxis;

  int climbable = 0;



void Start () {
    rb = GetComponent<Rigidbody2D>();
  } 

 bool IsOnGround() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
     if (hit.collider != null)
      {
        float distance = Mathf.Abs(hit.point.y - transform.position.y);
        if (distance < 0.6f) {
          return true;
        } else {
          return false;
        }
      }
      return false;
    }
  void Update() {
        if (!won)
        {


            if (climbable > 0 && Mathf.Abs(yaxis) > 0.2f)
            {
                rb.gravityScale = 0f;
            }
            else
            {
                rb.gravityScale = 1f;
            }
            if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
            {
                jump = true;
            }
            xaxis = Input.GetAxisRaw("Horizontal");
            yaxis = Input.GetAxisRaw("Vertical");
            //adjust jump

            if (climbable == 0)
            {
                if (rb.velocity.y < 0)
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }
                else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
                }
            }
        }
    
  }
  void FixedUpdate () {
        if (!won)
        {
            rb.velocity = new Vector2(xaxis * maxSpeed, rb.velocity.y); //multiply the direction by an appropriate speed, Vector3(0,1,0) is straight up.
            if (climbable > 0)
            {
                transform.Translate(new Vector2(0, yaxis * 0.1f));
            }

            if (jump && IsOnGround())
            {
                jump = false;
                rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            }
        }
  }

  void OnTriggerEnter2D(Collider2D col)
  {
    climbable++;
  }
  
  void OnTriggerExit2D(Collider2D col)
  {
    climbable--;
  }
}
