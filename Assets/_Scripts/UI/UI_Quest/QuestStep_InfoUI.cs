using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using HStrong.Quests;

public class QuestStep_InfoUI : MonoBehaviour,IObjectPoolItem,IPrefab {
    [SerializeField] TextMeshProUGUI describleQuestStep;
    [SerializeField] TextMeshProUGUI StatusQuestStep;
    [SerializeField] Image checkBox;
    [SpritePreview][SerializeField] Sprite[] Tick;
    Color color1 = HexToColor(" #00FEFE");
    Color color2 = HexToColor(" #FFEB04");
    Color color3 = HexToColor(" #aab0aa");

    public void SetQuestStepInfo(string describle, QuestStepState status){
        describleQuestStep.text = describle;
        StatusQuestStep.text = status.status;

        switch(status.state){
            case QuestStepStatus.COMPLETED:
                checkBox.sprite = Tick[1];
                describleQuestStep.color = color1;

                break;
            case QuestStepStatus.IN_PROGRESS:
                checkBox.sprite = Tick[0];
                describleQuestStep.color = color2;

                break;
            case QuestStepStatus.NOT_STARTED:
                checkBox.sprite = Tick[0];
                describleQuestStep.color = color3;

                break;
            default:
                break;
        }
    }
    static Color HexToColor(string hex)
    {
        // Nếu có dấu "#" ở đầu, loại bỏ
        if (hex.StartsWith(" #"))
        {
            hex = hex.Substring(2);
        }
        // Chuyển đổi thành giá trị RGB
        float r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        float g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        float b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        return new Color(r, g, b);
    }

    public void RemoveQuestStepInfo(){
        ReturnItemToPool();
    }
    #region CreatPool

        ObjectPool objectPool;
        void ReturnItemToPool()
        {
            if (objectPool != null)
            {
                this.transform.SetParent(PoolsContainer.Instance.transform);
                objectPool.ReturnObject(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void SetObjectPool(ObjectPool pool)
        {
            objectPool = pool;
        }
        public void Release()
        {
            objectPool = null;
        }

    #endregion
}