using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCore : IAttachable {
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    private float frameTime = 0.25f;
    private float animationTime = 0.0f;
    private int animationIndex = 0;
    private SpriteRenderer rend;

    private void Start() {
        rend = GetComponent<SpriteRenderer>();
    }
    
    private void Update() {
        coreAnimation();
    }

    private void coreAnimation() {
        if(animationTime > frameTime) {
            animationIndex = ++animationIndex >= sprites.Count ? 0 : animationIndex;
            //Debug.Log(animationIndex);
            rend.sprite = sprites[animationIndex];
            animationTime = 0.0f;
        }

        animationTime += Time.deltaTime;
    }
}