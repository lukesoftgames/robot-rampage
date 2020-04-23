using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetter : MonoBehaviour
{
    public enum TileState {Empty, Destroyed, Healthy};
    // Start is called before the first frame update
    [Header("Tile Base")]
    public Sprite baseTile;
    public Sprite crackedTile;
    [Header("Edge Overlay")]
    public Sprite left;
    public Sprite top;
    public Sprite right;
    public Sprite bottom;
    [Header("Destruction")]
    public Sprite destoryedTop;
    public Sprite destoryedLeft;
    public Sprite destoryedRight;
    public Sprite destoryedBottom;
    [Header("Varients")]
    public Sprite steel;
    void Start()
    {
        
    }
    private Sprite GetEdge(int edge) {
      switch (edge) {
        case 0:
          return bottom;
        case 1:
          return right;
        case 2:
          return top;
        case 3: 
          return left;
        default:
          Debug.LogError("Invalid edge");
          return null;
      }
    }
    private Sprite GetDestroyedEdge(int edge) {
      switch (edge) {
        case 0:
          return destoryedBottom;
        case 1:
          return destoryedRight;
        case 2:
          return destoryedTop;
        case 3: 
          return destoryedLeft;
        default:
          Debug.LogError("Invalid edge");
          return null;
      }
    }
    private GameObject AddChild(GameObject parent) {
      GameObject go = new GameObject();
      go.AddComponent(typeof(SpriteRenderer));
      go.AddComponent(typeof(SpriteMask));
      go.transform.SetParent(parent.transform);
      go.transform.position = parent.transform.position;
      go.transform.localScale = new Vector3(1,1,1);
      return go;
    }
    public void DecorateTile(int[,] neighborhood, GameObject g) {
      //Debug.Log(neighborhood.ToString());
      building_tile bt = g.GetComponent<building_tile>();
      if (bt.GetHealth() < 100) {
        g.GetComponent<SpriteRenderer>().sprite = crackedTile;
      } else {
        g.GetComponent<SpriteRenderer>().sprite = baseTile;
      }
      int[] directNeighbors = {neighborhood[0,1], neighborhood[1,2], neighborhood[2,1], neighborhood[1,0]};
      for (int i = 0; i < directNeighbors.Length; i++) {
        if (directNeighbors[i] == 0) {
          GameObject edgeGO = AddChild(g);
          edgeGO.GetComponent<SpriteRenderer>().sprite =  GetEdge(i);
        } else if (directNeighbors[i] == 2) {
          GameObject edgeGO = AddChild(g);
          if (g.GetComponent<SpriteMask>() == null) {
            g.AddComponent(typeof(SpriteMask));
          }
          edgeGO.GetComponent<SpriteMask>().sprite = GetDestroyedEdge(i);
        }
      }      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
