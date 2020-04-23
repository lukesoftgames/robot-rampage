using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        
    }

    public void Play()
    {
        Debug.Log("PLAY BOOm");
        audioData.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
