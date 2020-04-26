using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static List<GameObject> pieces = new List<GameObject>();
    public GameObject currentPiece;
    public GameObject placementArrow;
    public GameObject buildingTilePrefab;
    public GameObject player;
    private GameObject[] blocks;
    private Vector2 startPlace;
    private Vector2 endPlace;
    private GameObject heldBlock;
    private bool holding = false;
    private float radius = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        placePiece();
    }

    private void placePiece()
    {
        //startPlace = player.transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("PRESSING DOWN");
            startPlace = player.transform.position;//Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!holding)
            {
                blocks = GameObject.FindGameObjectsWithTag("block");

                GameObject closest = null;
                float distance = Mathf.Infinity;
                Vector3 position = player.transform.position;

                if (blocks.Length != 0)
                {
                    Debug.Log("Search");
                    foreach (GameObject block in blocks)
                    {
                        Vector3 diff = block.transform.position - position;
                        float curDistance = diff.sqrMagnitude;
                        if (curDistance < distance)
                        {
                            closest = block;
                            distance = curDistance;
                        }
                    }
                    distance = Vector3.Distance(player.transform.position, closest.transform.position);
                    Debug.Log(distance);
                    if (distance < 2.0f)
                    {
                        Debug.Log("NEAR");
                        heldBlock = closest;
                        holding = true;
                        heldBlock.GetComponent<Rigidbody2D>().gravityScale = 0;
                        heldBlock.transform.parent = player.transform;
                        heldBlock.transform.position += new Vector3(0, 1, 0);
                        heldBlock.AddComponent<Block>();
                        heldBlock.AddComponent<IAttachable>();
                        heldBlock.layer = 8;
                        Physics2D.IgnoreLayerCollision(8, 9);
                        Destroy(heldBlock.GetComponent<building_tile>());
                    }

                }

            }
        }




        startPlace = player.transform.position;
        Vector2 dir = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPlace;

        //Debug.Log(Input.GetMouseButtonUp(0));
        if (Input.GetMouseButtonUp(0))
        {
            if (holding)
            {
                if (dir != Vector2.zero)
                {
                    if (Physics2D.Raycast(startPlace, dir))
                    {
                        //GameObject movingPiece = Instantiate(currentPiece, startPlace, Quaternion.identity);
                        MovingAttachable move = heldBlock.AddComponent<MovingAttachable>();
                        Destroy(heldBlock.GetComponent<Block>());
                        move.dir = dir;
                        holding = false;
                        //heldBlock.tag = "RobotPiece";
                        heldBlock.transform.SetParent(null);
                    }
                }
            }
        }

    }


}
