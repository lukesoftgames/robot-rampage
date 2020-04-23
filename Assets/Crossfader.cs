using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Crossfader : MonoBehaviour
{
    // Start is called before the first frame update
    public static Crossfader current;

    public float transitionTime;
  
    public Animator transition;
    public bool playOnStart;
    void Awake()
    {
      transition = GetComponent<Animator>();
        current = this;
    }
    public void TransitionOut(Action callback) {
      transition.SetTrigger("FadeOut");
      StartCoroutine(WaitForTransition(callback));
    }
    IEnumerator WaitForTransition(Action callback){
      yield return new WaitForSeconds(transitionTime);
      callback();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
