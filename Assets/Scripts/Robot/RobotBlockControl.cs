using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBlockControl : MonoBehaviour
{
    private bool holding;
    private GameObject heldBlock;
    private Vector2 startPlace;
    public static List<GameObject> pieces = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("RobotPiece"))
        {
            pieces.Add(obj);
        }
        heldBlock = null;
    }

    // Update is called once per frame
    void Update()
    {
        pickupPiece();
    }

    private void pickupPiece()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "block")
                {
                    heldBlock = hit.collider.gameObject;
                    holding = true;
                    Destroy(heldBlock.GetComponent<building_tile>());
                    heldBlock.GetComponent<Block>().SetHold(holding);
                    heldBlock.AddComponent<IAttachable>();
                    heldBlock.AddComponent<MovingAttachable>();
                    heldBlock.GetComponent<MovingAttachable>().SetShot(false);
                    Destroy(heldBlock.GetComponent<DestroyTimer>());
                    heldBlock.layer = 0;
                }
                //Debug.Log(hit.collider.gameObject.tag);
            }
        }
        if (heldBlock != null)
        {
            holding = heldBlock.GetComponent<Block>().GetHold();
        }
        else
        {
            holding = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (holding)
            { 
                if (heldBlock == null) {
                    return;
                }
                startPlace = heldBlock.transform.position;
                Vector2 dir = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPlace;
                if (dir.magnitude>0.1)
                {
                    if (Physics2D.Raycast(startPlace, dir))
                    {
                        Debug.Log(dir.magnitude); 
                        holding = false;
                        MovingAttachable move = heldBlock.GetComponent<MovingAttachable>();
                        move.dir = dir;
                        float speed_factor = dir.magnitude;
                        if (speed_factor > 1.5) speed_factor = 1.5f;
                        move.SetSpeed(speed_factor);
                        move.SetShot(true);
                        heldBlock.GetComponent<Block>().SetHold(holding);
                        heldBlock = null;
                    }
                }
                else
                {
                    holding = false;
                    heldBlock.GetComponent<Block>().SetHold(holding);
                    heldBlock = null;
                }
            }
        }
    }
}
