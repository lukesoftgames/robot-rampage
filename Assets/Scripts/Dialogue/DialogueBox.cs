using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class DialogueBox : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueManager dialogueManager;
    public bool active;
    public TextMeshProUGUI textMesh; 
    public float textSpeed = 0.05f;
    public bool writing = false;
    int charsRevealed;
    public Image portrait;

    public Sprite playerPortrait;
    public Sprite helpPortrait;
    void Start()
    {
      textMesh.SetText("");
      gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -100f, 0f);
    }
    public void ShowAndPlay(string dialogueId) {
      textMesh.SetText("");
      writing = false;
      Debug.Log(dialogueId);
        LeanTween.moveY(gameObject.GetComponent<RectTransform>(), 120f, 0.5f).setEase(LeanTweenType.easeInBack)
        .setOnComplete(() => Play(dialogueId));

    }
    Sprite GetImage(string actor) {
      if (actor == "help") {
              Debug.Log(actor);

        return helpPortrait;
      } else {
        return playerPortrait;
      }
    }
    IEnumerator showText(Dialogue d) {
      textMesh.SetText(d.text);
      portrait.sprite = GetImage(d.actor);
      charsRevealed = 0;
      writing = true;
      int i = 0;
      while (i < d.text.Length) {
        charsRevealed += 1;
        textMesh.maxVisibleCharacters = charsRevealed;
        yield return new WaitForSeconds(textSpeed);
        i++;
      }
      writing = false;
    }
    public void Play(string id) {
      active = true;
      Dialogue d = dialogueManager.GetDialogue(id);
      Debug.Log(d.text);
      IEnumerator coroutine = showText(d);
      StartCoroutine(coroutine);
    }
    public void HideBox(Action callback) {
      active =false;
      writing =false;
      
      LeanTween.moveY(gameObject.GetComponent<RectTransform>(), -100, 0.5f).setEase(LeanTweenType.easeInBack)
      .setOnComplete(callback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
