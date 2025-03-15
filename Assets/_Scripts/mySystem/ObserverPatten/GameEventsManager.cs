using HStrong.Quests;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }
    public QuestEvents questEvent ;
    public GoldEvents goldEvents ;
    public MiscEvents miscEvents ;
    public InventoryEvent inventoryEvent ;
    public InputEvent inputEvent ;
    
    void Awake()
    {
        questEvent = new QuestEvents();
        goldEvents = new GoldEvents();
        miscEvents = new MiscEvents();
        inventoryEvent = new InventoryEvent();
        inputEvent = new InputEvent();

        if(instance != null){
            Debug.LogError("Found more than one Game Events Manager in the scene.");
            Destroy(this);
        }
        else{
            instance = this;
        }

        Debug.Log("Quest_event");


    }
}
public static class EventExtensionQuest
{
    public static GameEventsManager GameEvents(this MonoBehaviour sender)
    {
        return GameEventsManager.instance;
    }
    
}