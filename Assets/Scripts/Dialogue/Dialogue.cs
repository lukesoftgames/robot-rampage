using UnityEngine;
using System;

[Serializable]
public class Dialogue
{
  public string dialogueId;
  public string text;
  public bool finish;
  public string nextId;

  public string actor;
}