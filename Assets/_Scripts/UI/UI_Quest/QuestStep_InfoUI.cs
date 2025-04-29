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
    Color color1 = HSTool.HexToColor("#00FEFE");
    Color color2 = HSTool.HexToColor("#FFEB04");
    Color color3 = HSTool.HexToColor("#aab0aa");

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
    

    public void RemoveQuestStepInfo(){
        ReturnToPool();
    }
    #region CreatPool

        ObjectPool objectPool;
        public void SetObjectPool(ObjectPool pool)
        {
            objectPool = pool;
        }

        public void Release()
        {
            objectPool = null;
        }

        public void ReturnToPool()
        {
            if (objectPool != null)
            {
                objectPool.ReturnObject(this);
                this.transform.SetParent(PoolsContainer.Instance.transform);
            }else
            {
                Destroy(gameObject);
            }
        }

    #endregion
}