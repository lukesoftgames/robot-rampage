using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private List<GameObject> enemyTypes = new List<GameObject>();
    private List<float> weights = new List<float> { 0.7f, 1.0f };
    private float waveTime = 25.0f;
    private float currentTime;

    private void Update() {
        currentTime += Time.deltaTime;

        if(currentTime >= waveTime) {
            currentTime = 0.0f;

            float rand = Random.Range(0.0f, 1.0f);
            int index = -1;

            for(int i = 0; i < weights.Count; i++) {
                if(rand <= weights[i]) {
                    index = i;
                    break;
                }
            }

            if(index >= 0 && index < enemyTypes.Count) Instantiate(enemyTypes[index], transform.position, Quaternion.identity);
        }
    }
}