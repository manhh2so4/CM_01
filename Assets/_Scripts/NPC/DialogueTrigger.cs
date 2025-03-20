using UnityEngine;
using System.Diagnostics;
public class DialogueTrigger : MonoBehaviour,IClicker
{
    [SerializeField] DialogueGraph dialogue = null;
    public Chat chat;
    [SerializeField] string NameNPC;
    public bool isTalking = false;
    void Awake()
    {
        chat =  transform.GetComponentInChildren<Core>().GetCoreComponent<Chat>();
    }
    public void OnClick()
    {   
        
        if ( isTalking ) return;
        if (dialogue == null) return;
        isTalking = true;


        NodeParser.instance.StartDialogue(this,dialogue);
        
        

    }

    public string GetName()
    {
        return NameNPC;
    }
}       