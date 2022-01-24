using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class ChapterData
{
  [SerializeField]
  int scene;
  public int Scene { get {return scene; } set { this.scene = value;} }
  
  [SerializeField]
  int character1;
  public int Character1 { get {return character1; } set { this.character1 = value;} }
  
  [SerializeField]
  int character2;
  public int Character2 { get {return character2; } set { this.character2 = value;} }
  
  [SerializeField]
  int highlight;
  public int Highlight { get {return highlight; } set { this.highlight = value;} }
  
  [SerializeField]
  string dialogue;
  public string Dialogue { get {return dialogue; } set { this.dialogue = value;} }
  
}