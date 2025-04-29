using HStrong.Quests;
using UnityEngine;
public class GameEventsManager : Singleton<GameEventsManager>
{
    public QuestEvents questEvent  = new QuestEvents();
    public GoldEvents goldEvents = new GoldEvents();
    public MiscEvents miscEvents = new MiscEvents();
    public InventoryEvent inventoryEvent =  new InventoryEvent();
    public InputEvent inputEvent = new InputEvent();
    public HealthEvent healthEvent = new HealthEvent();
    public ShopEvents shopEvents = new ShopEvents();
}
public static class EventExtensionQuest
{
    public static GameEventsManager GameEvents(this MonoBehaviour sender)
    {
        return GameEventsManager.Instance;
    }
    
}