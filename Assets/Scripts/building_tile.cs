using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building_tile : MonoBehaviour
{
    private int health;
    private bool detached;
    public GameObject hit;
    public GameObject hitSmall;
    private float speed = 5.0f;
    private Rigidbody2D rb;
    private building building;

    private Vector2 coords;
    private bool floored;

    private float removeTime = 5.0f;
    private float curRemoveTimer = 0;
    private AudioSource audioData;


    // Start is called before the first frame update
    void Awake()
    {
        audioData = GetComponent<AudioSource>();
        detached = false;
        floored = false;
        health = 100;
        rb = this.GetComponent<Rigidbody2D>();
    }
    public int GetHealth()
    {
        return health;
    }
    public void SetBuilding(building b, Vector2 c)
    {
        //Debug.Log(c + " set");
        building = b;
        coords = c;
    }
    // Update is called once per frame
    void Update()
    {
        if (detached & !floored)
        {
            if (!floored)
            {
                rb.AddForce(Physics.gravity);
            }

            if (curRemoveTimer > removeTime)
            {
                Destroy(transform.gameObject);
            }
            else
            {
                curRemoveTimer += Time.deltaTime;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.gameObject.tag == "ball" & !detached & !floored)
        {
            audioData.Play(0);
            Destroy(collision.gameObject);
            health = health - 50;
            building.DrawTiles();
            if (health <= 0)
            {
                building.UpdateState(coords, 2);
                ReleaseBlock(false);

            }
            else
            {
                Instantiate(hit, transform.position, Quaternion.identity);

            }
        }

        if (collision.name == "floor" & detached & !floored)
        {
            detached = false;
            rb.velocity = Vector2.zero;
            floored = true;
            this.GetComponent<BoxCollider2D>().isTrigger = false;
            rb.gravityScale = 10;
        }
    }

    public void Burst()
    {
        ReleaseBlock(true);
    }

    private void ReleaseBlock(bool burst)
    {
        transform.parent = null;
        detached = true;
        Instantiate(hit, transform.position, Quaternion.identity);
        Vector2 fDir = Vector2.up;
        if (burst)
        {
            if (Random.value > 0.5)
            {
                fDir -= Vector2.right;
            }
            else
            {
                fDir += Vector2.right;
            }
        }
        else
        {
            fDir -= Vector2.right;
        }

        float strength = Random.Range(0.5f, 1.5f);
        rb.AddForce((fDir * 5.0f + 2.5f * rb.velocity.normalized) * strength, ForceMode2D.Impulse);
        transform.tag = "block";
        gameObject.layer = LayerMask.NameToLayer("Block");
        transform.gameObject.AddComponent<Block>();
        this.GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.AddComponent<DestroyTimer>();
        gameObject.GetComponent<DestroyTimer>().SetDestroyTime(removeTime);
        Destroy(this);
    }
}
