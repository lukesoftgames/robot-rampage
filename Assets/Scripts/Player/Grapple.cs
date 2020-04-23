using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    // Start is called before the first frame update
    GrappleRenderer grappleRenderer;
    public GameObject grapple;
    bool grappling;
    Vector2 mousePos;
    void Start()
    {
      grappleRenderer = grapple.GetComponent<GrappleRenderer>();
      grappling = false;
      grapple.SetActive(grappling);
    }
   
    // Update is called once per frame
    void Update()
    {
      
    
      
   
    }
}
