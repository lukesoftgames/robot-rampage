using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : MonoBehaviour
{
    public GameObject myPrefab;
    public TileSetter tileSetter;
    private float structuralHealth=1;
    private float initNumCols = 5;
    private float curNumTiles = 0;
    private float totalTiles = 0;
    // Tile state 0->never present, 1-> healthy, 2-> destroyed, 
    private int[,] tileGridStates;
    private Dictionary<Vector2, GameObject> tiles;
    private int maxHeight = 15;
    private AudioSource audioData;
    public GameObject explosionSound;
    private bool playing;


    // Start is called before the first frame update
    void Start()
    {
      audioData = GetComponent<AudioSource>();
      tiles = new Dictionary<Vector2, GameObject>();
      tileGridStates = new int[ (int) initNumCols + 2, maxHeight + 2];
      for (int i = 0; i < initNumCols; i++)
        {
            float numTiles = Random.Range(5, maxHeight);

            for (int j=0; j < numTiles; j++)
            {
              tileGridStates[i+1,j+1] = 1;
                Vector3 pos = transform.position;
                pos = pos + new Vector3(i, j, 0);
                GameObject buildingTile = Instantiate(myPrefab, pos, Quaternion.identity, transform);
                Vector2 coords = new Vector2(j+1, i+1);
                buildingTile.GetComponent<building_tile>().SetBuilding(this, coords);
                tiles.Add(coords, buildingTile);
                totalTiles += 1;
            }
        }
        DrawTiles();
    }
    public void DrawTiles() {
      foreach(KeyValuePair<Vector2, GameObject> entry in tiles) {
          int[,] neighborhood = new int[3,3];
          Vector2 pos = entry.Key;
          GameObject val = entry.Value;
          for (float i = 0f; i <= 2f; i++) {
            for (float j = 0f; j <= 2f; j++) {
              neighborhood[(int) j, (int) i] = tileGridStates[(int) (pos.y + i - 1f), (int) (pos.x + j - 1f)];
            }
          }
          tileSetter.DecorateTile(neighborhood, val);
        }
    }
    public void UpdateState(Vector2 coords, int newState) {
      tileGridStates[(int) coords.y, (int) coords.x] = newState;
      if (newState == 2) {
        tiles.Remove(coords);
      }
      Debug.Log("update");
      DrawTiles();
    }


    // Update is called once per frame
    void Update()
    {
        if (structuralHealth <= 0.8)
        {
            if (!playing)
            {
                audioData.Play();
                playing = true;
            }
            Debug.Log("BOOM");
            GameEvents.current.BuildingCollapse();
            foreach (Transform child in transform)
            {
                child.GetComponent<building_tile>().Burst();
            }
            transform.tag = "Untagged";
            //Destroy(transform.gameObject);
        }
        else
        {
            curNumTiles = 0;
            foreach (Transform child in transform)
            {
                curNumTiles += 1;
            }
            structuralHealth = curNumTiles / totalTiles;
        }
    }
}
