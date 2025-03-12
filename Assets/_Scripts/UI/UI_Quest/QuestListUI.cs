using UnityEngine;
using HStrong.Quests;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] QuestItemUI questPrefab;
    QuestList questList;
    
    // Start is called before the first frame update
    void Start()
    {
        questList = PlayerManager.GetPlayer().GetComponent<QuestList>();
        questList.onUpdate += Redraw;
        Redraw();
    }

    private void Redraw()
    {
        transform.DetachChildren();
        foreach (QuestStatus status in questList.GetStatuses())
        {
            QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
            uiInstance.Setup(status);
        }
    }
}