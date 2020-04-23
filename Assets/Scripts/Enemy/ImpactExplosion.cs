using System.Collections.Generic;
using UnityEngine;

public class ImpactExplosion : MonoBehaviour {
    private float currentTime = 0.0f;
    private GameObject currentFlash = null;
    private int spriteIndex = 0;
    [SerializeField] private float impactTime;
    [SerializeField] private List<GameObject> impactSprites = new List<GameObject>();
    // Update is called once per frame
    void Update() {
        if(currentTime > impactTime) {
            Destroy(currentFlash);
            Destroy(gameObject);
        } else if(currentTime == 0.0f) {
            currentFlash = Instantiate(impactSprites[spriteIndex], transform.position, Quaternion.identity);
            spriteIndex++;
        } else if(currentTime > (impactTime / 2.0f) && spriteIndex == 1) {
            Destroy(currentFlash);
            currentFlash = Instantiate(impactSprites[spriteIndex], transform.position, Quaternion.identity);
            spriteIndex++;
        }

        currentTime += Time.deltaTime;
    }
}