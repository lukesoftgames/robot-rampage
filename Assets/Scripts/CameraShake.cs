using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
  public static CameraShake current;

  bool shaking;
  void Awake() {
    current = this;
  }
  
  IEnumerator ShakeEnumerator (float duration, float magnitude, Action callback) {
    Vector3 originalPos = transform.localPosition;
    
    float elapsed = 0.0f;
    while (elapsed < duration && shaking) {
      float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
      float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;
      transform.position = new Vector3(originalPos.x + x,originalPos.y+y,originalPos.z);
      yield return null;
    }
    callback();
  }
  public void Shake(float duration, float magnitude) {
    IEnumerator shaker = ShakeEnumerator(duration, magnitude, ()=> {});
    shaking = true;
    StartCoroutine(shaker);
  }
  public void Shake(float duration, float magnitude, Action callback) {
    IEnumerator shaker = ShakeEnumerator(duration, magnitude, callback);
    shaking = true;
    StartCoroutine(shaker);
  }
  public void Shake(float magnitude) {
     IEnumerator shaker = ShakeEnumerator(1000f, magnitude, () => {});
    shaking = true;
    StartCoroutine(shaker);
  }
  public void StopShake() {
    shaking = false;
  }
}
