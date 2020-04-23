using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void DrawGrappleGuide(Vector2 start, Vector2 end) {
      LineRenderer lineRenderer = GetComponent<LineRenderer>();
      lineRenderer.SetVertexCount(2);
      lineRenderer.SetPosition(0, start);
      lineRenderer.SetPosition(1, end);
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
