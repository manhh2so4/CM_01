using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_NotifyBuff : MonoBehaviour,IObjectPoolItem {
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI TimeCount;
    public void SetData(Sprite icon){
        this.icon.sprite = icon;
        this.icon.SetNativeSize();
    }
    public void SetTime(string time){
        TimeCount.text = time;
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
            if(PoolsContainer.Instance != null){
                this.transform.SetParent(PoolsContainer.Instance.transform);
            }
        }else
        {
            Destroy(gameObject);
        }
    }
#endregion

}