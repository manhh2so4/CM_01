using UnityEngine;
using HStrong.Dialogue;

public class NPC : MonoBehaviour,IClicker
{
    [SerializeField] DialogueGraph dialogue = null;
    public Chat chat;
    [SerializeField] string NameNPC;
    void Awake()
    {
        chat =  transform.GetComponentInChildren<Core>().GetCoreComponent<Chat>();
    }
    public void OnClick()
    {
        Debug.Log("get  Dialogue");
        if (dialogue == null) return;
        //Debug.Log("get  Dialogue");
        NodeParser.instance.StartDialogue(this,dialogue);
    }
    public string GetName()
    {
        return NameNPC;
    }
}       