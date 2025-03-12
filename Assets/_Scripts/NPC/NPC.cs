using UnityEngine;


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
        if (dialogue == null) return;
        NodeParser.instance.StartDialogue(chat,dialogue);
    }

    public string GetName()
    {
        return NameNPC;
    }
}       