using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextAsset dialogueFile;

    public DialogueBox dialogueBox;
    DialogueDatabase database;
    Dictionary<string, Dialogue> dict;

    string currentId;
  public static DialogueManager current;

  public Action DialogueDone = delegate {};

    void Awake()
    {
      current = this;
      dict = new Dictionary<string, Dialogue>();
        database = JsonUtility.FromJson<DialogueDatabase>(dialogueFile.text);
        foreach (Dialogue d in database.dialogues) {
          Debug.Log("adding "+ d.dialogueId);
         dict.Add(d.dialogueId, d);
        }
    }
    void FinishDialogue() {
      DialogueDone();
      DialogueDone = delegate {};
    }
    void Next() {
      Dialogue d = GetDialogue(currentId);
      if (d.finish) {
        Debug.Log("close");
        dialogueBox.HideBox(FinishDialogue);
        
      } else {
        currentId = d.nextId;
        Debug.Log("Playing " + currentId);
        dialogueBox.Play(currentId);
      }
    }
    void Update() {
      if (!dialogueBox.writing && dialogueBox.active) {
        if (Input.GetKeyDown(KeyCode.Space)) {
          Next();
        }
      }
    }
    public Dialogue GetDialogue(string s){
      Dialogue dialogue;
      dict.TryGetValue(s,out dialogue);
      return dialogue;
    }
    public DialogueManager Play(string id) {
      currentId = id;
      dialogueBox.ShowAndPlay(id);
      return current;
    }
    public void OnDone(Action callback) {
      DialogueDone = callback;
    }
   
}
